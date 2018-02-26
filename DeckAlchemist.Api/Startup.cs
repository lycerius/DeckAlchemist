﻿using DeckAlchemist.Api.Auth;
using DeckAlchemist.Api.Objects.Card.Mtg;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Deck.Mtg;
using DeckAlchemist.Api.Sources.Group;
using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;

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
            services.AddMvc();

            services.AddCors();
            ConfigureAuthentication(services);
            ConfigureSources(services);
        }

        public void ConfigureSources(IServiceCollection services)
        {
            services.AddTransient<IMtgCardSource, MongoMtgCardSource>();
            services.AddTransient<IMtgDeckSource, MongoMtgDeckSource>();
            services.AddTransient<ICollectionSource, MongoCollectionSource>();
            services.AddTransient<IGroupSource, MongoGroupSource>();
            services.AddTransient<IUserSource, MongoUserSource>();
            services.AddSingleton<IAuthorizationHandler, EmailVerificationHandler>();
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

            app.UseMvc();

        }

        void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<MtgLegality>(cm => {
                cm.AutoMap();
                cm.SetDiscriminator("MtgLegality");
            });
            /* Uncomment when MtgDeckCard is Added
            BsonClassMap.RegisterClassMap<MtgDeckCard>(cm => {
                cm.AutoMap();
                cm.SetDiscriminator("MtgDeckCard");
            });
            */
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
