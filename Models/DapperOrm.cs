using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace WebApplication5.Models
{
    public static class DapperOrm
    {
        private static string _connectionString = @"Data Source=.;Initial Catalog=DapperDb;Integrated Security=True;";


        public static void ExceptionWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (var sqlCon = new SqlConnection(_connectionString))
            {

               sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (var sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
               return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }

        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (var sqlCon = new SqlConnection(_connectionString))
            {

                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}