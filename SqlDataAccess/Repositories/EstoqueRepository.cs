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
            

            var query = @"     SELECT
                               COUNT(*)
                               FROM Estoque

                               SELECT ProdutoId,ProdutoNOME,DATAEntrada,DATASaida,CodigoProduto,Quantidade 
                               FROM Estoque 
                               ORDER BY ProdutoId
                               OFFSET @Skip ROWS FETCH NEXT @Take ROWS Only";
            if(skip == 0 && take == 0) 
            {
                query = query.Replace("ORDER BY ProdutoId\r\n                               OFFSET @Skip ROWS FETCH NEXT @Take ROWS Only", " ");
            }

            return await MultipleQueryAsync(query, async (GridReader reader) => 
            {
                int quantidadeTotal =  reader.Read<int>().FirstOrDefault();
                List<Produto> produtos = reader.Read<Produto>().ToList();

                return new Paginacao<List<Produto>>(quantidadeTotal, produtos,paginaAtual,quantidadePorPagina);
            
            },param, CommandType.Text) ;
           
        }

        public async Task<Produto> BuscarProduto(Guid? codigo)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CodigoDoProduto", codigo, DbType.Guid);

            string query = @"Select 
                                   ProdutoId,ProdutoNome,DataEntrada,DataSaida,CodigoProduto,Quantidade 
                               FROM Estoque 
                             WHERE CodigoProduto = @CodigoDoProduto";
            return await QueryFirstOrDefaultAsync<Produto>(query, param, CommandType.Text);
        }

        public async Task<Produto> VerificaSeExiste(string? nome, Guid? codigo)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Nome", nome, DbType.AnsiString);
            param.Add("@Codigo", codigo, DbType.Guid);

            string query = @"SELECT ProdutoNome, CodigoProduto
                            FROM Estoque
                            WHERE ProdutoNome = @Nome OR CodigoProduto = @Codigo";

            return await QueryFirstOrDefaultAsync<Produto>(query, param, CommandType.Text);

        } 
        public async Task<int> AtualizarEstoque(Guid codigo,string nome, int reabastecer)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Codigo", codigo, DbType.Guid);
            param.Add("@Nome", nome, DbType.AnsiString);
            param.Add("@Quantidade", reabastecer, DbType.Int32);
            param.Add("@DataEntrada", DateTime.Now, DbType.DateTime);
            
            string query = @"UPDATE Estoque
                             SET Quantidade = Quantidade + @Quantidade,
                                 DataEntrada = @DataEntrada
                            WHERE CodigoProduto = @Codigo OR ProdutoNome = @Nome";
           
            return await ExecuteAsync(query,param,commandType: CommandType.Text);
        }

        public async Task<int> AtualizarProdutoParcial( Guid codigoDoProduto, string nome)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Codigo", codigoDoProduto, DbType.Guid);
            param.Add("@Nome", nome, DbType.AnsiString);
            string query = @"UPDATE Estoque 
                             SET ProdutoNome = @Nome
                            WHERE CodigoProduto = @Codigo";

            return await ExecuteAsync(query, param, commandType: CommandType.Text);
        }
    }
}
