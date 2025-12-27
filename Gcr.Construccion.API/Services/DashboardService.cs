using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync( DateTime? fromDate, DateTime? toDate )
        {
            // Normalización de fechas 
            var hasFrom = fromDate.HasValue && fromDate.Value > DateTime.MinValue;
            var hasTo = toDate.HasValue && toDate.Value > DateTime.MinValue;

            // Ingresos
            var ingresosQuery = _context.Ingresos.AsQueryable();
            if (hasFrom) ingresosQuery = ingresosQuery.Where(i => i.Fecha >= fromDate!.Value);
            if (hasTo) ingresosQuery = ingresosQuery.Where(i => i.Fecha <= toDate!.Value);

            var totalIngresos = await ingresosQuery.SumAsync(i => i.Monto);

            // Compras de material
            var comprasQuery = _context.Compras.AsQueryable();
            if (hasFrom) comprasQuery = comprasQuery.Where(c => c.FechaCompra >= fromDate!.Value);

            if (hasTo) comprasQuery = comprasQuery.Where(c => c.FechaCompra <= toDate!.Value);


            var totalCompras = await comprasQuery.SumAsync(c => c.MontoTotal);

            // Pagos de empleados (DbSet = PagosEmpleados)
            var pagosQuery = _context.PagosEmpleados.AsQueryable();
            if (hasFrom) pagosQuery = pagosQuery.Where(p => p.FechaPago >= fromDate!.Value);

            if (hasTo) pagosQuery = pagosQuery.Where(p => p.FechaPago <= toDate!.Value);


            var totalPagos = await pagosQuery.SumAsync(p => p.TotalPagado);

            return new DashboardSummaryDto
            {
                TotalIngresos = totalIngresos,
                TotalComprasMaterial = totalCompras,
                TotalPagosEmpleados = totalPagos,
                CapitalDisponible = totalIngresos - totalCompras - totalPagos,
                FromDate = hasFrom ? fromDate : null,
                ToDate = hasTo ? toDate : null
            };
        }
    }
}
