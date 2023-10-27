using Domain.Mensagem;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccess.Repositories.Interface
{
    public interface IEstoqueRepository
    {
        Task<Paginacao<List<Produto>>> BuscarProdutos(int? paginaAtual, int? quantidadePorPagina);
        Task<Produto> BuscarProduto(string codigo);
        Task<Produto> VerificaSeExiste(string nome, string codigo);
        Task<int> AtualizarEstoque(string codigo,string nome, int reabastecer);
        Task<int> AtualizarProdutoNome(string codigoDoProduto, string nome);


    }
}
