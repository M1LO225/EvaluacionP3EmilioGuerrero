using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Modelos
{
    internal class BuscaPais
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Region { get; set; }
        public string Mapa {  get; set; }
    }
}
