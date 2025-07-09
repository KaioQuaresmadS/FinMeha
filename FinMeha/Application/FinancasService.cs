namespace FinancasService
{
    public class FinancasService
    {
        private List<Transacao> transacoes = new();

        public void AdicionarTransacao(transacao t)
        {
            transacoes.Add(t);
        }

        public void ListarTodasTransacoes()
        {
            foreach (var t in transacoes)
            {
                Console.WriteLine($"{t.Data:dd / MM / yyyy} {t.Descricao} - R${t.Valor}");
            }
        }

        public void CalcularSaldo()
        {
            decimal saldo = 0;
            foreach (var t in transacoes)
            {
                saldo += t.Valor;
            }
            return saldo;
        }

        public void BuscarPorMes(int mes, int ano)
        {
            var filter = transacoes
                                    .Where(d => d.Data.Month == mes && d.Data.Year == ano)
                                    .ToList();
        }

        public Dictionary<int, num> CalcularTotalPorCategoria(Categoria cat)
        {
            var calc = transacoes
                                .GroupBy(d => Categoria)
                                .ToDictionary(grupo => grupo.Key, grupo => grupo.ToList())
                                .Sum();
        }

        public void FiltrarPorTipo(bool isReceita)
        {
            var filtro = transacoes
                                    .Where(d => d.Fonte == isReceita)
                                    .ToList();
        }

    }
}