using System.ComponentModel.DataAnnotations;

namespace GestionIncidencias.Models
{
    public class EstadoModel
    {
        [Key]public int EstadoID { get; set; }

        public string? Nombre { get; set; }

    }
}
