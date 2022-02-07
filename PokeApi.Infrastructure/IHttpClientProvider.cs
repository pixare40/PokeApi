using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Infrastructure
{
    public interface IHttpClientProvider
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}
