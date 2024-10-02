using MultiTelemetry;
using OpenTelemetry.Metrics;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddType1Instrumentation()
            .AddType2Instrumentation();

        // http://localhost:9464/metrics
        metrics.AddPrometheusHttpListener();
    });

var host = builder.Build();
host.Run();
