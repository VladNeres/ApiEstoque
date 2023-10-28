using Dapper;
using Domain.Mensagem;
using Domain.Model;
using SqlDataAccess.Repositories.Interface;
using System.Data;

using static Dapper.SqlMapper;

namespace SqlDataAccess.Repositories
{
    public class EstoqueRepository : BaseRepository, IEstoqueRepository
    {
        public EstoqueRepository(string connectionString) : base(connectionString)
        {

        }


        public async Task<Paginacao<List<Produto>>> BuscarProdutos(int? paginaAtual, int? quantidadePorPagina)
        {

            int maximoPorPaginas = 50;
            quantidadePorPagina = quantidadePorPagina < maximoPorPaginas ? quantidadePorPagina : maximoPorPaginas;

            int? skip = (paginaAtual - 1) * quantidadePorPagina;
            int? take = quantidadePorPagina;

            DynamicParameters param = new DynamicParameters();
            param.Add("@Skip", skip, DbType.Int32);
            param.Add("@Take", take, DbType.Int32);
            

            var query = @"Select
                          Count(*)
                           From Produtos

                         SELECT 
                                    NOME,
                                    VALOR,
                                    STATUS,
                                    DATACRIACAO,
                                    DATAALTERACAO,
                                    CategoriaID,
                                    CodigoDoProduto
                              FROM Produtos
                               ORDER BY ID
                               OFFSET @Skip ROWS FETCH NEXT @Take ROWS Only";
            if(skip == 0 && take == 0) 
            {
                query = query.Replace(" ORDER BY ID\r\n                               OFFSET @Skip ROWS FETCH NEXT @Take ROWS Only", " ");
            }

            return await MultipleQueryAsync<Paginacao<List<Produto>>>(query, async (GridReader reader) => 
            {
                int quantidadeTotal = reader.Read<int>().FirstOrDefault();
                List<Produto> produtos = reader.Read<Produto>().ToList();

                return new Paginacao<List<Produto>>(quantidadeTotal, produtos,paginaAtual,quantidadePorPagina);
            
            },param, CommandType.Text) ;
           
        }

        public async Task<Produto> BuscarProduto(string codigo)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CodigoDoProduto", codigo, DbType.AnsiString);

            string query = @"Select 
                                  Nome,Valor, Status,DataCriacao,DataAlteracao,QuantidadeEmEstoque,CategoriaID
                             From Produtos
                             WHERE CodigoDoProduto = @CodigoDoProduto";
            return await QueryFirstOrDefaultAsync<Produto>(query, param, CommandType.Text);
        }

        public async Task<Produto> VerificaSeExiste(string nome, string codigo)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Nome", nome, DbType.AnsiString);
            param.Add("@Codigo", codigo, DbType.AnsiString);

            string query = @"SELECT NOME, CodigoDoProduto
                            FROM Produtos 
                            WHERE NOME = @Nome OR CodigoDoProduto = @Codigo";

            return await QueryFirstOrDefaultAsync<Produto>(query, param, CommandType.Text);

        } 
        public async Task<int> AtualizarEstoque(string codigo,string nome, int reabastecer)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Codigo", codigo, DbType.AnsiString);
            param.Add("@Nome", nome, DbType.AnsiString);
            param.Add("@Quantidade", reabastecer, DbType.Int32);

            string query = @"UPDATE Produtos 
                             SET QuantidadeEmEstoque = QuantidadeEmEstoque + @Quantidade 
                            WHERE CodigoDoProduto = @Codigo OR Nome = @Nome";

            return await ExecuteAsync(query,param,commandType: CommandType.Text);
        }

        public async Task<int> AtualizarProdutoNome( string codigoDoProduto, string nome)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Codigo", codigoDoProduto, DbType.String);
            param.Add("@Nome", nome, DbType.AnsiString);
            string query = @"UPDATE Estoque 
                             SET ProdutoNome = @Nome
                            WHERE CodigoDoProduto = @Codigo";

            return await ExecuteAsync(query, param, commandType: CommandType.Text);
        }
    }
}
