using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models.ConfigModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement.Descriptions.Descriptors
{
    public abstract class FunTranslationsTranslator
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

        protected async Task<string> TranslateAsync(string input)
        {
            string sanitised = Regex.Replace(input, "[^0-9A-Za-z ,'.]", " ");

            string requestUrl = string.Format("{0}{1}{2}{3}", apiEndpoint, TranslationType, ".json?text=", Uri.EscapeDataString(sanitised));

            try
            {
                TranslatedResponseModel response = await httpClientProvider.GetAsync<TranslatedResponseModel>(requestUrl);
                return response.Contents.Translation;
            }
            catch(Exception e)
            {
                return sanitised;
            }
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
