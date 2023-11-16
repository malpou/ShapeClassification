using TypeOfShape.Core.Domain.Triangle;

namespace TypeOfShape.Core.TypeOfShape.Triangle;

public interface ITypeOfTriangleService : ITypeOfShapeService<TriangleTypes>
{
    ErrorOr<TriangleTypes> GetTypeOfTriangle(int[] sides);
}