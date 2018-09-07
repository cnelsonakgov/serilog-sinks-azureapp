# Serilog.Sinks.AzureApp [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.AzureApp.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.AzureApp/)

A Serilog Azure Diagnostics Application Logging sink.

Write [Serilog](https://github.com/serilog) events to Azure Diagnostics Application Logging using _Microsoft.Extensions.Logging_ and [Microsoft.Extensions.Logging.AzureAppServices](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices). Enables using the the Azure Log Stream and Blob storage for events.

Designed to be used with [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore). Works with `UseSerilog()`.

```
var log = new LoggerConfiguration()
    .WriteTo.AzureApp()
    .CreateLogger();
```
