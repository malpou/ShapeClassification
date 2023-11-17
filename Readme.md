# SimCorp Coding Challenge
<hr>

## Assignment

Write a program that will determine the type of a triangle.
- It should take the lengths of the triangle’s three sides as input, and return whether the triangle is equilateral, isosceles, or scalene. 
- We are looking for solutions that showcase problem solving skills and structural considerations that can be applied to larger and potentially more complex problem domains.
- Pay special attention to tests, readability, and error cases.
<hr>

## Solution
The solution is written as a .Net Web API project. 

The solution is structured in **4 source projects**:
- **Api** - The Web API project (this is the project that is started when the application is started)
- **Application** - The application logic (the core of the application, this is limited in this case, but in a real world application this would be the place to put the application logic)
- **Domain** - The domain model and logic (business logic of the application)
- **Contracts** - The contracts for the application (response records in this case)
- 
And **2 test projects**:
 - **Api.Tests** - Integration tests for the Web API testing the full from request to response (using the WebApplicationFactory)
 - **Domain.Tests** - Unit tests for the domain logic

### Usage
The Web API is hosted on `https://localhost:7555` and `http://localhost:6555`. 

The API has one endpoint: `/type-of-shape/triangle`. The endpoint accepts a `GET` request with a Query param called sides `ides=202,205,196`.
The sides query param is a comma separated list of integers or floats (using `.` as decimal seperator). 

The endpoint returns a JSON response with the type of triangle and the sides of the triangle.
```json
{
  "value": {
    "type": "Scalene"
  }
}
```
In the `TypeOfShape.Api` project there is a `TypeOfTriangle.http` file that can be used to test the API.

#### Error handling
The API contain error handling for the following cases:
- The sides provided are not able to be parsed into a list of numbers
- There are not exactly 3 sides provided
- Invalid input (not a number, negative number, zero, etc.)
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

