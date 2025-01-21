using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.Modelos
{
    internal class Pais
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NombreOficial { get; set; }
        public string Region { get; set; }
        public string GoogleMapsLink { get; set; }
        public string NombreBD { get; set; }
    }
}
