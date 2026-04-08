using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StockPriceApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Moq;

namespace Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Test");

            builder.ConfigureServices(services => {
                var descripter = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descripter != null)
                {
                    services.Remove(descripter);
                }
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DatbaseForTesting");
                });

                // Mock the FinnhubRepository to avoid making real API calls
                var finnhubRepositoryDescriptor = services.SingleOrDefault(temp => temp.ServiceType == typeof(IFinnhubRepository));
                if (finnhubRepositoryDescriptor != null)
                {
                    services.Remove(finnhubRepositoryDescriptor);
                }

                var mockFinnhubRepository = new Mock<IFinnhubRepository>();

                // Mock GetCompanyProfile to return a dictionary with the expected keys
                mockFinnhubRepository
                    .Setup(x => x.GetCompanyProfile(It.IsAny<string>()))
                    .ReturnsAsync(new Dictionary<string, object>
                    {
                        { "ticker", "MSFT" },
                        { "name", "Microsoft Corporation" }
                    });

                // Mock GetStockPriceQuote to return a dictionary with the expected keys
                mockFinnhubRepository
                    .Setup(x => x.GetStockPriceQuote(It.IsAny<string>()))
                    .ReturnsAsync(new Dictionary<string, object>
                    {
                        { "c", 350.50 }
                    });

                services.AddScoped<IFinnhubRepository>(provider => mockFinnhubRepository.Object);
            });
        }
    }
}
