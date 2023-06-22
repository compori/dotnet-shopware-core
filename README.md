# Shopware Core

Shopware 6 API Client

# Build

## Cake installieren oder aktualisieren

In der Powershell mit Administratorenrechten sollte das Cake Build Tool global installiert werden. Optional kann ein nuget Konfigurationsdatei übergeben werden.

```
dotnet tool install --global Cake.Tool --configfile C:\Tools\Nuget\NuGet.Config
```

Wenn Cake bereits installiert ist, kann nach Updates gesucht werden.

```
dotnet tool update --global Cake.Tool --configfile C:\Tools\Nuget\NuGet.Config
```

## Ausführen

```
dotnet-cake.exe build.cake --target="Build" --verbosity=normal
```