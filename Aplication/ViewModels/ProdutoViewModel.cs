using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class ProdutoViewModel
    {
        
        [JsonPropertyName("CodigoProduto")]
        public Guid CodigoProduto { get; set; }
        [JsonPropertyName("Nome")]
        public string? Nome { get; set; }
        [JsonPropertyName("QuantidadeEmEstoque")]
        public int  Quantidade { get; set; }
        [JsonPropertyName("DataEntrada")]
        public DateTime DataEntrada { get; set; }
        [JsonPropertyName("DataSaida")]
        public DateTime DataSaida { get; set; }
        
       
    }
}
