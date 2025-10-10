using Backend.API.DI;
using Backend.API.Filters;
using Backend.Infrastructure.PostgreSQL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

//Config NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//Connect DB
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
                       ?? throw new InvalidOperationException("Connection string 'DbConnection' is not configured.");

DatabaseConfiguration.Connect(builder.Services, connectionString);

//Add collection extensions for DI
DiConfiguration.AddCollectionExtensions(builder.Services);

var domain = builder.Configuration.GetSection("Domain").Value
             ?? throw new InvalidOperationException("Section 'Domain' is not configured.");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyCookie";
        options.Cookie.Domain = domain;
        options.Cookie.HttpOnly = true;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });
builder.Services.AddAuthorization();

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

app.MapGet("api/getData", () => "Hallo Mann!");

app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();