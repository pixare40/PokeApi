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
        private readonly IDescriptionStrategy descriptionStrategy;
        private IDictionary<string, IDescriptionService> descriptors;

        public DescriptionProviderFactory(IDescriptionStrategy descriptionStrategy)
        {
            this.descriptionStrategy = descriptionStrategy;
            descriptors = descriptionStrategy.InitialiseDescriptorMap();
        }

        public IDescriptionService GetDescriptor(Pokemon pokemon)
        {
            string key = descriptionStrategy.GetDescriptorKey(pokemon);
            return descriptors[key];
        }
    }
}
