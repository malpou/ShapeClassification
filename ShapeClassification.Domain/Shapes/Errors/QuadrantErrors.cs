namespace ShapeClassification.Domain.Shapes.Errors;

public static class QuadrantErrors
{
    private static readonly DomainErrors Errors = new(nameof(TriangleErrors));

    public static Error QuadrantNotSupported => Errors.Validation("Quadrants are not supported");
}