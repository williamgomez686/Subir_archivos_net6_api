using Microsoft.AspNetCore.Mvc;
using Subir_archivos.Context;
using Subir_archivos.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Subir_archivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class archivosController : ControllerBase
    {
        //inicialisamos el context
        public readonly DbContext _context;

        public archivosController(DbContext context)
        {
                _context = context;
        }
        // GET: api/<archivosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.archivos.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/<archivosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<archivosController>
        [HttpPost]
        public IActionResult PostArchivo([FromForm] List<IFormFile> files)
        {
            List<Archivo> oArchivos = new List<Archivo>();

            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        //subiendo archivo a carpeta espesifica
                        var filepath = "C:\\SubirArchivos\\" + file.FileName;
                        
                        using(var stream = System.IO.File.Create(filepath))
                        {
                            file.CopyToAsync(stream);
                        }
                        //Agregando informacion a la base de datos

                        double tamanio = file.Length;
                        tamanio = tamanio / 1048576;
                        tamanio = Math.Round(tamanio, 2);//redondeamos a 2 decimales
                        Archivo archivo = new Archivo();
                        archivo.extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.nombre = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.tamanio = tamanio;
                        archivo.ubicacion = filepath;
                        oArchivos.Add(archivo);
                    }
                    _context.archivos.AddRange(oArchivos);
                    _context.SaveChanges();
                } 
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(oArchivos);
        }

        // PUT api/<archivosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<archivosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
