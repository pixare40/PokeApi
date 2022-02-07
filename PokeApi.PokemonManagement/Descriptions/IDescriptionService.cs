using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement
{
    public interface IDescriptionService
    {
        public Task<string> GetDescriptionAsync(Pokemon pokemon);
    }
}
