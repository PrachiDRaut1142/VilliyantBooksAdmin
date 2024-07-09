using Freshlo.Repository;
using Freshlo.RI;
using Freshlo.Services;
using Freshlo.SI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freshlo.Web.Services
{
    public static class ServiceConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISalesRI, SalesRepository>();
            services.AddScoped<IEmployeeRI, EmployeeRepository>();
            services.AddScoped<IItemRI, ItemRepository>();
            services.AddScoped<IWastageRI, WastageRepository>();
            services.AddScoped<ICustomerRI, CustomerRepository>();
            services.AddScoped<INotificationRI, NotificationRepository>();
            services.AddScoped<DropDownRI, DropDownRepository>();
            services.AddScoped<IFinancialRI, FinancialRepository>();
            services.AddScoped<ISystemConfigRI, SystemConfigRepository>();
            services.AddScoped<IPurchaseRI, PurchaseRepository>();
            services.AddScoped<DashboardRI, DashboardRepository>();
            services.AddScoped<IPricelistRI, PricelistRepository>();
            services.AddScoped<ISettingRI, SettingRepository>();
            services.AddScoped<IStockRI, StockRepository>();
            services.AddScoped<IOfferRI, LiveOfferRepository>();
            services.AddScoped<IVendorRI, VendorRepository>();
            services.AddScoped<BannerRI, BannerRepository>();
            services.AddScoped<ICategoriesRI, CategoriesRepository>();
            services.AddScoped<ICoupenRI, CoupenRepository>();
            services.AddScoped<IHubRI, HubRepository>();
            services.AddScoped<InventoryRI, InventoryRepository>();
            services.AddScoped<SaleSummaryRI, SaleSummaryRepository>();



            services.AddScoped<ISalesSI, SalesServices>();
            services.AddScoped<IEmployeeSI, EmployeeServices>();
            services.AddScoped<IItemSI, ItemService>();
            services.AddScoped<IWastageSI, WastageServices>();
            services.AddScoped<ICustomerSI, CustomerService>();
            services.AddScoped<INotificationSI, NotificationService>();
            services.AddScoped<DropDownSI, DropDownServices>();
            services.AddScoped<IFinancialSI, FinancialServices>();
            services.AddScoped<ISystemConfigSI, SystemConfigServices>();
            services.AddScoped<IPurchaseSI, PurchaseServices>();
            services.AddScoped<DashboardSI, DashboardService>();
            services.AddScoped<IPricelistSI, PricelistService>();
            services.AddScoped<ISettingSI, SettingService>();
            services.AddScoped<IStockSI, StockService>();
            services.AddScoped<IOfferlist, LiveOfferService>();
            services.AddScoped<IVendorSI, VendorService>();
            services.AddScoped<BannerSI, BannerService>();
            services.AddScoped<ICategoriesSI, CategoriesService>();
            services.AddScoped<ICoupenSI, CoupenService>();
            services.AddScoped<IHubSI, HubService>();
            services.AddScoped<Inventory, InventoryService>();
            services.AddScoped<SaleSummarySI, SaleSummaryService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/Error";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "Freshlo.SharedCookie";
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });
        }

        public static void AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("NoCache", new CacheProfile()
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
                options.Filters.Add(new ResponseCacheAttribute
                {
                    CacheProfileName = "Nocache"
                });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddCookieTempDataProvider();
        }

        public static void AddDbConfig_Development(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConfig, DbConfig>((ctx) =>
            {
                return new DbConfig(configuration.GetConnectionString("DefaultConnection"), configuration.GetSection("BusinessInfo").GetSection("businessId").Value);
            });

        
        }

        public static void ConfigureCookiePolicyOptions(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
        }

        public static void ConfigureCookieTempDataProvider(this IServiceCollection services)
        {
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });
        }
    }
}
