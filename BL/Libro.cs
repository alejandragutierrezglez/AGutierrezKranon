using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Libro
    {
        public static ML.Libro GetAllEF(ML.Libro libro)
        {
            ML.Libro resLibro = new ML.Libro();
            libro.Autor = (libro.Autor == null) ? "" : libro.Autor;
            libro.Titulo = (libro.Titulo == null) ? "" : libro.Titulo;
            libro.FechaPublicacion = (libro.FechaPublicacion == null) ? "" : libro.FechaPublicacion;
            libro.Editorial = (libro.Editorial == null) ? "" : libro.Editorial;
            {
                try
                {
                    using (DL.AGutierrezKranonEntities context = new DL.AGutierrezKranonEntities())
                    {
                        var query = context.LibroGetAll(libro.Autor, libro.Titulo, libro.FechaPublicacion,libro.Editorial).ToList();

                        if (query != null)
                        {
                            resLibro.Objects = new List<object>();

                            foreach (var resultUsuario in query)
                            {
                                libro = new ML.Libro();
                                libro.Autor = resultUsuario.Autor;
                                libro.Titulo = resultUsuario.Titulo;
                                libro.FechaPublicacion = resultUsuario.FechaPublicacion.Value.ToString("dd/MM/yyyy");
                                libro.Editorial= resultUsuario.Editorial;

                                resLibro.Objects.Add(libro);
                            }
                            resLibro.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resLibro.Correct = false;
                    resLibro.Ex = ex;
                    resLibro.ErrorMessage = ex.Message;
                }
                return resLibro;
            }
        }

        public static ML.Libro AddEF(ML.Libro libro)
        {
            using (DL.AGutierrezKranonEntities context = new DL.AGutierrezKranonEntities())
            {
                ML.Libro resLibro = new ML.Libro();
                try
                {
                    var query = context.LibroAdd(libro.Autor, libro.Titulo, DateTime.Parse(libro.FechaPublicacion), libro.Editorial);

                    if (query > 0)
                    {
                        resLibro.Correct = true;
                    }
                    else
                    {
                        resLibro.Correct = false;
                    }
                }
                catch (Exception ex)
                {

                    resLibro.ErrorMessage = ex.Message;
                    resLibro.Ex = ex;
                    resLibro.Correct = false;
                }
                return resLibro;

            }
        }      

    }
}

