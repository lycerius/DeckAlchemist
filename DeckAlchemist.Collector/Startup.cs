using System;
using DeckAlchemist.Collector.Schedulers;
using DeckAlchemist.Collector.Services;
using DeckAlchemist.Collector.Sources.Cards.Mtg;
using DeckAlchemist.Collector.Sources.Cards.Mtg.External;
using DeckAlchemist.Collector.Sources.Cards.Mtg.Internal;
using DeckAlchemist.Collector.Sources.Decks.Mtg.External;
using DeckAlchemist.Collector.Sources.Decks.Mtg.Internal;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Decks;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<ICardDatabaseServiceScheduler, CardDatabaseServiceScheduler>();
            services.AddSingleton<IDeckDatabaseServiceScheduler, DeckDatabaseServiceScheduler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RegisterClassMaps();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseMvc();
            StartUpdateSchedulers(app.ApplicationServices);
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

        void StartUpdateSchedulers(IServiceProvider services)
        {
            var cardScheduler = services.GetService<ICardDatabaseServiceScheduler>();
            var deckScheduler = services.GetService<IDeckDatabaseServiceScheduler>();

            cardScheduler.Start();
            deckScheduler.Start();
        }

    }
}
