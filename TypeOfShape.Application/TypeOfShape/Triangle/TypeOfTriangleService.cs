using TypeOfShape.Domain.Triangle;

namespace TypeOfShape.Application.TypeOfShape.Triangle;

public class TypeOfTriangleService : ITypeOfTriangleService
{
    public ErrorOr<TriangleTypes> Handle(float[] sides)
    {
        var triangle = Domain.Triangle.Triangle.CreateFromSides(sides);

        if (triangle.IsError) return triangle.Errors;

        return triangle.Value.GetType();
    }
}