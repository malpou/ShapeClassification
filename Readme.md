# SimCorp Coding Challenge

## Assignment

Write a program that will determine the type of a triangle.
- It should take the lengths of the triangle’s three sides as input and return whether the triangle is equilateral, isosceles, or scalene. 
- We are looking for solutions that showcase problem-solving skills and structural considerations that can be applied to larger and potentially more complex problem domains.
- Pay special attention to tests, readability, and error cases.

## Solution
The solution is written as a .Net Web API project. 

The solution is structured in **4 source projects**:
- **Api** - The Web API project (this is the project that is started when the application is started)
- **Application** - The application logic (the core of the application; this is limited in this case, but in a real-world application, this would be the place to put the application logic)
- **Domain** - The domain model and logic (business logic of the application)
- **Contracts** - The contracts for the application (response records in this case)
  
And **2 test projects**:
 - **Api.Tests** - Integration tests for the Web API testing the full from request to response (using the WebApplicationFactory)
 - **Domain.Tests** - Unit tests for the domain logic

### Usage
The Web API is hosted on `https://shapeclassification.azurewebsites.net`. 

The API has one endpoint: `/shape`. The endpoint accepts a `GET` request with a Query param called sides `?sides=202,205,196` (for now, the endpoint only supports triangles; the endpoint could easily be extended to support quadrants, for example).

The sides query param is a comma-separated list of integers or doubles (using `.` as a decimal separator). 

The endpoint returns a JSON response with the classification of the shape, the type, and the sides of the requested shape.
```json
{
  "value": {
    "classification": "Scalene",
    "type": "Triangle",
    "sides": [
      202,
      205,
      196
    ]
  }
}
```
In the `TypeOfShape.Api` project, there is a `ShapeClassification.http` file that can be used to test the API.

Three different host environments for the HTTP file can be used:
- **prod** - The production environment (the deployed API on Azure)
- **local-https** - The local environment using HTTPS
- **local-http** - The local environment using HTTP

#### Error handling
The API contains error handling for the following cases:
- The sides provided are not able to be parsed into a list of numbers
- There are not exactly three sides provided
- Invalid Input (not a number, negative number, zero, etc.)
- Invalid triangle (flat triangle, impossible triangle, etc.)

Example error response:
```json
{
  "error": {
    "code": "TriangleErrors.FlatTriangleError",
    "message": "Triangle is flat"
  }
}
```

### Packages
The solution uses the following packages:
#### Source projects
- **ErrorOr** - A package providing a fluent discriminated union for error and result handling
#### Test projects
- **xunit** - A unit testing framework
- **FluentAssertions** - A package providing fluent assertions for unit testing
- **Microsoft.AspNetCore.Mvc.Testing** - A package providing a WebApplicationFactory for integration testing
- **coverlet.collector** - A package providing code coverage for unit testing
