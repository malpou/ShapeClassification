using TypeOfShape.Api.Endpoints.TypeOfShape;

namespace TypeOfShape.Api.Endpoints;

public class TypeOfShapeEndpoints : IEndpoints
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("triangle", TypeOfTriangleEndpoint.Handle);
    }
}