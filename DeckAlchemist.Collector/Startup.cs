using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Collector.Objects.Cards;
using DeckAlchemist.Collector.Objects.Decks;
using DeckAlchemist.Collector.Services;
using DeckAlchemist.Collector.Sources.Cards.Mtg;
using DeckAlchemist.Collector.Sources.Cards.Mtg.External;
using DeckAlchemist.Collector.Sources.Cards.Mtg.Internal;
using DeckAlchemist.Collector.Sources.Decks.Mtg.External;
using DeckAlchemist.Collector.Sources.Decks.Mtg.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;

namespace DeckAlchemist.Collector
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
            AddSources(services);
            AddUpdateServices(services);
        }

        void AddSources(IServiceCollection services)
        {
            services.AddTransient<IMtgInternalCardSource, MongoMtgInternalCardSource>();
            services.AddTransient<IMtgExternalCardSource, MtgJsonExternalCardSource>();
            services.AddTransient<IMtgExternalDeckSource, MtgGoldFishExternalDeckSource>();
            services.AddTransient<IMtgInternalDeckSource, MongoMtgInternalDeckSource>();
        }

        void AddUpdateServices(IServiceCollection services)
        {
            services.AddSingleton<ICardDatabaseUpdater, CardDatabaseUpdater>();
            services.AddSingleton<IDeckDatabaseUpdater, DeckDatabaseUpdater>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            RegisterClassMaps();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            var cardServ = app.ApplicationServices.GetService<ICardDatabaseUpdater>();
            cardServ.UpdateCardDatabase();
            var serv = app.ApplicationServices.GetService<IDeckDatabaseUpdater>();
            serv.UpdateDecks();
        }

        void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<MtgLegality>(cm => {
                cm.AutoMap();
                cm.SetDiscriminator("MtgLegality");
            });

            BsonClassMap.RegisterClassMap<MtgDeckCard>(cm => {
                cm.AutoMap();
                cm.SetDiscriminator("MtgDeckCard");
            });
        }
    }
}
