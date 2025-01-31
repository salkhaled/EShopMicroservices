using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var assembly = typeof(Program).Assembly;
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddCarter();

        var connectionString = builder.Configuration.GetConnectionString("Database");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        builder.Services.AddMarten(config =>
        {
            config.Connection(connectionString);
        }).UseLightweightSessions();

        if (builder.Environment.IsDevelopment())
            builder.Services.InitializeMartenWith<CatalogInitialData>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddHealthChecks()
            .AddNpgSql(connectionString);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks("/health");

        app.Run();
    }
} 