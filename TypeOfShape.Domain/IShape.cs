namespace TypeOfShape.Domain;

public interface IShape<out TShapeType>
    where TShapeType : Enum
{
    TShapeType GetType();
}