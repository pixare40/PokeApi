using PokeApi.Models;
using System.Collections.Generic;

namespace PokeApi.PokemonManagement.Descriptions
{
    public interface IDescriptionStrategy
    {
        IDictionary<string, IDescriptionService> InitialiseDescriptorMap();

        string GetDescriptorKey(Pokemon pokemon);
    }
}