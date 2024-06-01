using Aplication.ViewModels;
using Domain.Mensagem;


namespace Aplication.Interface
{
    public interface IEstoqueService
    {
        Task<MensagemBase<ProdutoRequestViewModel>> ReabastecerProduto(ProdutoRequestViewModel produto);
        Task<MensagemBase<List<ProdutoRequestViewModel>>> InserirProdutosPorLista(string caminho);
        Task<MensagemBase<Paginacao<List<ProdutoViewModel>>>> BuscarProdutos(int paginaAtual, int quantidadePorPagina);
        Task<MensagemBase<ProdutoViewModel>> BuscarProduto(Guid codigo);
        Task<MensagemBase<ProdutoRequestViewModel>> AtualizarProdutoParcial(Guid codigoDoPedido, string nome);
        Task<MensagemBase<ProdutoRequestViewModel>> AtualizarProduto(ProdutoRequestViewModel produto);
    }
}
