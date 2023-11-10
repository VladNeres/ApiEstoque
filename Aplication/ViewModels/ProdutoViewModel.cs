using Newtonsoft.Json;

namespace Aplication.ViewModels
{
    public class ProdutoViewModel
    {
        
        [JsonProperty("codigo_do_produto")]
        public string Codigo_Produto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("quantidade_em_estoque")]
        public int Quantidade { get; set; }
        [JsonProperty("data_entrada")]
        public DateTime DataEntrada { get; set; }
        [JsonProperty("data_said")]
        public DateTime DataSaida { get; set; }
        
       
    }
}
