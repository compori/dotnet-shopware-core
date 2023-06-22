#tool nuget:?package=NuGet.CommandLine&version=6.5.0
#tool nuget:?package=Microsoft.TestPlatform&version=17.6.0
#tool nuget:?package=coverlet.console&version=3.2.0
#tool nuget:?package=ReportGenerator&version=5.1.20
#addin nuget:?package=Cake.Coverlet&version=3.0.4

var target = Argument("target", "Default");
var configuration = Argument("Configuration", "Release");
var outputDirectory = Argument<DirectoryPath>("OutputDirectory", "output");
var codeCoverageDirectory = Argument<DirectoryPath>("CodeCoverageDirectory", "output/coverage");
var packageDirectory = Argument<DirectoryPath>("CodeCoverageDirectory", "output/packages");
var solutionFile = Argument("SolutionFile", "shopware-core.sln");
var versionSuffix = Argument("VersionSuffix", "");
var nugetDeployFeed = Argument("NugetDeployFeed", "https://api.nuget.org/v3/index.json");
var nugetDeployApiKey = Argument("NugetDeployApiKey", "");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

// Target : Clean
// 
// Description
// - Cleans binary directories.
// - Cleans output directory.
// - Cleans the test coverage directory.
Task("Clean")
    .Does(() =>
{
    CleanDirectory(packageDirectory);
    CleanDirectory(codeCoverageDirectory);
    CleanDirectory(outputDirectory);


    // remove all binaries in source files
    var srcBinDirectories = GetDirectories("./src/**/bin");
    foreach(var directory in srcBinDirectories)
    {
        CleanDirectory(directory);
    }

    // remove all intermediates in source files
    var srcObjDirectories = GetDirectories("./src/**/obj");
    foreach(var directory in srcObjDirectories)
    {
        CleanDirectory(directory);
    }

    // remove all binaries in test files
    var testsBinDirectories = GetDirectories("./tests/**/bin");
    foreach(var directory in testsBinDirectories)
    {
        CleanDirectory(directory);
    }
    
    // remove all intermediates in source files
    var testsObjDirectories = GetDirectories("./tests/**/obj");
    foreach(var directory in testsObjDirectories)
    {
        CleanDirectory(directory);
    }    
});

// Target : Restore-NuGet-Packages
// 
// Description
// - Restores all needed NuGet packages for the projects.
Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    // https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore
    //
    // Reload all nuget packages used by the solution
    NuGetRestore(solutionFile);
});

// Target : Build
// 
// Description
// - Builds the artifacts.
Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
    
      // Use MSBuild
      MSBuild(solutionFile, settings => {
        settings.ArgumentCustomization = 
            args => args
                .Append("/p:IncludeSymbols=true")
                .Append("/p:IncludeSource=true")
                .Append($"/p:VersionSuffix={versionSuffix}");
        settings.SetConfiguration(configuration);
      });
    
    } else {
    
      // Use XBuild
      XBuild(solutionFile, settings => {
        settings.ArgumentCustomization = 
            args => args
                .Append("/p:IncludeSymbols=true")
                .Append("/p:IncludeSource=true")
                .Append($"/p:VersionSuffix={versionSuffix}");
        settings.SetConfiguration(configuration);
      });

    }
});

