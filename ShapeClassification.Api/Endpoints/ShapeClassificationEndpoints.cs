namespace ShapeClassification.Api.Endpoints;

public class ShapeClassificationEndpoints : IEndpoints
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("shape", ShapeClassificationEndpointHandler.Handle);
    }
}