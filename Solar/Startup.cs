using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Vidyano.Service;
using Vidyano.Service.RavenDB;
using Solar.Service;

namespace Solar
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVidyanoRavenDB(Configuration, options =>
            {
                var settings = new DatabaseSettings();
                Configuration.Bind("Database", settings);
                if (settings.CertPath != null)
                    settings.CertPath = Path.Combine(Environment.ContentRootPath, settings.CertPath);

                var store = settings.CreateStore();
                options.Store = store;

                options.OnInitialized = () => IndexCreation.CreateIndexes(typeof(Startup).Assembly, store);
            });
            services.AddScoped<SolarContext>();
            services.AddTransient<RequestScopeProvider<SolarContext>>();

            services.AddHttpClient<FusionSolarApi>(client =>
            {
                client.BaseAddress = new("https://eu5.fusionsolar.huawei.com/thirdData/");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseVidyano(Environment, Configuration);
        }
    }
}
