namespace Gcr.Construccion.API.DTOs
{
    public class DashboardSummaryDto
    {
        public decimal TotalIngresos { get; set; }
        public decimal TotalComprasMaterial { get; set; }
        public decimal TotalPagosEmpleados { get; set; }
        public decimal CapitalDisponible { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
