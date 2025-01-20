using EvaluacionP3EmilioGuerrero.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EvaluacionP3EmilioGuerrero.Repositories
{
    internal class PaisesRepository
    {
        string _dbPath;


        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Pais>();
        }

        public PaisesRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void agregarPais(string name, string region)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                if (string.IsNullOrEmpty(region))
                    throw new Exception("Valid description required");


                result = conn.Insert(new Pais { Nombre = name, Region = region});

                StatusMessage = string.Format("{0} record(s) added (Nombre: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<Pais> GetAllPais()
        {
            try
            {
                Init();
                return conn.Table<Pais>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Pais>();
        }
        public void EliminarPais(string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");
                var person = conn.Table<Modelos.Pais>().FirstOrDefault(p => p.Nombre == name);
                result = conn.Delete(person);

                StatusMessage = string.Format("{0} record(s) deleted (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0}. Error: {1}", name, ex.Message);
            }
        }

    }
}

