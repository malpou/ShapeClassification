namespace ShapeClassification.Contracts;

public record ShapeClassificationResponse(string Classification, string Type, double[] Sides);