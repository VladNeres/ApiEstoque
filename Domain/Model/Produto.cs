namespace Domain.Model
{
    public class Produto
    {
        public int Produto_ID { get; set; }
        public string Codigo_Produto { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}