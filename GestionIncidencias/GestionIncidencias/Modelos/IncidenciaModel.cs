using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionIncidencias.Models
{
    public class IncidenciaModel
    {
        [Key]public int IncidenciaID { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? EstadoID { get; set; }
        
        [ForeignKey("EstadoID")]public EstadoModel? Estado { get; set; }

        public int? EmpleadoID { get; set; }

        [ForeignKey("EmpleadoID")]public EmpleadoModel? Empleado { get; set; }

    }
}
