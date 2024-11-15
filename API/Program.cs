using API;
using Ocelot.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information($"Starting application: {typeof(Program).Assembly.GetName().Name}");

try
{
    var builder = WebApplication.CreateBuilder(args);

    await builder
        .ConfigureServices()
        .Build()
        .ConfigurePipeline()
        .RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

