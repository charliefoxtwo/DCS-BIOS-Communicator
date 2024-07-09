using System;
using System.Collections.Generic;
using DcsBios.Communicator.Configuration.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace DcsBios.Communicator.Configuration;

[JsonConverter(typeof(OutputConverter))]
public abstract record BiosOutput
{
    public ushort Address { get; set; }

    public string Suffix { get; set; } = null!;

    public string Type { get; set; } = null!;

    private static readonly Dictionary<string, Type> Types = new()
    {
        [OutputInteger.OutputType] = typeof(OutputInteger),
        [OutputString.OutputType] = typeof(OutputString),
    };

    public static Type GetTypeForType(in string type)
    {
        return Types.TryGetValue(type, out var result) ? result : typeof(OutputUnknown);
    }
}
