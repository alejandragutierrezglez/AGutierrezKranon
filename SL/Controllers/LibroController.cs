using BL;
using DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace SL.Controllers
{
    public class LibroController : ApiController
    {
        // GET: Libro
        [HttpGet]
        [Route("api/Libro/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Libro libro = new ML.Libro();
            libro.Autor = (libro.Autor == null) ? "" : libro.Autor;
            libro.Titulo = (libro.Titulo == null) ? "" : libro.Titulo;
            libro.FechaPublicacion = (libro.FechaPublicacion == null) ? "" : libro.FechaPublicacion;
            libro.Editorial = (libro.Editorial == null) ? "" : libro.Editorial;

            ML.Libro resLibro = BL.Libro.GetAllEF(libro);
            if (resLibro.Correct)
            {
                return Ok(resLibro);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Libro/Add")]
        public IHttpActionResult Add([FromBody] ML.Libro libro)
        {
            ML.Libro resLibro = BL.Libro.AddEF(libro);
            if (resLibro.Correct)
            {
                return Ok(resLibro);
            }
            else
            {
                return NotFound();
            }
        }
    }
}