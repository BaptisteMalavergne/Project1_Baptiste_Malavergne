using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ProjectV2.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure DbContext
builder.Services.AddDbContext<MedicalSystemContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Adjust max depth if necessary
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical API", Version = "v1" });
});

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical API v1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.UseRouting();
app.MapRazorPages();

app.Run();