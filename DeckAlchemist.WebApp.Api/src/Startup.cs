using DeckAlchemist.WebApp.Api.Managers;
using DeckAlchemist.WebApp.Api.Managers.Source;
using DeckAlchemist.WebApp.Api.Managers.Source.MtgJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeckAlchemist.WebApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            ConfigureDependencies(services);
        }

        private void ConfigureDependencies(IServiceCollection services) {
            services.AddTransient<ICardDatabaseSource, MtgJsonCardDatabaseSource>();
            services.AddSingleton<ICardDatabaseManager, CachingCardDatabaseManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
