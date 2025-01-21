using EvaluacionP3EmilioGuerrero.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<PaisApi> GetPaisAsync(string nombre)
        {
            var url = $"https://restcountries.com/v3.1/name/{nombre}?fields=name,region,maps";
            var response = await _httpClient.GetStringAsync(url);
            var paises = JsonSerializer.Deserialize<List<PaisApi>>(response);
            return paises?.FirstOrDefault();
        }
    }
}
