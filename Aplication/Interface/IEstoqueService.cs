using Aplication.ViewModels;
using Domain.Mensagem;


namespace Aplication.Interface
{
    public interface IEstoqueService
    {
        Task<MensagemBase<ProdutoRequestViewModel>> ReabastecerProduto(ProdutoRequestViewModel produto);
        Task<List<ProdutoRequestViewModel>> InserirProdutosPorCodigo(string caminho);
        Task<MensagemBase<Paginacao<List<ProdutoViewModel>>>> BuscarProdutos(int paginaAtual, int quantidadePorPagina);
        Task<MensagemBase<ProdutoViewModel>> BuscarProdutoPorCodigo(string codigo);
        Task<MensagemBase<ProdutoRequestViewModel>> AtualizarProdutoNome(string codigoDoPedido, string nome);


    }
}
