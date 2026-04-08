using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;

namespace StockPriceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider service, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(service);
            });


            builder.Services.AddHttpLogging();
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddScoped<IStocksService, StocksService>();
            builder.Services.AddScoped<IFinnhubService, FinnhubService>();
            builder.Services.AddScoped<IStocksRepository, StocksRepository>();
            builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
            builder.Services.AddHttpClient();

            if (builder.Environment.IsEnvironment("Test") == false)
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }

            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (builder.Environment.IsEnvironment("Test") == false)
            {
                Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
            }

            app.UseSerilogRequestLogging();
            app.UseHttpLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
