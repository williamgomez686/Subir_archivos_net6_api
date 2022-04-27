using System.ComponentModel.DataAnnotations;

namespace Subir_archivos.Models
{
    public class Archivo
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? extension { get; set; }
        public double? tamanio  { get; set; }
        public string? ubicacion { get; set; }
    }
}
