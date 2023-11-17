using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TypeOfShape.Application.TypeOfShape.Triangle;
using TypeOfShape.Contracts;

namespace TypeOfShape.Api.Endpoints.TypeOfShape;

public class TypeOfTriangleEndpoint
{
    public static IResult Handle(
        [FromQuery] string sides,
        [FromServices] ITypeOfTriangleService typeOfTriangleService,
        [FromServices] ILogger<TypeOfTriangleEndpoint> logger)
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

        var typeOfTriangle = typeOfTriangleService.Handle(sidesArray);

        if (!typeOfTriangle.IsError)
        {
            var type = typeOfTriangle.Value.ToString();
            
            logger.LogInformation("Type of triangle: {type} for '{sides}'", type, sides);
            
            return Results.Ok(
                new BaseResponse<TypeOfShapeResponse>(new TypeOfShapeResponse(type)));
        }
            
        var error = typeOfTriangle.FirstError;
        
        logger.LogError("Error: {code} for '{sides}'", error.Code, sides);
        return Results.BadRequest(new BaseResponse(new Error(error.Code, error.Description)));
    }
}