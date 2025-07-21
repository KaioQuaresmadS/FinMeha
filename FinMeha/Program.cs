using System;
using FinMeha.Domain;
using FinMeha.Enums;
using FinMeha.Application;
using System.Net;
using System.Runtime.CompilerServices;

class Program
{
    static FinancasService financas = new();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== MENU FINMEHA ====");
            Console.WriteLine("1 - Adicionar Receita");
            Console.WriteLine("2 - Adicionar despesas");
            Console.WriteLine("3 - Listar todas as transações");
            Console.WriteLine("4 - Calcular saldo total");
            Console.WriteLine("5 - Top 3 gastos com transporte (Ano Atual)");
            Console.WriteLine("6 - Somar lazer por mês");
            Console.WriteLine("7 - Agrupar receitas por fonte");
            Console.WriteLine("8 - Listar despesas altas (R$200+)");
            Console.WriteLine("9 - Agrupar despesas por categoria");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("Escolha a opção: ");

            var opcoes = Console.ReadLine();

            switch (opcoes)
            {
                case "1":
                    AdicionarReceita(); // 1 - Adicionar Receita
                    break;
                case "2":
                    AdicionarDespesa(); // 2 - Adicionar Despesa
                    break;
                case "3":
                    ListarTodasTransacoes(); // 3 - Listar Todas as Transações
                    break;
                case "4":
                    CalcularSaldo(); // 4 - Calcular Saldo Total
                    break;
                case "5":
                    Top3Transportes(); // 5 - Top 3 Gastos com Transporte (Ano Atual)
                    break;
                case "6":
                    SomarLazerPorMes(); // 6 - Somar Lazer por Mês
                    break;
                case "7":
                    AgruparReceitaFonte(); // 7 - Agrupar Receitas por Fonte
                    break;
                case "8":
                    ListarDespesasAltas(); // 8 - Listar Despesas Altas (R$200+)
                    break;

                case "9":
                    AgruparDespesaCategoria();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção ínvalida.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para contunuar...");
            Console.ReadKey();
        }
    }

    // 1 - Adicionar Receita: Solicita dados ao usuário e adiciona uma receita
    static void AdicionarReceita()
    {
        Console.Write("Descricao: ");
        string descricao = Console.ReadLine() ?? "";

        if (string.IsNullOrEmpty(descricao))
        {
            Console.WriteLine("Descrição invalída!");
            return;
        }

        Console.Write("Valor: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.Write("Valor incorreto!");
            return;
        }

        Console.Write("Data: (dd/MM/yyyy) ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
        {
            Console.Write("Data incorreta!");
            return;
        }

        Console.Write("Fonte: ");
        string fonte = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(fonte))
        {
            Console.Write("Invalído");
            return;
        }

        var receitas = new Receita
        {
            Descricao = descricao,
            Valor = valor,
            Data = data,
            Fonte = fonte
        };

        financas.AdicionarReceita(receitas);
        Console.WriteLine("Receita adicionada com sucesso!");
    }

    // 2 - Adicionar Despesa: Solicita dados ao usuário e adiciona uma despesa
    static void AdicionarDespesa()
    {
        Console.Write("Descrição: ");
        string descricao = Console.ReadLine() ?? "";

        if (string.IsNullOrEmpty(descricao))
        {
            Console.WriteLine("Inválido, tente novamente!");
        }

        Console.Write("Valor: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor inválido: ");
            return;
        }

        Console.Write("Data: (dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
        {
            Console.WriteLine("Data inválida: ");
            return;
        }

        Console.Write("Categoria: ");
        foreach (var c in Enum.GetValues(typeof(CategoriaEnum)))
        {
            Console.WriteLine($"- {c}");
        }

        Console.Write("Escolha: ");
        if (!Enum.TryParse(Console.ReadLine(), true, out CategoriaEnum categoria))
        {
            Console.WriteLine("Categoria inválida.");
            return;
        }

        var despesa = new Despesa
        {
            Descricao = descricao,
            Valor = valor,
            Data = data,
            Categoria = categoria
        };

        financas.AdicionarDespesa(despesa);
        Console.WriteLine("Despesa adicionada com sucesso!");
    }

    // 3 - Listar Todas as Transações: Exibe todas as transações registradas
    static void ListarTodasTransacoes()
    {
        financas.ListarTodasTransacoes();
    }

    // 4 - Calcular Saldo Total: Exibe o saldo total calculado
    static void CalcularSaldo()
    {
        decimal saldo = financas.CalcularSaldo();
        Console.WriteLine($"Saldo total: R${saldo:F2}");
    }

    // 5 - Top 3 Gastos com Transporte (Ano Atual): Exibe as 3 maiores despesas de transporte do ano atual
    static void Top3Transportes()
    {
        foreach (var transporte in financas.Top3TransportesAnoAtual())
        {
            Console.Write(transporte);
        }
    }

    // 6 - Somar Lazer por Mês: Exibe o total de gastos com lazer
    static void SomarLazerPorMes()
    {
        decimal lazer = financas.SomarDespesasLazerOrdenadasPorData();
        Console.WriteLine($"O total de gastos com lazer é: {lazer:F2}");
    }

    // 7 - Agrupar Receitas por Fonte: Exibe receitas agrupadas por fonte
    static void AgruparReceitaFonte()
    {
        var agrupado = financas.AgruparReceitasPorFonte();
        foreach (var grupo in agrupado)
        {
            Console.WriteLine($"Fonte: {grupo.Key}");
            foreach (var receita in grupo.Value)
            {
                Console.WriteLine($"\t{receita.Data:dd/MM/yyyy} - {receita.Descricao} - R${receita.Valor:F2}");
            }
        }
    }

    // 8 - Listar Despesas Altas (R$200+): Exibe despesas acima de R$200
    static void ListarDespesasAltas()
    {
        foreach (var alto in financas.ListarDespesasAltas())
        {
            Console.WriteLine(alto);
        }
    }

    // 9 - Agrupar Despesas por Categoria: Exibe despesas agrupadas por fonte

    static void AgruparDespesaCategoria()
    {
        var agrupado = financas.AgruparDespesasPorCategoria();
        foreach (var grupo in agrupado)
        {
            Console.WriteLine($"Categoria: {grupo.Key}");
            foreach (var despesa in grupo.Value)
            {
                Console.WriteLine($"\t{despesa.Data:dd/MM/yyyy} - {despesa.Descricao} - R${despesa.Valor:F2} - {despesa.Categoria}");
            }
        }
    } 
}