namespace Catalog.FunctionalTests.Services
{
    internal class CatalogWebHostFactory : WebApplicationFactory<Program>
    {
        public static string DefaultUserId { get; set; } = Guid.NewGuid().ToString();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var path = Assembly.GetAssembly(typeof(CatalogWebHostFactory))?.Location;

            builder
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json", false);
                })
                .ConfigureHostConfiguration(configuration =>
                {
                    configuration.SetBasePath(Directory.GetCurrentDirectory());
                    configuration.AddJsonFile("hostsettings.json", false);
                })
                .ConfigureServices((HostBuilderContext hostContext, IServiceCollection services) =>
                {
                    // Remove the app's ApplicationDbContext registration.
                    var optionsDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CatalogContext>));

                    if (optionsDescriptor != null)
                    {
                        services.Remove(optionsDescriptor);
                    }

                    var contextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(CatalogContext));

                    if (contextDescriptor != null)
                    {
                        services.Remove(contextDescriptor);
                    }

                    var connectionString = hostContext.Configuration.GetConnectionString("TestDb");

                    var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
                    optionsBuilder.UseNpgsql(connectionString);

                    services.AddScoped<DbContextOptions<CatalogContext>>((sp) => optionsBuilder.Options);
                    services.AddDbContext<CatalogContext, CatalogTestContext>();
                });

            Environment.SetEnvironmentVariable("JWT_SECURITYKEY", "SomethingSecret!");

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.Configure<TestAuthHandlerOptions>(options => options.UserId = DefaultUserId);

                services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                    .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
            });
        }
    }
}
