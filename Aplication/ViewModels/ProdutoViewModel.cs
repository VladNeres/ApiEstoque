using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class ProdutoViewModel
    {
        
        [JsonPropertyName("CodigoProduto")]
        public Guid CodigoProduto { get; set; }
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }
        [JsonPropertyName("quantidade_em_estoque")]
        public int  Quantidade { get; set; }
        [JsonPropertyName("data_entrada")]
        public DateTime DataEntrada { get; set; }
        [JsonPropertyName("data_saida")]
        public DateTime DataSaida { get; set; }
        
       
    }
}
