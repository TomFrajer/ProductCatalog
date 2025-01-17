using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Repositories;
using ProductCatalog.Tests.MockData.ProductApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Configure SQLite Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ProductCatalog.Infrastructure")));

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Product Catalog API",
        Description = "API documentation for Product Catalog API - Version 1"
    });
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v2",
        Title = "Product Catalog API v2",
        Description = "API documentation for Product Catalog API - Version 2 with pagination"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // Replace the placeholder with the actual version
    options.SubstituteApiVersionInUrl = true;
});

// UseMockData configuration
var useMock = builder.Configuration.GetValue<bool>("UseMockData");

if (useMock)
{
    builder.Services.AddScoped<IProductRepository, MockProductRepository>();
}
else
{
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
}

// Add application services
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Product Catalog API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
