using GestionIncidencias.DBContext;
using GestionIncidencias.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionIncidencias.Controllers
{
    public class ServicioIncidencias : BackgroundService
    {

        private readonly ILogger<ServicioIncidencias> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ServicioIncidencias(ILogger<ServicioIncidencias> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ServicioIncidencias Encendido");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                    var incidenciasAbiertas = await db.Incidencias.CountAsync(i => i.EstadoID == 1);

                    var incidenciasAbiertasList = await db.Incidencias.Where(i => i.EstadoID == 1).ToListAsync();

                    _logger.LogInformation($"Hay {incidenciasAbiertas} incidencias abiertas.");

                    incidenciasAbiertasList =  modificarIncidencias(incidenciasAbiertasList);

                    await db.SaveChangesAsync();

                }

                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    _logger.LogInformation("Tarea cancelada.");
                }
              
            }

            _logger.LogInformation("ServicioIncidencias parado");
        }

        private List<IncidenciaModel> modificarIncidencias(List<IncidenciaModel> incidenciasAbiertas)
        {
            DateTime ahora = DateTime.Now;

            foreach (var item in incidenciasAbiertas)
            {
                if (item.FechaCreacion.HasValue)
                {
                    var Totaldias = (DateTime.Now - item.FechaCreacion.Value).TotalDays;
                    if (Totaldias > 7)
                    {
                        //Se cambia el estado a "Cerrada"
                        item.EstadoID = 3;

                        _logger.LogInformation($"Incidencia {item} ha sido modificada a Cerrada");

                    }
                }
                
            }

            return incidenciasAbiertas;
        }

    }

}
