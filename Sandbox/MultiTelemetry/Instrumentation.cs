namespace MultiTelemetry;

using OpenTelemetry.Metrics;

using System.Diagnostics.Metrics;
using System.Reflection;

public static class Extensions
{
    public static MeterProviderBuilder AddType1Instrumentation(this MeterProviderBuilder builder)
    {
        builder.AddMeter(Type1Metrics.MeterName);
        return builder.AddInstrumentation(() => new Type1Metrics());
    }

    public static MeterProviderBuilder AddType2Instrumentation(this MeterProviderBuilder builder)
    {
        builder.AddMeter(Type2Metrics.MeterName);
        return builder.AddInstrumentation(() => new Type2Metrics());
    }
}

internal sealed class Type1Metrics
{
    internal static readonly AssemblyName AssemblyName = typeof(Type1Metrics).Assembly.GetName();
    internal static readonly string MeterName = AssemblyName.Name!;

    private static readonly Meter MeterInstance = new(MeterName, AssemblyName.Version!.ToString());

    public Type1Metrics()
    {
        MeterInstance.CreateObservableUpDownCounter(
            "test.value",
            () => new Measurement<double>[]
            {
                new(DateTime.Now.Minute, new("type", "type1"), new("id", "1")),
                new(DateTime.Now.Second, new("type", "type1"), new("id", "2")),
            });
    }
}

internal sealed class Type2Metrics
{
    internal static readonly AssemblyName AssemblyName = typeof(Type2Metrics).Assembly.GetName();
    internal static readonly string MeterName = AssemblyName.Name!;

    private static readonly Meter MeterInstance = new(MeterName, AssemblyName.Version!.ToString());

    public Type2Metrics()
    {
        MeterInstance.CreateObservableUpDownCounter(
            "test.value",
            () => new Measurement<double>[]
            {
                new(DateTime.Now.Millisecond, new("type", "type2"), new("id", "x")),
                new(DateTime.Now.Microsecond, new("type", "type2"), new("id", "y")),
            });
    }
}
