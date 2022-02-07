using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApi.Models.ConfigModels
{
    public class PokemonApiConfigModel
    {
        public const string PokemonApiConfig = "PokemonApiConfig";

        public string PokeApiEndpoint { get; set; }

        public string FunTranslationsEndpoint { get; set; }
    }
}
