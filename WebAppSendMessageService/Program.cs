using WebAppSendMessageService.BLL;
using WebAppSendMessageService.Interfaces;
using WebAppSendMessageService.Models;
using WLog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//==================================================================================================================
builder.Services.AddSingleton<ISmsService, TurboSmsService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMessageProvider, EmailMessageProvider>();

//adding logger to application
builder.Services.AddWLogger();
//==================================================================================================================
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
