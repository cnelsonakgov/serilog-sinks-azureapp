// Copyright 2013-2018 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Microsoft.Extensions.Logging;

namespace Serilog.Sinks.AzureApp
{
    class AzureAppSink : ILogEventSink
    {
        readonly ITextFormatter _textFormatter;
        readonly Microsoft.Extensions.Logging.ILogger _logger;

        public AzureAppSink(ITextFormatter textFormatter)
        {
            if (textFormatter == null) throw new ArgumentNullException(nameof(textFormatter));
       
            _textFormatter = textFormatter;
            _logger = AzureAppLogging.CreateLogger<AzureAppSink>();
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
           
            var sr = new StringWriter();
            _textFormatter.Format(logEvent, sr);
            var text = sr.ToString().Trim();

            if (logEvent.Level == LogEventLevel.Fatal)
                _logger.LogCritical(text);
            else if (logEvent.Level == LogEventLevel.Error)
                _logger.LogError(text);
            else if (logEvent.Level == LogEventLevel.Warning)
                _logger.LogWarning(text);
            else if (logEvent.Level == LogEventLevel.Information)
                _logger.LogInformation(text);
            else if (logEvent.Level == LogEventLevel.Debug)
                _logger.LogDebug(text);
            else
                _logger.LogTrace(text);
        }

        public static class AzureAppLogging
        {
            public static ILoggerFactory LoggerFactory {get;} = new LoggerFactory().AddAzureWebAppDiagnostics();
            public static Microsoft.Extensions.Logging.ILogger CreateLogger<T>() =>
                LoggerFactory.CreateLogger<T>();
        }
    }
}
