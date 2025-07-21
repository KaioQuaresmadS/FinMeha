using FinMeha.Enums;

namespace FinMeha.Domain
{
    public class Despesa : Transacao
    {
        public CategoriaEnum Categoria { get; set; }
    }
}