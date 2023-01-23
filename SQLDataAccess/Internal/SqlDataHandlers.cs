using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SQLDataAccess.Internal
{
    internal class SqlDataHandlers
    {
        private readonly IConfiguration _config;
        const string _defaultConnectionStringName = "MyApp";
        public SqlDataHandlers(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string name) => _config.GetConnectionString(name);

        public static DynamicParameters ToDynamic<T>(T obj)
        {
            DynamicParameters output = new DynamicParameters();
            if (obj != null)
            {
                var proerties = obj.GetType().GetProperties();
                foreach (var item in proerties)
                {
                        output.Add(item.Name, item.GetValue(obj));
                }
            }
            return output;
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.QueryAsync<T>(storedProcedure, ToDynamic(parameters),
                                commandType: CommandType.StoredProcedure);
                    return output.ToList();
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
        }

        public async Task<T> LoadSingleData<T, U>(string storedProcedure, U parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, ToDynamic(parameters),
                                commandType: CommandType.StoredProcedure);

                    return output;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.ExecuteScalarAsync(storedProcedure, ToDynamic(parameters),
                        commandType: CommandType.StoredProcedure);
                    if (output != null)
                    {
                        return Convert.ToInt32(output);
                    }
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                return 0;
            }
        }

        public async Task<T> SaveDataWithRefresh<T>(string storedProcedure, T parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, ToDynamic(parameters),
                        commandType: CommandType.StoredProcedure);
                    return output;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }

            }
        }

        public async Task<bool> DeleteData<T>(string storedProcedure, T parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.ExecuteAsync(storedProcedure, ToDynamic(parameters),
                        commandType: CommandType.StoredProcedure);

                    return output != 0;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }

            }
        }

        public async Task<bool> Execute<T>(string sqlStatement, CommandType commandType, T parameters, string connectionStringName = _defaultConnectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var output = await connection.ExecuteAsync(sqlStatement, ToDynamic(parameters),
                        commandType: commandType);

                    return output != 0;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
        }

    }
}
