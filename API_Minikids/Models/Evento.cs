namespace API_Minikids.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public string Pacote { get; set; }
        public DateTime? HorarioFesta { get; set; }
        public TimeSpan TempoDeFesta { get; set; }
        public string Endereco { get; set; }
        public string? Observacoes { get; set; }
        public decimal? ValorTotalPacote { get; set; }

        // Chave estrangeira para Cliente
        public int? ClienteId { get; set; }
        public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}
