using Domain.Mensagem;
using Domain.Model;


namespace SqlDataAccess.Repositories.Interface
{
    public interface IEstoqueRepository
    {
        Task<Paginacao<List<Produto>>> BuscarProdutos(int? paginaAtual, int? quantidadePorPagina);
        Task<Produto> BuscarProduto(Guid? codigo);
        Task<Produto> VerificaSeExiste(string? nome, Guid? codigo);
        Task<int> AtualizarEstoque(Guid codigo, string nome, int reabastecer);
        Task<int> AtualizarProdutoParcial( Guid codigoDoProduto, string nome);
        Task<int> AtualizarProduto(Guid codigo, string nome, int reabastecer);
        Task<int> CadastrarProdutoNoEstoque(Produto produto);
    }
}
