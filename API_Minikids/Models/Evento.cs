namespace API_Minikids.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Pacote { get; set; }
        public TimeSpan TempoDeFesta { get; set; }
        public string Endereco { get; set; }

        // Chave estrangeira para Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
