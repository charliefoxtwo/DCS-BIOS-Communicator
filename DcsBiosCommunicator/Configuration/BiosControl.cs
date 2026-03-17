using System.Collections.Generic;

// ReSharper disable UnusedMember.Global

namespace DcsBios.Communicator.Configuration;

// ReSharper disable once ClassNeverInstantiated.Global
public record BiosControl(
    string Category,
    string ControlType,
    string Identifier,
    string Description,
    List<BiosInput> Inputs,
    List<BiosOutput> Outputs,
    DeprecatedAttribute? Deprecated,
    List<string>? Positions,
    string? Color
);
