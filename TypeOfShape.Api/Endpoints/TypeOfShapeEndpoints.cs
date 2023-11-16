using TypeOfShape.Api.Endpoints.TypeOfShape;

namespace TypeOfShape.Api.Endpoints;

public class TypeOfShapeEndpoints : IEndpoints
{
    private const string BasePath = "type-of-shape";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BasePath}/triangle", TypeOfTriangleEndpoint.Handle);
    }
}