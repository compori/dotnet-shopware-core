using System;
using System.IO;
using System.IO.Compression;
using Xunit;

namespace Compori.Shopware.Factories
{
    public class SettingsFactoryTests
    {
        [Fact()]
        public void TestReadFromJsonFile()
        {
            var sut = new SettingsFactory();
            Assert.Throws<ArgumentNullException>(() => sut.ReadFromJsonFile(null));
            Assert.Throws<ArgumentNullException>(() => sut.ReadFromJsonFile(""));

            var tmpFile = Path.GetTempFileName();
            sut.SaveJsonFile(tmpFile, new Settings());
            Assert.Null(sut.Settings);
            sut.ReadFromJsonFile(tmpFile);
            Assert.NotNull(sut.Settings);
            File.Delete(tmpFile);
        }

        [Fact()]
        public void TestSaveJsonFile()
        {
            var sut = new SettingsFactory();
            Assert.Throws<ArgumentNullException>(() => sut.SaveJsonFile(null, null));
            Assert.Throws<ArgumentNullException>(() => sut.SaveJsonFile("", null));

            var tmpFile = Path.GetTempFileName();
            Assert.Throws<ArgumentNullException>(() => sut.SaveJsonFile(tmpFile, null));
            sut.SaveJsonFile(tmpFile, new Settings());
            File.Delete(tmpFile);
        }

        [Fact()]
        public void TestReadFromJson()
        {
            var sut = new SettingsFactory();
            Assert.Throws<ArgumentNullException>(() => sut.ReadFromJson(null));
            Assert.Throws<ArgumentNullException>(() => sut.ReadFromJson(""));

            sut.ReadFromJson("{}");
            Assert.False(sut.Settings.EnableTls11);
            Assert.False(sut.Settings.EnableTls12);
            Assert.False(sut.Settings.EnableTls13);
            Assert.False(sut.Settings.ForceTls11);
            Assert.False(sut.Settings.ForceTls12);
            Assert.False(sut.Settings.ForceTls13);
            Assert.False(sut.Settings.Trace);

            Assert.Null(sut.Settings.Url);
            Assert.Null(sut.Settings.ClientAgent);
            Assert.Null(sut.Settings.ClientId);
            Assert.Null(sut.Settings.ClientKey);
            Assert.Equal(Settings.DefaultTimeout, sut.Settings.Timeout);
        }

        [Fact()]
        public void TestCreate()
        {
            var sut = new SettingsFactory();
            Assert.Throws<InvalidOperationException>(() => sut.Create());

            sut.ReadFromJson("{}");
            Assert.NotNull(sut.Create());
        }
    }
}