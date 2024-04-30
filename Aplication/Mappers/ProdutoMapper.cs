using Aplication.ViewModels;
using Domain.Model;


namespace Aplication.Mappers
{
    public static class ProdutoMapper
    {

        public static ProdutoViewModel ParaViewModel( this Produto produto) =>
            new ProdutoViewModel()
            {
                Nome = produto.ProdutoNome,                 
                Quantidade = produto.Quantidade,
                CodigoProduto = produto.CodigoProduto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };

        public static Produto ParaProduto(this ProdutoViewModel produto) =>
            new Produto()
            {
                ProdutoNome = produto?.Nome,
                Quantidade = produto.Quantidade,
                CodigoProduto = produto.CodigoProduto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };
    }
}
