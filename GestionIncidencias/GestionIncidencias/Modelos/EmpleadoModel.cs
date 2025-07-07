using System.ComponentModel.DataAnnotations;

namespace GestionIncidencias.Models
{
    public class EmpleadoModel
    {

        [Key]public int EmpleadoID { get; set; }

        public string? Nombre { get; set; }

        public string? Email { get; set; }
    }
}
