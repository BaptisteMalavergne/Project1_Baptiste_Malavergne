
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ProjectV2.Mapping;



//using ProjectV2.Models.Mapping;

var builder = WebApplication.CreateBuilder(args);

//Mapping à retirer si ça marche pas 
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<MedicalSystemContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.MaxDepth = 64; // Si nécessaire, ajustez la profondeur maximale
        });
/*
// MVC + API
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Si nécessaire, ajustez la profondeur maximale
    });
*/

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical API", Version = "v1" });
});

builder.Services.AddRazorPages();


var app = builder.Build();
/*
// Cette ligne permet à l'application de servir des vues Razor
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});*/

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
app.UseAuthorization();
app.MapRazorPages();


app.Run();

