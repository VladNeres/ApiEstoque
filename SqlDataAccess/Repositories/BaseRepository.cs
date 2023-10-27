using Dapper;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace SqlDataAccess.Repositories
{
    public class BaseRepository
    {
        protected string _connectionString;
        public Guid _connectionID {  get;private set; }
        public bool _getConnectionClient { get; set; }

        public BaseRepository(string connectionString,  bool getConnectionClient = false) =>
            (_connectionString,_getConnectionClient) = (connectionString,getConnectionClient);
        


        
        protected async Task<int> ExecuteAsync(string query, object param = null, CommandType? commandType = null)
        {
            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                return await conn.ExecuteAsync(query, param:param, commandType:commandType);
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected async Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null, CommandType? commandType = null)
        {

            try
            {
               using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();

                GetClienteConnection(conn);

                return await conn.QueryFirstOrDefaultAsync<T>(query, param, commandType: commandType);

            }
            catch (Exception)
            {

                throw;
            }
        }




        protected async Task<T>QuerySingleAsync<T>(string query, object param = null, CommandType? commandType = null)
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                return await QuerySingleAsync<T>(query, param, commandType:commandType);

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async Task<IEnumerable<T>>QueryAsync<T>(string query, object param = null, CommandType? commandType = null)
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);

                conn.Open();
                GetClienteConnection(conn);

                return await QueryAsync<T>(query, param: param, commandType: commandType);

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task<IEnumerable<T2>>QueryAsync<T,T1,T2>(string query, Func<T,T1,T2> map, object param = null, CommandType? commandType = null, string splitOn = "id")
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                return await conn.QueryAsync(query, map, param, commandType: commandType, splitOn:splitOn);

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async Task<IEnumerable<T3>> QueryAsync<T, T1, T2,T3>(string query, Func<T, T1, T2,T3> map, object param = null, CommandType? commandType = null, string splitOn = "id")
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                return await conn.QueryAsync(query, map, param, commandType: commandType, splitOn: splitOn);

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async Task<IEnumerable<T4>> QueryAsync<T, T1, T2, T3,T4>(string query, Func<T, T1, T2, T3,T4> map, object param = null, CommandType? commandType = null, string splitOn = "id")
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                return await conn.QueryAsync(query, map, param, commandType: commandType, splitOn: splitOn);

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task<T>MultipleQueryAsync<T>(string query, Func<GridReader, Task<T>> mapearRetorno, object param = null , CommandType? commandType = null)
        {

            try
            {
                using IDbConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                GetClienteConnection(conn);

                using (var retorno = await conn.QueryMultipleAsync(query, param, commandType: commandType))
                {
                    return await mapearRetorno(retorno);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetClienteConnection(IDbConnection conn)
        {
            if(_getConnectionClient)
            {
                _connectionID = (conn as SqlConnection).ClientConnectionId;
            }
        }

        

    }
}
