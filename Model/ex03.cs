namespace ex03
{
    public class Transacao
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }

    public class Despesa : Transacao
    {
        public string Categoria { get; set; }
    }

    public class Receita : Transacao
    {
        public string Fonte { get; set; }
    }

    public class FinancasService
    {
        private List<Transacao> transacoes = new();

        public void AdicionarTransacao(Transacao t)
        {
            transacoes.Add(t);
        }

        public void ListarTodasTransacoes()
        {
            foreach (var t in transacoes)
            {
                Console.WriteLine($"[{t.Data:dd/MM/yyyy}] {t.Descricao} - R${t.Valor}");
            }
        }

        public decimal CalcularSaldo()
        {
            var saldo = 0;

            foreach (var t in transacoes)
            {
                saldo += t.Valor;
            }

            return saldo;
        }

        public List<Transacao> BuscarPorMes(int mes, int ano)
        {
            return transacoes
                            .Where(d => d.Data.Month == mes && d.Data.Year == ano)
                            .ToList();
        }
    }
}