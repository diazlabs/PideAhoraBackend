using Elastic.Apm.SerilogEnricher;
using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;

namespace Infrastructure.Common
{
    internal class Logger
    {
        public static void CreateLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithProperty("Application", "pide-ahora-api")
                .Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", path =>
                {
                    string[] excludedPaths = ["/swagger", "/healthz"];

                    return excludedPaths.Any(x => path.StartsWith(x));
                }))
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch([new Uri(configuration["ElasticConfiguration:Uri"]!)],
                    opts =>
                    {
                        opts.DataStream = new DataStreamName($"logs-{DateTime.UtcNow:yyyy-MM}", "pide-ahora");
                        opts.BootstrapMethod = BootstrapMethod.None;
                        opts.ConfigureChannel = channelOpts =>
                        {
                            channelOpts.BufferOptions = new BufferOptions
                            {
                                ExportMaxConcurrency = 10,
                            };
                        };
                    })
                .CreateLogger();
        }
    }
}
