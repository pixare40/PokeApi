using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement.Descriptions.Descriptors
{
    public abstract class FunTranslationsTranslator : DefaultDescriptor
    {
        private readonly IHttpClientProvider httpClientProvider;
        private readonly string apiEndpoint;

        protected abstract string TranslationType { get; }

        public FunTranslationsTranslator(IHttpClientProvider httpClientProvider, 
            IOptions<PokemonApiConfigModel> pokemonApiConfig)
        {
            this.httpClientProvider = httpClientProvider;
            apiEndpoint = pokemonApiConfig.Value.FunTranslationsEndpoint;
        }

        public async Task<DescriptionModel> GetDescriptionAsync(Pokemon pokemon, bool translated = false)
        {
            if (ShouldReturnEmptyDescription(pokemon))
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

        protected virtual async Task<DescriptionModel> TranslateAsync(string input)
        {
            string sanitised = SanitiseInputString(input);

            string requestUrl = string.Format("{0}{1}{2}{3}", apiEndpoint, TranslationType, ".json?text=", Uri.EscapeDataString(sanitised));

            try
            {
                TranslatedResponseModel response = await httpClientProvider.GetAsync<TranslatedResponseModel>(requestUrl);
                return new DescriptionModel { Success = true, Description = response.Contents.Translated };
            }
            catch (Exception e)
            {
                return new DescriptionModel { Success = false, Description = sanitised };
            }
        }

        private static bool ShouldReturnEmptyDescription(Pokemon pokemon)
        {
            return pokemon.FlavorTextEntries == null || pokemon.FlavorTextEntries.Length == 0;
        }

    }


    public class TranslatedResponseModel
    {
        public object Success { get; set; }

        public Contents Contents { get; set; }
    }

    public class Contents
    {
        public string Translated { get; set; }

        public string Text { get; set; }

        public string Translation { get; set; }
    }
}
