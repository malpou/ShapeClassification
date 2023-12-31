﻿namespace ShapeClassification.Domain.Shapes.Errors;

public static class TriangleErrors
{
    private static readonly DomainErrors Errors = new(nameof(TriangleErrors));

    public static Error InvalidTriangleError => Errors.Validation("Invalid triangle");

    public static Error FlatTriangleError => Errors.Validation("Triangle is flat");
}