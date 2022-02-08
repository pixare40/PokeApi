using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement.Descriptions.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApi.PokemonManagement.Descriptions
{
    public class DefaultDescriptionStrategy : IDescriptionStrategy
    {
        private IDictionary<string, IDescriptionService> descriptors;
        private readonly IHttpClientWrapper httpClientProvider;
        private readonly IOptions<PokemonApiConfigModel> pokemonApiConfig;

        public DefaultDescriptionStrategy(IHttpClientWrapper httpClientProvider,
            IOptions<PokemonApiConfigModel> pokemonApiConfig)
        {
            this.httpClientProvider = httpClientProvider;
            this.pokemonApiConfig = pokemonApiConfig;
        }

        public virtual IDictionary<string, IDescriptionService> InitialiseDescriptorMap()
        {
            descriptors = new Dictionary<string, IDescriptionService>();
            descriptors.Add("YODA", new YodaTranslator(httpClientProvider, pokemonApiConfig));
            descriptors.Add("SHAKESPEAR", new ShakespearTranslator(httpClientProvider, pokemonApiConfig));

            return descriptors;
        }

        public virtual string GetDescriptorKey(Pokemon pokemon)
        {
            if (pokemon.Habitat?.Name.ToLowerInvariant() == "cave" || pokemon.IsLegendary)
            {
                return "YODA";
            }
            else
            {
                return "SHAKESPEAR";
            }
        }
    }
}
