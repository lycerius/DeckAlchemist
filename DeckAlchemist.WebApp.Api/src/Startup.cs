using DeckAlchemist.WebApp.Api.Managers;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Integration;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External.MtgJson;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeckAlchemist.WebApp.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();
            ConfigureDependencies (services);
        }

        private void ConfigureDependencies (IServiceCollection services) {
            services.AddTransient<IExternalCardDatabaseSource, MtgJsonCardDatabaseSource> ();
            services.AddTransient<ICardDatabaseManager, CardDatabaseManager> ();
            services.AddSingleton<ILocalCardDatabaseSource, InMemoryLocalCardDatabaseSource> ();
            services.AddSingleton<ICardDatabaseIntegrator, CardDatabaseIntegrator> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            var integrate = app.ApplicationServices.GetService<ICardDatabaseIntegrator> ();
            integrate.Integrate ();
            app.UseMvc ();
        }
    }
}