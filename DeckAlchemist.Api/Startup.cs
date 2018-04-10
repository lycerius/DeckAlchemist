using DeckAlchemist.Api.Utility;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Deck.Mtg;
using DeckAlchemist.Api.Sources.Group;
using DeckAlchemist.Api.Sources.Messages;
using DeckAlchemist.Api.Sources.User;
using DeckAlchemist.Support.Objects.Cards;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.Decks;
using DeckAlchemist.Api.Sources.UserDeck;

namespace DeckAlchemist.Api
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
            services.AddCors();
            ConfigureAuthentication(services);
            ConfigureSources(services);
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Deck Alchemist Web Api", Version = "v1" });
            });
        }

        public void ConfigureSources(IServiceCollection services)
        {
            services.AddTransient<IMtgCardSource, MongoMtgCardSource>();
            services.AddTransient<IMtgDeckSource, MongoMtgDeckSource>();
            services.AddTransient<ICollectionSource, MongoCollectionSource>();
            services.AddTransient<IGroupSource, MongoGroupSource>();
            services.AddTransient<IUserSource, MongoUserSource>();
            services.AddTransient<IMtgCardSource, MongoMtgCardSource>();
            services.AddTransient<IMtgDeckSource, MongoMtgDeckSource>();
            services.AddSingleton<IAuthorizationHandler, EmailVerificationHandler>();
            services.AddTransient<IMessageSource, MongoMessageSource>();
            services.AddTransient<IUserDeckSource, MongoUserDeckSource>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RegisterClassMaps();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseCors(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

        }

        void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<MtgLegality>(cm => {
                cm.AutoMap();
                cm.SetDiscriminator("MtgLegality");
            });
            BsonClassMap.RegisterClassMap<UserMessage>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("UserMessage");
            });
            BsonClassMap.RegisterClassMap<LoanRequestMessage>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("LoanRequestMessage");
            });
            BsonClassMap.RegisterClassMap<GroupInviteMessage>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("GroupInviteMessage");
            });
            BsonClassMap.RegisterClassMap<BorrowedCard>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("BorrowedCard");
            });
            BsonClassMap.RegisterClassMap<OwnedCard>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("OwnedCard");
            });
            BsonClassMap.RegisterClassMap<MtgDeckCard>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("MtgDeckCard");
            });
            BsonClassMap.RegisterClassMap<MtgDeck>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("MtgDeck");
            });

        }

        void ConfigureAuthentication(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/deckalchemist";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/deckalchemist",
                        ValidateAudience = true,
                        ValidAudience = "deckalchemist",
                        ValidateLifetime = true
                    };
                    options.SaveToken = true;
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Email", policy => {
                    policy.AddRequirements(new EmailVerificationRequirement());
                });
            });
            
        }
    }
}
