﻿using Microsoft.Extensions.Options;
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
        public YodaTranslator(IHttpClientWrapper httpClientProvider, IOptions<PokemonApiConfigModel> pokemonApiConfig)
            : base(httpClientProvider, pokemonApiConfig)
        {
        }

        protected override string TranslationType => "yoda";
    }
}
