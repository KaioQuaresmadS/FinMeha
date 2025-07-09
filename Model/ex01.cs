namespace Usuario;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public decimal Saldo { get; private set; } = string.Empty;

    public adicionarReceita(decimal valor)
    {
        Saldo += valor;
    }

    public adicionarDespesa(decimal valor)
    {
        Saldo -= valor;
    }

    public abstract class Transacao
    {
        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }
        public string Categoria { get; private set; }

    }

    public class Receita : Transacao
    {
        public string Receita { get; set; }
    }

    public class Despesa : Transacao
    {
        public retirar(Transacao transacao);
    }

    public class Relatorio {
        public static void MotivoExtrato(List<Transacao> transacao)
        {
            foreach (var t in transacao)
            {
                Console.WriteLine($"{t.Data:dd/mm/aaaa} - {t.Categoria} - R${t.Valor:F2}");
                }
        }
    }
};