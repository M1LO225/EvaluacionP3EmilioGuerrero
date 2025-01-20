using EvaluacionP3EmilioGuerrero.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Repositories
{
    internal class BuscarPaisRepository
    {
        string _dbPath;


        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<BuscaPais>();
        }

        public BuscarPaisRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void agregarBuscaPais(string nombre, string region)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Valid name required");

                if (string.IsNullOrEmpty(region))
                    throw new Exception("Valid description required");

                result = conn.Insert(new BuscaPais { Nombre = nombre, Region = region });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", nombre, ex.Message);
            }

        }

        public List<BuscaPais> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<BuscaPais>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<BuscaPais>();
        }
        public void EliminarPersona(string nombre)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Valid name required");
                var person = conn.Table<Modelos.BuscaPais>().FirstOrDefault(p => p.Nombre == nombre);
                result = conn.Delete(person);

                StatusMessage = string.Format("{0} record(s) deleted (Nombre: {1})", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0}. Error: {1}", nombre, ex.Message);
            }
        }
    }
}

