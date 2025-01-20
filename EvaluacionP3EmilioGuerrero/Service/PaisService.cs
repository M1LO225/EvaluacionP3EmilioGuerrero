using EvaluacionP3EmilioGuerrero.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace EvaluacionP3EmilioGuerrero.Service
{
    internal class PaisService
    {
        private readonly HttpClient _httpClient;

        public PaisService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Pais>> GetPaisAsync()
        {
            var url = "https://restcountries.com/v3.1/name"; 
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<List<Pais>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
