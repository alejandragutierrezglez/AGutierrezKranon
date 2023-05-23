using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Text;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Libro libro = new ML.Libro();

            ML.Libro libroObjects = new ML.Libro();
            libroObjects.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string str = System.Configuration.ConfigurationManager.AppSettings["WebApi"];
                    client.BaseAddress = new Uri(str);

                    var responseTask = client.GetAsync("Libro/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Libro>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Libro resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(resultItem.ToString());
                            libroObjects.Objects.Add(resultItemList);
                        }
                    }
                    libro.Libros = libroObjects.Objects;
                }

            }
            catch (Exception ex)
            {
                libroObjects.Correct = false;
                libroObjects.ErrorMessage = ex.Message;
                libroObjects.Ex = ex;

            }

            return View(libro);
        }

        [HttpPost]

        public ActionResult GetAll(ML.Libro libro)
        {           
            ML.Libro result = BL.Libro.GetAllEF(libro);

            if (result.Correct)

            {
                libro.Libros = result.Objects;

                return View(libro);
            }
            else
            {
                return View(libro);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdLibro)
        {
            ML.Libro libro = new ML.Libro();
            ML.Libro resLibro = BL.Libro.GetAllEF(libro);
           

            if (resLibro.Correct)
            {
                libro.Libros = resLibro.Objects;
            }
            if (IdLibro == null)
            {
                return View(libro);
            }
            else
            {
                return View(libro);
            }
        }
        [HttpPost] 
        public ActionResult Form(ML.Libro libro)
        {            
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var postTask = client.PostAsJsonAsync<ML.Libro>("Libro/Add", libro);
                    postTask.Wait();

                    var resLibro = postTask.Result;
                    if (resLibro.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha agregado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha agregado el registro";
                        return PartialView("Modal");
                    }
                }
            }
        

    }
}