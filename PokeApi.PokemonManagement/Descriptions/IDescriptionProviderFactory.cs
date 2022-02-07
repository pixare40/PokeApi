using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApi.PokemonManagement
{
    public interface IDescriptionProviderFactory
    {
        IDescriptionService GetDescriptor(Pokemon pokemon);
    }
}
