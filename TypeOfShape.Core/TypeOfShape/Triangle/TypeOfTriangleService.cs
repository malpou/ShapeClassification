using TypeOfShape.Core.Domain.Triangle;

namespace TypeOfShape.Core.TypeOfShape.Triangle;

public class TypeOfTriangleService : ITypeOfTriangleService
{
    public ErrorOr<TriangleTypes> GetTypeOfTriangle(int[] sides)
    {
            var triangle = Domain.Triangle.Triangle.CreateFromSides(sides);
            
            if (triangle.IsError)
            {
                return triangle.Errors;
            }

            return triangle.Value.GetType();

    }
}