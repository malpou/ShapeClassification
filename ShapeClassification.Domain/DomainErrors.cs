using System.Runtime.CompilerServices;

namespace ShapeClassification.Domain;

public class DomainErrors(string baseName)
{
    public Error Validation(string message, [CallerMemberName] string? propertyName = null)
    {
        return CreateError(Error.Validation, message, propertyName);
    }

    private Error CreateError(Func<string, string, Error> errorFactory, string message, string? propertyName)
    {
        var name = propertyName ?? "Unknown";
        return errorFactory($"{baseName}.{name}", message);
    }
}