using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class ProdutoRequestViewModel
    {
        [JsonPropertyName("codigo_do_produto")]
        public string CodigoDoProduto { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("quantidade_em_estoque")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
