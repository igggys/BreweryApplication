using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAppSendMessageService.DataAccess;
using WebAppSendMessageService.DataAccess.Interfaces;
using WebAppSendMessageService.DataAccess.Models;
using WebAppSendMessageService.DataAccess.Services;
using WLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//==================================================================================================================
builder.Services.AddSingleton<ISmsService, TurboSmsService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<SendMessageServiceSQLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddScoped<IMessageDbService, MessageMSSQLService>();

//adding logger to application
builder.Services.AddWLogger();
//==================================================================================================================
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
