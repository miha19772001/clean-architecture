using Backend.API.DI;
using Backend.API.Filters;
using Backend.Infrastructure.PostgreSQL;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

//Config NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//Connect DB
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
                       ?? throw new InvalidOperationException("Connection string 'DbConnection' is not configured.");

DatabaseConfiguration.Connect(builder.Services, connectionString);

//Add collection extensions for DI
DIConfiguration.AddCollectionExtensions(builder.Services, builder.Configuration);

//Config CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        //.WithOrigins("http://localhost:4200");
    });
});

builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
        c.CustomSchemaIds(x => x.FullName);
    });
}

//Settings controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiErrorFilter>();
    options.Filters.Add<LoggerFilter>();
});

var app = builder.Build();

// Settings HTTP-container
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();

    DatabaseConfiguration.CreateSchema(app);
}

app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();