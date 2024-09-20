using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Models
{
    public class Caixa
    {
        public int Id { get; set; } 
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Origem { get; set; }  
        public string Destino { get; set; } 
        public bool Entrada { get; set; }
        public bool Saida { get; set; }
        public decimal SaldoTotal { get; set; }

        public static decimal CalcularSaldoTotal(IEnumerable<Caixa> caixas, Caixa novoCaixa)
        {
            var saldoAtual = caixas.Sum(c => c.Entrada ? c.Valor : -c.Valor);

            // Atualiza o saldo total com base na nova entrada ou saída
            if (novoCaixa.Entrada)
            {
                saldoAtual += novoCaixa.Valor; // Se for entrada, adiciona ao saldo
            }
            else if (novoCaixa.Saida)
            {
                saldoAtual -= novoCaixa.Valor; // Se for saída, subtrai do saldo
            }

            return saldoAtual;
        }
    }
}
