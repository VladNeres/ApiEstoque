namespace Domain.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoDoProduto { get; set; }
        public string Nome { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public bool Status { get; set; }
        public int CategoriaId { get; set; }
    }
}