using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ViewModels
{
    public class ProdutoRequestViewModel
    {
        [JsonProperty("codigo_do_produto")]
        public string CodigoDoProduto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("quantidade_em_estoque")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
