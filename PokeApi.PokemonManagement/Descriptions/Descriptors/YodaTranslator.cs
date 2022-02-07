using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement.Descriptions.Descriptors
{
    public class YodaTranslator : FunTranslationsTranslator, IDescriptionService
    {
        public YodaTranslator(IHttpClientProvider httpClientProvider, IOptions<PokemonApiConfigModel> pokemonApiConfig)
            : base(httpClientProvider, pokemonApiConfig)
        {
        }

        protected override string TranslationType => "yoda";

  
        public async Task<string> GetDescriptionAsync(Pokemon pokemon)
        {
            if (pokemon.FlavorTextEntries.Length == 0)
            {
                return string.Empty;
            }

            string defaultDescription = pokemon.FlavorTextEntries[0].FlavourText;

            return await TranslateAsync(defaultDescription);
        }
    }
}
