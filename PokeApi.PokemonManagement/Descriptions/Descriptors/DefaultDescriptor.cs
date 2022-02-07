using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PokeApi.PokemonManagement.Descriptions.Descriptors
{
    public class DefaultDescriptor
    {

        protected static string SanitiseInputString(string input)
        {
            return Regex.Replace(input, "[^0-9A-Za-z ,'.]", " ");
        }

        protected virtual DescriptionModel DefaultDescription(Pokemon pokemon)
        {
            return new DescriptionModel { Success = true, Description = SanitiseInputString(pokemon.FlavorTextEntries[0].FlavourText) };
        }
    }
}
