namespace Domain.Model
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public Guid CodigoProduto { get; set; }
        public string? ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}