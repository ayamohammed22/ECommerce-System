using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Common.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
            (context, loggerConfiguration) =>
            {
                var env = context.HostingEnvironment;
                loggerConfiguration.MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                    .Enrich.WithProperty("Environment", env.EnvironmentName)
                    .Enrich.WithExceptionDetails()
                    .MinimumLevel.Override("Microsoft.ASPNETCORE", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.LifeTime", LogEventLevel.Warning)
                    .WriteTo.Console();

                if (env.IsDevelopment())
                {
                    loggerConfiguration.MinimumLevel.Override("Basket", LogEventLevel.Debug);
                    loggerConfiguration.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
                    loggerConfiguration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
                    loggerConfiguration.MinimumLevel.Override("Ordering", LogEventLevel.Debug);
                }

                // config elastic search 

                var elasticurl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
                if (!string.IsNullOrEmpty(elasticurl))
                {
                    loggerConfiguration.WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticurl))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv8,
                        IndexFormat = "E-commerce-Logs-{0:yyyy.MM.dd}",
                        MinimumLogEventLevel = LogEventLevel.Debug
                    });
                }
            };

    }
}
