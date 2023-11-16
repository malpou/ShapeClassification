namespace TypeOfShape.Core.Domain.Errors;

public class CommonErrors
{
    private static readonly DomainErrors Errors = new(nameof(CommonErrors));
    
    public static Error ToFewSidesError => Errors.Validation( "Too few sides");

    public static Error ZeroOrNegativeSideError => Errors.Validation("Zero or negative side");
}