// Target : Test
// 
// Description
// - Executes the test and generates with code coverage files.
Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    CreateDirectory(codeCoverageDirectory);

    var includeFilter = "[Compori.Shopware*]Compori.Shopware*";
    var excludeFilter = "[xunit.*]*,[*]Compori.Shopware.MimeTypes*";

    var coverletPath = Context.Tools.Resolve("coverlet.console.dll");
    Information("coverlet.exe: " + (coverletPath ?? "N/A"));
    var vstestPath = Context.Tools.Resolve("vstest.console.exe");    
    Information("vstest.console.exe: " + (vstestPath ?? "N/A"));

    var testAssemblies = GetFiles($"./tests/**/bin/{configuration}/net35/*Tests.dll");
    foreach(var testAssembly in testAssemblies)
    {
        var assemblyDirectory = testAssembly.GetDirectory();
        var testAssemblyPath = testAssembly.FullPath;
        var targetFramework = assemblyDirectory.Segments[assemblyDirectory.Segments.Length - 1];
        
        var logFileName = testAssembly.GetFilenameWithoutExtension() + "." + targetFramework + ".trx";
        var logFilePath = MakeAbsolute(codeCoverageDirectory).CombineWithFilePath(logFileName);

        var coverageFile = testAssembly.GetFilenameWithoutExtension() + ".cobertura." + targetFramework + ".xml";
        var coveragePath = MakeAbsolute(codeCoverageDirectory).CombineWithFilePath(coverageFile);

        // VSTest test
        DotNetExecute(
            coverletPath,
            new ProcessArgumentBuilder()
                    .Append("\"" + testAssemblyPath + "\"")
                    .Append($"--target \"{vstestPath}\"")
                    .Append($"--targetargs \"\\\"{testAssemblyPath}\\\" /Framework:Framework35 /logger:trx;LogFileName=\\\"{logFilePath}\\\"\"")
                    .Append("--format cobertura")
                    .Append("--output \"" + coveragePath.FullPath + "\"")
                    .Append("--include \"" + includeFilter + "\"")
                    .Append("--exclude \"" + excludeFilter + "\""),
            new DotNetExecuteSettings {               
            }
        );       
    }

    var targetFrameworks = new string[] {"net48", "net6.0"};
    var projectFiles = GetFiles("./tests/**/*Tests.csproj");
    foreach(var projectFile in projectFiles)
    {
        foreach(var targetFramework in targetFrameworks)
        {
            var coverageFile = projectFile.GetFilenameWithoutExtension() + ".cobertura.xml";
            var coveragePath = MakeAbsolute(codeCoverageDirectory).CombineWithFilePath(coverageFile);
            var logFileName = projectFile.GetFilenameWithoutExtension() + "." + targetFramework + ".trx";
            var logFilePath = MakeAbsolute(codeCoverageDirectory).CombineWithFilePath(logFileName);

            // coverlet test via dotnet test
            DotNetTest(
                projectFile.FullPath,
                new DotNetTestSettings
                {
                    Configuration = configuration,
                    Framework = targetFramework,
                    NoBuild = true,
                    // https://github.com/dotnet/sdk/issues/29543
                    EnvironmentVariables = new Dictionary<string, string> 
                    {
                        { "DOTNET_CLI_UI_LANGUAGE", "en-US" }
                    },
                    Verbosity = DotNetVerbosity.Minimal,
                    ArgumentCustomization = args => args
                        .Append($"--logger trx;LogFileName=\"{logFilePath}\"")                
                },
                new CoverletSettings 
                {
                    CollectCoverage = true,
                    CoverletOutputFormat = CoverletOutputFormat.cobertura,
                    CoverletOutputDirectory = codeCoverageDirectory,
                    CoverletOutputName = coverageFile,
                    Include = new List<string>() 
                    {
                        includeFilter
                    },
                    Exclude = new List<string>() 
                    {
                        excludeFilter
                    }
                }
            );
        }
    }

    ReportGenerator( 
        new GlobPattern(MakeAbsolute(codeCoverageDirectory).FullPath + "/*.cobertura.*.xml"), 
        MakeAbsolute(codeCoverageDirectory).FullPath + "/report",
        new ReportGeneratorSettings(){
            ReportTypes = new[] { 
                ReportGeneratorReportType.HtmlInline,
                ReportGeneratorReportType.Badges 
            }
        }
    );
});

// Target : Deploy
// 
// Description
// - Deploys package to nuget repository.
Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{
    CreateDirectory(packageDirectory);

    var packageFiles = GetFiles($"src/**/bin/{configuration}/*.nupkg");
    foreach(var packageFile in packageFiles)
    {
        var packageFilename = packageFile.GetFilename();
        var destionation = MakeAbsolute(packageDirectory).CombineWithFilePath(packageFilename);
        CopyFile(packageFile.FullPath, destionation);
    }

    var spackageFiles = GetFiles($"src/**/bin/{configuration}/*.snupkg");
    foreach(var spackageFile in spackageFiles)
    {
        var spackageFilename = spackageFile.GetFilename();
        var sdestination = MakeAbsolute(packageDirectory).CombineWithFilePath(spackageFilename);
        CopyFile(spackageFile.FullPath, sdestination);
    }

    packageFiles = GetFiles(MakeAbsolute(packageDirectory).FullPath + "/*.nupkg");
    spackageFiles = GetFiles(MakeAbsolute(packageDirectory).FullPath + "/*.snupkg");
    if(string.IsNullOrWhiteSpace(nugetDeployApiKey)) 
    {
        Error("No nuget api key provided. Please use argument e.g. --NugetDeployApiKey=1234567-8901-abcd-ef12-13212313121");
        return;
    }

    // Push the package.
    NuGetPush(packageFiles,
        new NuGetPushSettings {
                Source = nugetDeployFeed,
                ApiKey = nugetDeployApiKey,
                SkipDuplicate = true
    });    
});

// Target : Build
// 
// Description
// - Setup the default task.
Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
