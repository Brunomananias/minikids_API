using System.ComponentModel.DataAnnotations;

namespace API_Minikids.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }

        // Coleção de eventos associados ao cliente
        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}

