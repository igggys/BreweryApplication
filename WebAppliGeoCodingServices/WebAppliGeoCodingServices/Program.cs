using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Reflection.Metadata;
using WebAppGeoCodingServices.DataLayer;
using WebAppGeoCodingServices.Filters;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Services.GeoCoding;

//var AllowSpecificOrigins = "_AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(GeoCodingActionFilterAttribute));
    options.Filters.Add(typeof(GeoCodingExceptionFilterAttribute));
    //options.Filters.Add(typeof(GeoCodingResultFilterAttribute));
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: AllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("*")
//                          .AllowAnyHeader()
//                          .AllowAnyMethod()
//                          .WithExposedHeaders("sessionId")
//                          .WithExposedHeaders("serviceId");
//                      });
//});

builder.Services.Configure<List<GeoCodingServiceSettings>>(builder.Configuration.GetSection("GeoCodingServiceSettings"));
builder.Services.Configure<List<DbConnection>>(builder.Configuration.GetSection("DbConnections"));
builder.Services.Configure<ServiceProperties>(builder.Configuration.GetSection("ServiceProperties"));
builder.Services.Configure<List<ServiceProperties>>(builder.Configuration.GetSection("Services"));

List<GeoCodingServiceSettings> GeoServicesSettings = builder.Configuration.GetSection("GeoCodingServiceSettings").Get<List<GeoCodingServiceSettings>>();
builder.Services.AddHttpClient("TomTom", httpClient =>
{
    httpClient.BaseAddress = new Uri(GeoServicesSettings.FirstOrDefault(item => item.ServiceName == "TomTom").BaseUrl);
});
builder.Services.AddSingleton<IServiceGeoCoding, WebAppGeoCodingServices.Services.GeoCoding.TomTom.Service>();
builder.Services.AddHttpClient("Here", httpClient =>
{
    httpClient.BaseAddress = new Uri(GeoServicesSettings.FirstOrDefault(item => item.ServiceName == "Here").BaseUrl);
});
builder.Services.AddSingleton<IServiceGeoCoding, WebAppGeoCodingServices.Services.GeoCoding.Here.Service>();
builder.Services.AddHttpClient("GoogleMaps", httpClient =>
{
    httpClient.BaseAddress = new Uri(GeoServicesSettings.FirstOrDefault(item => item.ServiceName == "GoogleMaps").BaseUrl);
});
builder.Services.AddSingleton<IServiceGeoCoding, WebAppGeoCodingServices.Services.GeoCoding.GoogleMaps.Service>();

builder.Services.AddSingleton<IServiceGeoCoding, WebAppGeoCodingServices.Services.GeoCoding.Here.Service>();
builder.Services.AddHttpClient("BingMaps", httpClient =>
{
    httpClient.BaseAddress = new Uri(GeoServicesSettings.FirstOrDefault(item => item.ServiceName == "BingMaps").BaseUrl);
});
builder.Services.AddSingleton<IServiceGeoCoding, WebAppGeoCodingServices.Services.GeoCoding.BingMaps.Service>();


builder.Services.AddSingleton<LogWriter>();
builder.Services.AddSingleton<SessionsManager>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
