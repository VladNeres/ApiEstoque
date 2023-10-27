using Newtonsoft.Json;

namespace Aplication.ViewModels
{
    public class ProdutoViewModel
    {
        [JsonProperty("codigo_do_produto")]
        public string CodigoDoProduto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("quantidade_em_estoque")]
        public int QuantidadeEmEstoque { get; set; }
        [JsonProperty("data_entrada")]
        public DateTime DataEntrada { get; set; }
        [JsonProperty("data_said")]
        public DateTime DataSaida { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("categoria_id")]
        public int CategoriaId { get; set; }
    }
}
