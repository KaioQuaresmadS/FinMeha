using FinMeha.Domain;
using FinMeha.Enums;

namespace FinMeha.Application
{
    public class FinancasService
    {
        private List<Transacao> transacoes = new();

        private readonly List<Despesa> despesas = new();

        private readonly List<Receita> receita = new();


        // 1 - Adicionar Receita: Insere uma nova receita nas listas de receitas e transações
        public void AdicionarReceita(Receita r)
        {
            receita.Add(r);
            transacoes.Add(r);
        }

        // 2 - Adicionar Despesa: Insere uma nova despesa nas listas de despesas e transações
        public void AdicionarDespesa(Despesa d)
        {
            despesas.Add(d);
            transacoes.Add(d);
        }

        // 3 - Adicionar Transação: Insere uma transação genérica na lista de transações
        public void AdicionarTransacao(Transacao t)
        {
            transacoes.Add(t);
        }

        // 4 - Listar Todas as Transações: Imprime e retorna todas as transações registradas
        public List<Transacao> ListarTodasTransacoes()
        {
            foreach (var t in transacoes)
            {
                Console.WriteLine($"{t.Data:dd/MM/yyyy} {t.Descricao} - R${t.Valor}");
            }
            return transacoes;
        }

        // 5 - Calcular Saldo: Soma todos os valores das transações para obter o saldo total
        public decimal CalcularSaldo()
        {
            decimal totalDespesas = transacoes.OfType<Despesa>().Sum(d => d.Valor);
            decimal totalReceita = transacoes.OfType<Receita>().Sum(r => r.Valor);
            return totalReceita - totalDespesas;
        }

        // 6 - Buscar Por Mês: Filtra transações por mês e ano informados
        public List<Transacao> BuscarPorMes(int mes, int ano)
        {
            return transacoes
                                .Where(d => d.Data.Month == mes && d.Data.Year == ano)
                                .ToList();
        }

        // 7 - Calcular Total Por Categoria: Soma o valor das despesas de uma categoria específica
        public decimal CalcularTotalPorCategoria(CategoriaEnum categoria)
        {
            return transacoes
                            .OfType<Despesa>() //Pegar só despesas
                            .Where(d => d.Categoria == categoria)
                            .Sum(d => d.Valor);
        }

        // 8 - Filtrar Por Tipo: Retorna todas as receitas ou despesas conforme o parâmetro
        public List<Transacao> FiltrarPorTipo(bool isReceita) // Funciona como se fosse um if
        {
            return isReceita
                            ? transacoes.OfType<Receita>().Cast<Transacao>().ToList() // ? se for true | Cast agrupa tudo para sair homogeneo os dados de Receita ou despesas
                            : transacoes.OfType<Despesa>().Cast<Transacao>().ToList(); // : se for false
        }

        // 9 - Somar Despesas Lazer Ordenadas Por Data: Soma despesas de lazer ordenadas por data
        public decimal SomarDespesasLazerOrdenadasPorData()
        {
            return transacoes
                            .OfType<Despesa>()
                            .Where(d => d.Categoria == CategoriaEnum.Lazer)
                            .OrderBy(d => d.Data)
                            .Sum(d => d.Valor);
        }

        // 10 - Top 3 Transportes Ano Atual: Retorna as 3 maiores despesas de transporte do ano atual
        public List<string> Top3TransportesAnoAtual()
        {
            int ano = DateTime.Now.Year;
            return transacoes
                            .OfType<Despesa>()
                            .Where(d => d.Categoria == CategoriaEnum.Transporte && d.Data.Year == ano)
                            .OrderByDescending(d => d.Valor)
                            .Take(3)
                            .Select(d => $"R${d.Valor:F2} - {d.Data:dd/MM/yyyy}")
                            .ToList();
        }

        // 11 - Agrupar Receitas Por Fonte: Agrupa receitas por fonte e retorna em um dicionário
        public Dictionary<string, List<Receita>> AgruparReceitasPorFonte()
        {
            return transacoes
                            .OfType<Receita>()
                            .GroupBy(r => r.Fonte)
                            .ToDictionary(grupo => grupo.Key, grupo => grupo.ToList());
        }

        // 11 - Agrupar Despesas Por Categoria: Agrupa despesas por categoria e retorna em um dicionário
        public Dictionary<CategoriaEnum, List<Despesa>> AgruparDespesasPorCategoria()
        {
            return transacoes
                            .OfType<Despesa>()
                            .GroupBy(d => d.Categoria)
                            .ToDictionary(grupo => grupo.Key, grupo => grupo.ToList());


        }

        // 12 - Listar Despesas Altas: Retorna despesas acima de R$200 ordenadas por valor
        public List<string> ListarDespesasAltas()
        {
            return despesas
                        .OfType<Despesa>()
                        .Where(d => d.Valor > 200m)
                        .OrderByDescending(d => d.Valor)
                        .Select(d => $"{d.Data: dd/MM/yyyy} - R${d.Valor:F2}")
                        .ToList();
        }

        // 13 - 
    }
}