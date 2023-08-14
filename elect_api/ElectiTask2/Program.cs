using Microsoft.EntityFrameworkCore;
using ElectiTask.Models;
using Microsoft.OpenApi.Models;

// Get the logger instance for the current class
var logger = NLog.LogManager.GetCurrentClassLogger();

try
{
    // Create a new web application builder
    var builder = WebApplication.CreateBuilder(args);

    // Configure Entity Framework Core DbContext for BookContext using SQL Server
    //builder.Services.AddDbContext<BookContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection")));
    //builder.Services.AddDbContext<LogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection")));
    //builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")));
    builder.Services.AddDbContext<BookContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("BookConnection")));
    builder.Services.AddDbContext<LogContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("LogConnection")));
    builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("UserConnection")));

    // Add controllers to the service container
    builder.Services.AddControllers();

    // Configure Cross-Origin Resource Sharing (CORS) policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("corsapp", builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Allow requests from this origin
                   .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
                   .AllowAnyHeader() // Allow any HTTP headers
                   .AllowCredentials(); // Allow credentials like cookies, authorization headers, etc.
        });
    });

    // Configure Swagger/OpenAPI documentation generation
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v2", new OpenApiInfo { Title = "MVCCallWebAPI", Version = "v2" });
    });

    // Build the web application
    var app = builder.Build();

    // Check if the application is running in the development environment
    if (app.Environment.IsDevelopment())
    {
        // Enable Swagger UI for API documentation
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "MVCCallWebAPI");
        });
    }

    // Use CORS policy defined earlier
    app.UseCors("corsapp");
    // Use authorization middleware (if applicable)
    app.UseAuthorization();
    // Map controllers and endpoints
    app.MapControllers();
    // Start the application
    app.Run();
}
catch (Exception ex)
{
    // Log any exception that occurred during application startup
    logger.Error(ex, "An error occurred during application startup");

    // Rethrow the exception to propagate it further
    throw;
}
finally
{
    // Shutdown NLog and release resources
    NLog.LogManager.Shutdown();
}

public partial class Program { }

