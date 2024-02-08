using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using WhatsAppMeta.Enums;
using WhatsAppMeta.Exceptions;
using WhatsAppMeta.Interfaces;
using WhatsAppMeta.Models;

namespace WhatsAppMeta.Repository;
public class Repository : IRepository
{
    private readonly string _connectionString;
    private readonly int _commandTimeout;
    private readonly Logger<WhatsAppRepository> _logger;

    /// <summary>
    /// Inject the IConfiguration when creating an instance of this class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="log"></param>
    public Repository(DbContext context, Logger<WhatsAppRepository> log)
    {
        _connectionString = context.Database.GetDbConnection().ConnectionString;
        _commandTimeout = context.Database.GetCommandTimeout() == null ? 30 : Convert.ToInt32(context.Database.GetCommandTimeout());
        _logger = log;
    }

    private SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }

    private static SqlConnection OpenCustomConnection(string customConnectionString)
    {
        var conn = new SqlConnection(customConnectionString);
        conn.Open();
        return conn;
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a return data set it not expected.</para>
    /// </summary>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    public void ExecuteSp(string storedProcedureName, List<ParametersCollection> parameters = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            using var connection = OpenConnection();
            connection.Execute(storedProcedureName, param: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSP");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a return data set it not expected.</para>
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="commandTimeOut">Optional Command Time Out.</param>
    public async Task ExecuteSpAsync(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, int? commandTimeOut = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            await using var connection = OpenConnection();
            await connection.ExecuteAsync(new CommandDefinition(commandText: storedProcedureName, cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut ?? _commandTimeout)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPAsync");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute a Store Procedure when a List of T is expected in return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>An IEnumerable of type T.</returns>
    public IEnumerable<T> ExecuteSpList<T>(string storedProcedureName, List<ParametersCollection> parameters = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            using var connection = OpenConnection();
            var output = connection.Query<T>(storedProcedureName, param: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);
            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPList");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute a Store Procedure when a List of T is expected in return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="commandTimeout">Optional set of parameters that matches the query.</param>
    /// <returns>An IEnumerable of type T.</returns>
    public async Task<IEnumerable<T>> ExecuteSpListAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, int? commandTimeout = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            await using var connection = OpenConnection();
            var output = await connection.QueryAsync<T>(new CommandDefinition(commandText: storedProcedureName, cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? _commandTimeout)).ConfigureAwait(false);
            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPListAsync");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a return value is expected as a return.</para>
    /// </summary>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>long value</returns>
    public long ExecuteSpReturnValue(string storedProcedureName, List<ParametersCollection> parameters = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            procedureParameters.Add("ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.ReturnValue);

            using (var connection = OpenConnection())
            {
                connection.Execute(storedProcedureName, param: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);
            }

            long returnValue = procedureParameters.Get<int>("ReturnValue");

            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPReturnValue");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a return value is expected as a return.</para>
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="customConnectionString">Optional set of custom connection string that matches the query.</param>
    /// <returns>long value</returns>
    public async Task<long> ExecuteSpReturnValueAsync(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, string customConnectionString = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            procedureParameters.Add("ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.ReturnValue);

            if (customConnectionString is not null)
            {
                await using (var connection = OpenCustomConnection(customConnectionString))
                {
                    await connection.ExecuteAsync(new CommandDefinition(commandText: storedProcedureName, cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout)).ConfigureAwait(false);
                }

                return procedureParameters.Get<int>("ReturnValue");
            }

            await using (var connection = OpenConnection())
            {
                await connection.ExecuteAsync(new CommandDefinition(commandText: storedProcedureName, cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout)).ConfigureAwait(false);
            }

            long returnValue = procedureParameters.Get<int>("ReturnValue");

            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPReturnValueAsync");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a single item is expected as a return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>A single instance of type T.</returns>
    public T ExecuteSpSingle<T>(string storedProcedureName, List<ParametersCollection> parameters = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            T returnSingle;

            using (var connection = OpenConnection())
            {
                returnSingle = connection.QueryFirstOrDefault<T>(storedProcedureName, param: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);

            }
            if (returnSingle != null)
            {
                return returnSingle;
            }

            throw new StoredProcedureExecutionException("No Records Found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPSingle");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    /// <summary>
    /// <para>Execute any Stored Procedure where a single item is expected as a return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>A single instance of type T.</returns>
    public async Task<T> ExecuteSpSingleAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            T returnSingle;

            await using (var connection = OpenConnection())
            {
                returnSingle = await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(commandText: storedProcedureName, cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout)).ConfigureAwait(false);
            }

            if (returnSingle != null)
            {
                return returnSingle;
            }

            //return (T)Enumerable.Empty<T>();

            throw new StoredProcedureExecutionException("No Records Found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteSPSingleAsync");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }

    public async Task<dynamic> ExecuteQueryMultipleAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, IEnumerable<MapItem> mapItems = null)
    {
        try
        {
            var procedureParameters = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    procedureParameters.Add(parameter.ParameterName, parameter.ParameterValue, dbType: parameter.ParameterType, direction: parameter.ParameterDirection);
                }
            }

            var data = new ExpandoObject();

            await using var multi = await OpenConnection().QueryMultipleAsync(new CommandDefinition(commandText: storedProcedureName,
                cancellationToken: token, parameters: procedureParameters, commandType: CommandType.StoredProcedure,
                commandTimeout: _commandTimeout)).ConfigureAwait(false);
            if (mapItems == null) return data;

            foreach (var item in mapItems)
            {
                switch (item.DataFetchType)
                {
                    case DataFetchType.FirstOrDefault:
                        {
                            var singleItem = multi.ReadAsync(item.Type).Result.FirstOrDefault();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                            break;
                        }
                    case DataFetchType.ToList:
                        {
                            var listItem = multi.ReadAsync(item.Type).Result.ToList();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                            break;
                        }
                    default:
                        throw new InvalidEnumArgumentException("Invalid Enum Value");
                }
            }
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LogError Running ExecuteQueryMultipleAsync");
            throw new StoredProcedureExecutionException(ex.Message);
        }
    }
}