                            |==================| ARQUITETURA|==================|

|Transacao(Camada: Domain)|
{
    Entidade base. Tudo do sistema é uma transação [Model];
    Sem métodos pois o papel dela é ser um modelo da entidade e não tem que ter regras de negócio e manipulação.
}

|Receita e Despesas (Camada: Domain)|
{
    São espicializações de transação, ou seja, herdam tudo dela.

[Propriedades]
    Receita[
        Propriedade extra: string Fonte
    ]
    Despesa[
        Propriedade extra: string CategoriaEnum Categoria
    ]
}

|CategoriaEnum(Camada: Domain/Enums)|
{
    Enumeração que define os tipos possíveis de categorias de despesa.Útil para filtrar, agrupar, e evitar texto solto no código.
    [Exemplo]

    public enum Categoria {
        Alimentacao,
        Transporte,
        Lazer,
        Saúde,
        Educação,
        Outros
    }
}

|FinancasService(Camada: Application/Service)|
{
    Classe que controla a lógica do sistema, ou seja, a alma do FinMeha.
    *Responsavél por armazenar a lista de transações (List<Transacao>).
    *[Método de Operação]*
        - AdicionarTransacao(transacao t)
        - ListarTodasTransacoes()
        - CalcularSaldo()
        - BuscarPorMes(int mes, int ano)
        - CalcularTotalPorCategoria(Categoria cat)
        - FiltrarPorTipo(bool isReceita)

        /==| Deve usar LINQ, GroupBy, Where, Sum, etc... |==/
}

|DTOs(Camada: Application/DTOs)|
{
    Objeto usados para comunicação entre camadas (entrada e sáida de dados). Enviamos ess DTO para FinancasService ele cria a Transacao correta.
    [Exemplo]

    public class CriarTransacaoDTO
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool IsReceita { get; set; }
        public string? Fonte { get; set; }
        public Categoria? Categoria { get; set; }
    }
}

|Program(Camada Presentation)|
{
    Interação com o usuário(por enquanto via terminal)
    [Terá:]
        - Menu com opções:
            - Inserir transação
            - Listar todos
            - Buscar por mês
            - Calcular saldo
            - Sair

    Vai usar Console.ReadLine() para pegar valores e chamar métodos do FinancasService
}



________________________Base de Consulta:________________________
📁 Domain/
Tudo que representa as regras de negócio puras, sem saber se você vai usar API, Console ou Banco de Dados.

Transacao.cs: classe base com Descricao, Valor, Data

Receita.cs e Despesa.cs: herdam de Transacao

Categoria.cs: enum com categorias de despesa (Alimentação, Transporte...)

📁 Application/
Onde ficam as regras de uso. Aqui você organiza os métodos que manipulam os objetos da camada Domain.

FinancasService.cs: contém AdicionarTransacao, CalcularSaldo, etc.

DTOs/: dados de entrada para métodos (como se fossem formulários)

📁 Presentation/
O que interage com o mundo exterior.

No nosso caso inicial, será o Program.cs com um menu que o usuário interage via terminal.

Depois, pode virar um Controller em API, se desejar.

🚀 Ordem de implementação:
Criar Domain/Transacao, Receita, Despesa

Criar FinancasService em Application/Services/

Criar Program.cs que chama os métodos do serviço com entrada do usuário via console