using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement.Descriptions.Descriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokeApi.PokemonManagement.Descriptions
{
    public class DescriptionProviderFactory : IDescriptionProviderFactory
    {
        private IDictionary<string, IDescriptionService> descriptors;

        public DescriptionProviderFactory(IHttpClientProvider httpClientProvider, IOptions<PokemonApiConfigModel> pokemonApiConfig)
        {
            descriptors = new Dictionary<string, IDescriptionService>();
            descriptors.Add("YODA", new YodaTranslator(httpClientProvider, pokemonApiConfig));
            descriptors.Add("SHAKESPEAR", new ShakespearTranslator(httpClientProvider, pokemonApiConfig));
        }

        public IDescriptionService GetDescriptor(Pokemon pokemon)
        {
            if(pokemon.Habitat?.Name.ToLowerInvariant() == "cave" || pokemon.IsLegendary)
            {
                return descriptors["YODA"];
            }
            else
            {
                return descriptors["SHAKESPEAR"];
            }
        }
    }
}
