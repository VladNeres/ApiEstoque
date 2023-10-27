using Aplication.ViewModels;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Mappers
{
    public static class ProdutoMapper
    {

        public static ProdutoViewModel ParaViewModel(Produto produto) =>
            new ProdutoViewModel()
            {
                CategoriaId = produto.CategoriaId,
                Nome = produto.Nome,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                Status = produto.Status,
                CodigoDoProduto = produto.CodigoDoProduto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };

        public static Produto ParaProduto(ProdutoViewModel produto) =>
            new Produto()
            {
                CategoriaId = produto.CategoriaId,
                Nome = produto.Nome,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                Status = produto.Status,
                CodigoDoProduto = produto.CodigoDoProduto,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };
    }
}
