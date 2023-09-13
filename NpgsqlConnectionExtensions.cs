using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Postgres
{
    public static class NpgsqlConnectionExtensions
    {
        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns a list of the specified type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> QueryFunction<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT * FROM {function}({param});";

            var result = connection.Query<T>(query, parameters);

            return result.ToList();
        }

        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns a list of the specified type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<List<T>> QueryFunctionAsync<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT * FROM {function}({param});";

            var result = await connection.QueryAsync<T>(query, parameters);

            return result.ToList();
        }

        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns an item of the specified type;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T QueryFunctionFirst<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT * FROM {function}({param});";

            T result = connection.QueryFirst<T>(query, parameters);

            return result;
        }

        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns an item of the specified type;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> QueryFunctionFirstAsync<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT * FROM {function}({param});";

            T result = await connection.QueryFirstAsync<T>(query, parameters);

            return result;
        }

        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns a item of the specified type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T ExecuteFunction<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT {function}({param});";

            var result = connection.QuerySingleOrDefault<T>(query, parameters);

            return result;
        }

        /// <summary>
        /// Calls a Postgres function as a parameterized query and returns a item of the specified type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteFunctionAsync<T>(this NpgsqlConnection connection, string function, DynamicFunctionParameters? parameters = null)
        {
            string param = "";
            if (parameters != null)
            {
                param = parameters.GetParameterList();
            }

            string query = $"SELECT {function}({param});";

            var result = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);

            return result;
        }
    }
}
