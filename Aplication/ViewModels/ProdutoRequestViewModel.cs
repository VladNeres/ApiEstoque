using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class ProdutoRequestViewModel
    {
        [JsonPropertyName("codigoDoProduto")]
        public string CodigoDoProduto { get; set; }
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }
        [JsonPropertyName("quantidade_em_estoque")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
