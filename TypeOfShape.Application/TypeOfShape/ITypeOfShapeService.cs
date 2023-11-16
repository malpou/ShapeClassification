namespace TypeOfShape.Application.TypeOfShape;

public interface ITypeOfShapeService<TShapeType>
    where TShapeType : Enum
{
    ErrorOr<TShapeType> Handle(double[] sides);
}