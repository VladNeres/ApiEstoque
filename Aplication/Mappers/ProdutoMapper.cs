using Aplication.ViewModels;
using Domain.Model;


namespace Aplication.Mappers
{
    public static class ProdutoMapper
    {

        public static ProdutoViewModel ParaViewModel(Produto produto) =>
            new ProdutoViewModel()
            {
                Nome = produto.ProdutoNome,                 
                Quantidade = produto.Quantidade,
                Codigo_Produto = produto.Codigo_Produto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };

        public static Produto ParaProduto(ProdutoViewModel produto) =>
            new Produto()
            {
                ProdutoNome = produto.Nome,
                Quantidade = produto.Quantidade,
                Codigo_Produto = produto.Codigo_Produto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };
    }
}
