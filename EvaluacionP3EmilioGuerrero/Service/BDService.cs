using EvaluacionP3EmilioGuerrero.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Service
{
    public class BDService
    {
        private readonly SQLiteAsyncConnection _database;

        public BDService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "EGuerrero.db3");
            _database = new SQLiteAsyncConnection(dbPath);


            _database.CreateTableAsync<Pais>().Wait();
        }

        public Task<int> SavePaisAsync(Pais pais)
        {
            return _database.InsertAsync(pais);
        }

        public Task<List<Pais>> GetPaisesAsync()
        {
            return _database.Table<Pais>().ToListAsync();
        }

        public Task<int> DeletePaisAsync(Pais pais)
        {
            return _database.DeleteAsync(pais);
        }

        public Task<int> UpdatePaisAsync(Pais pais)
        {
            return _database.UpdateAsync(pais);
        }
    }
}
