using GestionIncidencias.DBContext;
using GestionIncidencias.DTOs;
using GestionIncidencias.ModeloDatos;
using GestionIncidencias.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionIncidencias.Endpoints
{
    public static class GestorEndpoints
    {

        public static void MapIncidenciasEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/estatus", async (AppDBContext db) =>
            {
                try
                {
                    var puedeConectar = await db.Database.CanConnectAsync();
                    if (puedeConectar)
                        return Results.Ok("Conectado correctamente a la base de datos.");
                    else
                        return Results.Problem("No se pudo conectar a la base de datos.");
                }
                catch (Exception ex)
                {
                    return Results.Problem($" Error de conexión: {ex.Message}");
                }
            });

            app.MapGet("/api/estados", async (AppDBContext db) =>
            {

                var estados = await db.Estados.ToListAsync();
                return Results.Ok(estados);


            });

            app.MapGet("/api/empleados", async (AppDBContext db) =>
            {

                var empleados = await db.Empleados.ToListAsync();
                return Results.Ok(empleados);


            });

            app.MapGet("/api/listaIncidencias", async (AppDBContext db) =>
            {
                var incidencias = await db.Incidencias
                    .Include(i => i.Empleado)
                    .Include(i => i.Estado)
                    .ToListAsync();
                return Results.Ok(incidencias);


            });

            app.MapPost("/api/empleados/add", async (EmpleadoModel nuevoEmpleado, AppDBContext db) =>
            {

                db.Empleados.Add(nuevoEmpleado);
                await db.SaveChangesAsync();


                return Results.Ok(nuevoEmpleado);
            });

            app.MapPost("/api/incidencias", async (IncidenciaModel nuevaIncidencia, AppDBContext db) =>
            {
                // Asignamos la fecha actual si no viene en el JSON
                if (nuevaIncidencia.FechaCreacion.Equals(null))
                {
                    nuevaIncidencia.FechaCreacion = DateTime.Now;
                }

                if (nuevaIncidencia.EmpleadoID.Equals(null))
                {
                    nuevaIncidencia.EmpleadoID = 1;
                }

                // Guardamos la incidencia en la base de datos
                db.Incidencias.Add(nuevaIncidencia);
                await db.SaveChangesAsync();

                // Retornamos la incidencia creada con el ID generado
                return Results.Created($"/api/incidencias/{nuevaIncidencia.IncidenciaID}", nuevaIncidencia);
            });

            app.MapPut("/api/incidencias/{id}/fecha", async (int id, ModificarFecha fecha, AppDBContext db) =>
            {

                var incidencias = await db.Incidencias.FindAsync(id);

                if (incidencias == null)
                    return Results.NotFound("No se encontró la incidencia.");

                if (fecha.Fecha.HasValue)
                    incidencias.FechaCreacion = fecha.Fecha;

                await db.SaveChangesAsync();

                return Results.Ok(incidencias);
            });

            app.MapPut("/api/incidencias/{id}/asignar", async (int id, AsignarIncidencia idEmpleado, AppDBContext db) =>
            {

                var incidencias = await db.Incidencias.FindAsync(id);

                if (incidencias == null)
                    return Results.NotFound("No se encontró la incidencia.");

                if (idEmpleado.EmpleadoID.HasValue)
                    incidencias.EmpleadoID = idEmpleado.EmpleadoID;

                await db.SaveChangesAsync();

                return Results.Ok(incidencias);
            });

            app.MapGet("/api/incidencias", async (string? estado, AppDBContext db) =>
            {
                var estados = await db.Estados.ToListAsync();
                if (estados.Any(e => e.Nombre == estado))
                {
                    var estadoFiltrado = await db.Estados.FirstOrDefaultAsync(e => e.Nombre == estado);

                    var incidenciasFiltradas = await db.Incidencias.Where(i => i.EstadoID == estadoFiltrado.EstadoID).Include(i => i.Empleado).ToListAsync();

                    await db.SaveChangesAsync();

                    return Results.Ok(incidenciasFiltradas);
                }
                else
                {
                    return Results.Ok("Estado no encontrado");
                }

            });
        }
    }
}
