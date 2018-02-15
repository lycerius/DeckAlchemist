using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Objects.Cards.Mtg;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            services.AddTransient<IMTGCardSource, MongoMtgCardSource>();
            services.AddCors();
            ConfigureAuthentication(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
        }
    }
}
