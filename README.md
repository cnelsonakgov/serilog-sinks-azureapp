# Serilog.Sinks.AzureApp [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.AzureApp.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.AzureApp/)

A Serilog sink that supports Azure App Services 'Diagnostics logs' and 'Log stream' features.

Write [Serilog](https://github.com/serilog) events to Azure Diagnostics Application Logging using _Microsoft.Extensions.Logging_ and [Microsoft.Extensions.Logging.AzureAppServices](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices). Enables using the the Azure Log Stream and Blob storage for events.

Designed to be used with [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore). Works with `UseSerilog()`.

```
var log = new LoggerConfiguration()
    .WriteTo.AzureApp()
    .CreateLogger();
```

Diagnostic logs and log stream are configured in the Azure App Service's Monitoring section.
