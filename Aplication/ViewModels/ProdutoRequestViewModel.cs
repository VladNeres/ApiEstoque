using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class ProdutoRequestViewModel
    {
        public Guid CodigoProduto { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
}
