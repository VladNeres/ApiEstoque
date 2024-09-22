namespace Domain.Model
{
    public class Produto
    {
        public string? ProdutoNome { get; set; } = string.Empty;
        public Guid CodigoProduto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public Produto(Guid codigoProduto, string? produtoNome, int quantidade, DateTime dataEntrada, DateTime dataSaida)
        {
            CodigoProduto = codigoProduto;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
        }

        public Produto(){}
        public DateTime DataSaida { get; set; }
    }
}