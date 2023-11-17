namespace ShapeClassification.Domain.Errors;

public static class CommonErrors
{
    private static readonly DomainErrors Errors = new(nameof(CommonErrors));

    public static Error WrongAmountOfSides => Errors.Validation("Wrong amount of sides");

    public static Error ZeroOrNegativeSideError => Errors.Validation("Zero or negative side");
}