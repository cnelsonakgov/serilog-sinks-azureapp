# Serilog Azure Diagnostics Application Logging Sink

Write [Serilog](https://github.com/serilog) events to the Azure Diagnostics Application Log using _Microsoft.Extensions.Logging_ and the [Microsoft.Extensions.Logging.AzureAppServices](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices). Enables using the the Azure Log Stream and Blob storage for events.

Designed for `serilog-aspnetcore`. Works with `UseSerilog()`.

```
var log = new LoggerConfiguration()
    .WriteTo.AzureApp()
    .CreateLogger();
```
