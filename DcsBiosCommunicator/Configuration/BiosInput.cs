using System;
using System.Collections.Generic;
using DcsBios.Communicator.Configuration.JsonConverters;
using Newtonsoft.Json;

namespace DcsBios.Communicator.Configuration;

[JsonConverter(typeof(InputConverter))]
public abstract record BiosInput
{
    private static readonly Dictionary<string, Type> Types = new()
    {
        [InputFixedStep.Interface] = typeof(InputFixedStep),
        [InputSetState.Interface] = typeof(InputSetState),
        [InputAction.Interface] = typeof(InputAction),
        [InputVariableStep.Interface] = typeof(InputVariableStep),
        [InputSetString.Interface] = typeof(InputSetString),
    };

    public static Type GetTypeForType(in string type)
    {
        return Types.TryGetValue(type, out var result) ? result : typeof(InputUnknown);
    }
}
