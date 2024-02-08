namespace WhatsAppMeta.Exceptions;

public sealed class StoredProcedureExecutionException : Exception
{
    public StoredProcedureExecutionException(string errorMessage) : base($"Error Executing Stored Procedure. Error : {errorMessage}")
    {
            
    }
}