using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;
using WLog;
using PhoneModel.Services;
using BreweryApplication.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWLogger();

builder.Services.AddPhonesService();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("uk"), new CultureInfo("en") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("uk"), new CultureInfo("en") };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseMiddleware<CultureSetterMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{language=uk}/{controller=Home}/{action=Index}");

app.Run();
