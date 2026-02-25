namespace DcsBios.Communicator.Configuration;

// ReSharper disable once ClassNeverInstantiated.Global
public record DeprecatedAttribute(string? Since = null, string? Description = null, string? UseInstead = null);
