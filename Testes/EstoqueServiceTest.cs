using Aplication.Interface;
using Aplication.Service;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SqlDataAccess.Repositories.Interface;
using Xunit;

namespace Testes
{
    public class EstoqueServiceTest
    {
        private readonly EstoqueService _service;
        private readonly IEstoqueRepository _repository;

        public EstoqueServiceTest()
        {
            _repository = Substitute.For<IEstoqueRepository>();
            _service = new EstoqueService(_repository);
        }

        [Fact]
        public async Task BuscarProduto_ProdutoNaoEncontrado_Retornar422()
        {
            //ARRAGNGE
            _repository.BuscarProduto(Arg.Any<Guid>()).ReturnsNull();

            //ACT
            var result = await _service.BuscarProduto(Guid.NewGuid());

            //ASSER
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, result.StatusCodes);

        }

        [Fact]
        public async Task BuscarProduto_ProdutoEncontrado_Retornar422()
        {
            //ARRAGNGE
            Produto produto = new Produto(Guid.NewGuid(),"Teste", 40, DateTime.Now,DateTime.Now);
           
            _repository.BuscarProduto(Arg.Any<Guid>()).Returns(produto);

            //ACT
            var result = await _service.BuscarProduto(Guid.NewGuid());

            //ASSER
            Assert.Equal(StatusCodes.Status200OK, result.StatusCodes);

        }

        public async Task BuscarProdutos_ListaVazia_Retornar400() { }
        public async Task BuscarProdutos_ListaComProdutosValidos_Retornar400(){}
        public async Task ReabastecerProduto_ProdutoNaoExistir_Retornar400(){}
        public async Task ReabastecerProduto_ProdutoExistir_Retornar200(){}
        public async Task InserirProdutosPorLista_ListaVazia_Retornar400(){}
        public async Task InserirProdutosPorLista_CodigoInvalido_Retornar400(){}
        public async Task AtualizarProdutoParcial_ListaCorreta_Retornar200(){}
        public async Task AtualizarProdutoParcial_CodigoNaoExiste_Retorna400(){}
        public async Task AtualizarProdutoParcial_ProdutoComNomeRepetido_Retorna400(){}
        public async Task AtualizarProdutoParcial_ProdutoComNome_Retorna200(){}



    }
}
