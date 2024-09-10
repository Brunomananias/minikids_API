namespace API_Minikids.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }

    }
}
