# Serilog.Sinks.AzureApp [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.AzureApp.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.AzureApp/)

A Serilog sink that supports Azure App Services 'Diagnostics logs' and 'Log stream' features.

Write [Serilog](https://github.com/serilog) events to Azure Diagnostics Application Logging using _Microsoft.Extensions.Logging_ and [Microsoft.Extensions.Logging.AzureAppServices](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices). Enables using the the Azure Log Stream and Blob storage for events.

Designed to be used with [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore). Works with `UseSerilog()`.

## Install

To add Serilog:

```shell
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.AzureApp
```

## Basic Setup

In `Program.cs`:

Add reference and update the `Main` method:

```csharp
using Serilog;

[...]

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

[...]

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });
```

Diagnostic logs and log stream are configured in the Azure App Service's Monitoring section.
