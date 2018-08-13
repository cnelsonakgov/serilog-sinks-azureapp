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
using System.Collections.Concurrent;
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
        /// <summary>
        /// Our very own Microsoft.Extensions.LoggerFactory, this is where we'll send Serilog events so that Azure can pick up the logs.
        /// We expect that Serilog has replaced this in the app's services.
        /// </summary>
        static ILoggerFactory CoreLoggerFactory { get; } = new LoggerFactory().AddAzureWebAppDiagnostics();

        /// <summary>
        /// The Microsoft.Extensions.LoggerFactory implementation of CreateLogger(string category) uses lock(_sync) before looking in its dictionary.
        /// We'll use our own ConcurrentDictionary for performance, since we lookup the category on every log write.
        /// </summary>
        readonly ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger> loggerCategories = new ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger>();

        readonly ITextFormatter textFormatter;

        public AzureAppSink(ITextFormatter textFormatter)
        {
            this.textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) 
                throw new ArgumentNullException(nameof(logEvent));
           
            var sr = new StringWriter();
            textFormatter.Format(logEvent, sr);
            var text = sr.ToString().Trim();

            var category = logEvent.Properties.TryGetValue("SourceContext", out var value) ? value.ToString() : "";
            var logger = loggerCategories.GetOrAdd(category, s => CoreLoggerFactory.CreateLogger(s));

            switch (logEvent.Level)
            {
                case LogEventLevel.Fatal:
                    logger.LogCritical(text);
                    break;
                case LogEventLevel.Error:
                    logger.LogError(text);
                    break;
                case LogEventLevel.Warning:
                    logger.LogWarning(text);
                    break;
                case LogEventLevel.Information:
                    logger.LogInformation(text);
                    break;
                case LogEventLevel.Debug:
                    logger.LogDebug(text);
                    break;
                case LogEventLevel.Verbose:
                    logger.LogTrace(text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
