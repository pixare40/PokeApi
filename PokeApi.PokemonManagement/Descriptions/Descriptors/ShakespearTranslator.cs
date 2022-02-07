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
    public class ShakespearTranslator : FunTranslationsTranslator, IDescriptionService
    {
        public ShakespearTranslator(IHttpClientProvider httpClientProvider, IOptions<PokemonApiConfigModel> pokemonApiConfig)
            :base(httpClientProvider, pokemonApiConfig)
        {
        }

        protected override string TranslationType => "shakespeare";

        public async Task<DescriptionModel> GetDescriptionAsync(Pokemon pokemon, bool translated = false)
        {
            if (pokemon.FlavorTextEntries.Length == 0)
            {
                return new DescriptionModel { Success = true, Description = string.Empty };
            }

            if (!translated)
            {
                return DefaultDescription(pokemon);
            }

            string defaultDescription = pokemon.FlavorTextEntries[0].FlavourText;

            return await TranslateAsync(defaultDescription);
        }
    }

}
