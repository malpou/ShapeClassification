using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ShapeClassification.Application;
using ShapeClassification.Contracts;

namespace ShapeClassification.Api.Endpoints;

public class ShapeClassificationEndpointHandler
{
    public static IResult Handle(
        [FromQuery] string sides,
        [FromServices] IShapeFactory shapeFactory,
        [FromServices] ILogger<ShapeClassificationEndpointHandler> logger)
    {
        double[] sidesArray;

        try
        {
            sidesArray = sides.Split(',')
                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();
        }
        catch (Exception)
        {
            logger.LogError("Incorrect sides format: '{sides}'", sides);
            
            return Results.BadRequest(new BaseResponse(new Error("Api.IncorrectSidesFormat",
                "Invalid format, correct format is: sides=2,2,3 or sides=2.5,1,2.3 etc.")));
        }

        var shape = shapeFactory.CreateShape(sidesArray);

        if (!shape.IsError)
        {
            var classification = shape.Value.GetClassification();
            var type = shape.Value.GetShapeType();
            
            logger.LogInformation("Type of {type}: {classification} for '{sides}'", type, classification, sides);
            
            var response = new ShapeClassificationResponse(classification, type, sidesArray);
            
            return Results.Ok(new BaseResponse<ShapeClassificationResponse>(response));
        }
            
        var error = shape.FirstError;
        
        logger.LogError("Error: {code} for '{sides}'", error.Code, sides);
        return Results.BadRequest(new BaseResponse(new Error(error.Code, error.Description)));
    }
}