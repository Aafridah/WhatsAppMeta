using WhatsAppMeta.Models;

namespace WhatsAppMeta.Interfaces;
public interface IRepository
{

    /// <summary>
    /// <para>Execute any Stored Procedure where a return data set it not expected.</para>
    /// </summary>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    void ExecuteSp(string storedProcedureName, List<ParametersCollection> parameters = null);

    /// <summary>
    /// <para>Execute any Stored Procedure where a return data set it not expected.</para>
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="commandTimeOut">Optional Command Time Out.</param>
    Task ExecuteSpAsync(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, int? commandTimeOut = null);

    /// <summary>
    /// <para>Execute any Stored Procedure where a single item is expected as a return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>A single instance of type T.</returns>
    T ExecuteSpSingle<T>(string storedProcedureName, List<ParametersCollection> parameters = null);

    /// <summary>
    /// <para>Execute any Stored Procedure where a single item is expected as a return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>A single instance of type T.</returns>
    Task<T> ExecuteSpSingleAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null);

    /// <summary>
    /// <para>Execute any Stored Procedure where a return value is expected as a return.</para>
    /// </summary>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>long value</returns>
    long ExecuteSpReturnValue(string storedProcedureName, List<ParametersCollection> parameters = null);

    /// <summary>
    /// <para>Execute any Stored Procedure where a return value is expected as a return.</para>
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="customConnectionString">Optional set of custom connection string that matches the query.</param>
    /// <returns>long value</returns>
    Task<long> ExecuteSpReturnValueAsync(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, string customConnectionString = null);

    /// <summary>
    /// <para>Execute a Store Procedure when a List of T is expected in return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <returns>An IEnumerable of type T.</returns>
    IEnumerable<T> ExecuteSpList<T>(string storedProcedureName, List<ParametersCollection> parameters = null);

    /// <summary>
    /// <para>Execute a Store Procedure when a List of T is expected in return.</para>
    /// </summary>
    /// <typeparam name="T">The type that matches the database table.</typeparam>
    /// <param name="token">Cancellation Token</param>
    /// <param name="storedProcedureName">Name of the stored procedure to be executed.</param>
    /// <param name="parameters">Optional set of parameters that matches the query.</param>
    /// <param name="commandTimeout">Optional set of parameters that matches the query.</param>
    /// <returns>An IEnumerable of type T.</returns>
    Task<IEnumerable<T>> ExecuteSpListAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, int? commandTimeout = null);

    Task<dynamic> ExecuteQueryMultipleAsync<T>(CancellationToken token, string storedProcedureName, List<ParametersCollection> parameters = null, IEnumerable<MapItem> mapItems = null);

}
