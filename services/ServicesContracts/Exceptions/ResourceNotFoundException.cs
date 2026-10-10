namespace Ms06ServicesContracts.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message) : base(message) { }
}

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}