using WebAppTest.DataLayer;
using WebAppTest.Filters;
using WebAppTest.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using WebAppTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

builder.Services.AddMemoryCache();

builder.Services.Configure<ServiceProperties>(builder.Configuration.GetSection("ServiceProperties"));
builder.Services.Configure<List<ServiceProperties>>(builder.Configuration.GetSection("Services"));

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(LogingActionAttribute));
});

builder.Services.Configure<List<DbConnection>>(builder.Configuration.GetSection("DbConnections"));

builder.Services.AddSingleton<LogWriter>();
builder.Services.AddSingleton<SessionsManager>();
builder.Services.AddSingleton<Connector>();

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
