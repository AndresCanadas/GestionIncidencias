using GestionIncidencias.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionIncidencias.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
       : base(options)
        {
        }

        public DbSet<IncidenciaModel> Incidencias { get; set; }

        public DbSet<EmpleadoModel> Empleados { get; set; }

        public DbSet<EstadoModel> Estados { get; set; }
    }
}
