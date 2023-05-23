using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libro
    {
        public int? IdLibro { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public string FechaPublicacion { get; set; }
        public string AñoPublicacion { get; set; }
        public string Editorial { get; set; }
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Ex { get; set; }
        public object ObjLibro { get; set; }
        public List<object> Objects { get; set; }
        public List<object> Libros { get; set; }
    }
}
