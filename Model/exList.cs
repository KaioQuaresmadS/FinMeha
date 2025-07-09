namespace ListEx
{
    public class Despesa
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; } = 0;
        public DateTime Data { get; set; } = DateTime.Now;
        public string Categoria { get; set; } = string.Empty;
    }

    public class FinancasService
    {
        private List<Despesa> despesas = new();

        public void AdicionarDespesa(string descricao, decimal valor, DateTime data)
        {
            despesas.Add(new Despesa // Criado nova lista que será gerada e armazenada. Será criado despesa1,despesa2 ou vai ficar separado como um Array de objetos que tem as listas e dados separados?
            {
                Descricao = descricao,
                Valor = valor,
                Data = data
            });
        }


        public Dictionary<string, List<Despesa>> AgruparPorCategoria()
        {
            return despesas
                            .Where.GroupBy(d => Categoria)
                            .ToDictionary(grupo => grupo.Key, grupo => grupo.ToList()); //Agrupa todas as despesas por categoria, e me dá uma lista de despesas para cada uma.
        }

        public Dictionary<int, decimal> TotalMensalPorAno(int ano) {
            public 
        }

        public List<Despesas> FiltrarPorValorMinimo()
    }
}