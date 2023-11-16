namespace TypeOfShape.Application.TypeOfShape;

public interface ITypeOfShapeService<TShapeType>
    where TShapeType : Enum
{
    ErrorOr<TShapeType> Handle(float[] sides);
}