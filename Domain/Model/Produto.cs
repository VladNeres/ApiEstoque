namespace Domain.Model
{
    public class Produto
    {
        public Produto(Guid codigoProduto, string? produtoNome, int quantidade, DateTime dataEntrada, DateTime dataSaida)
        {
            CodigoProduto = codigoProduto;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
        }

        public Produto(){}
        public Guid CodigoProduto { get; set; }
        public string? ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}