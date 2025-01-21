using EvaluacionP3EmilioGuerrero.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Repositories
{
    public interface IPaisRepository
    {
        Task<int> AddPaisAsync(Pais pais);
        Task<List<Pais>> GetAllPaisesAsync();
        Task<int> DeletePaisAsync(int id);
        Task<int> UpdatePaisAsync(Pais pais);
    }
}
