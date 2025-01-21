using EvaluacionP3EmilioGuerrero.Modelos;
using EvaluacionP3EmilioGuerrero.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly BDService _databaseService;

        public PaisRepository()
        {
            _databaseService = new BDService();
        }

        public Task<int> AddPaisAsync(Pais pais)
        {
            return _databaseService.SavePaisAsync(pais);
        }

        public Task<List<Pais>> GetAllPaisesAsync()
        {
            return _databaseService.GetPaisesAsync();
        }

        public async Task<int> DeletePaisAsync(int id)
        {
            var pais = await _databaseService.GetPaisesAsync();
            var paisToDelete = pais.Find(p => p.Id == id);
            return await _databaseService.DeletePaisAsync(paisToDelete);
        }

        public Task<int> UpdatePaisAsync(Pais pais)
        {
            return _databaseService.UpdatePaisAsync(pais);
        }
    }
}

