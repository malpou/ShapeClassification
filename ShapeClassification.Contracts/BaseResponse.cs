﻿namespace ShapeClassification.Contracts;

public record BaseResponse<TValueType>(TValueType? Value);

public record BaseResponse(Error? Error = null);