using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeApi.Infrastructure;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement;
using PokeApi.PokemonManagement.Descriptions;
using PokeApi.PokemonManagement.Descriptions.Descriptors;
using PokemonManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeApi
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
            services.AddHttpClient();
            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
            services.AddSingleton<YodaTranslator>();
            services.AddSingleton<ShakespearTranslator>();
            services.AddSingleton<IDescriptionStrategy, DefaultDescriptionStrategy>();
            services.AddSingleton<IDescriptionProviderFactory, DescriptionProviderFactory>();
            services.Configure<PokemonApiConfigModel>(Configuration
                .GetSection(PokemonApiConfigModel.PokemonApiConfig));
            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddMemoryCache();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
