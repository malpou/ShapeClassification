namespace TypeOfShape.Contracts;

public record BaseResponse<TValueType>(TValueType? Value, Error? Error = null);