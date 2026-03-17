namespace DcsBios.Communicator;

public interface IBiosTranslator
{
    /// <summary>
    /// Processes new string data from dcs-bios.
    /// </summary>
    void HandleStringData(in BiosPacket<string> data);

    /// <summary>
    /// Processes new integer data from dcs-bios
    /// </summary>
    void HandleIntegerData(in BiosPacket<int> data);
}

/// <summary>
/// Packet of data received from dcs-bios
/// </summary>
/// <param name="ModuleName">the name of the module to which the control belongs</param>
/// <param name="Identifier">the identifier of the control</param>
/// <param name="OldValue">the previous value of the control</param>
/// <param name="NewValue">the new value of the control</param>
/// <typeparam name="T">the type of data received</typeparam>
public readonly record struct BiosPacket<T>(
    string ModuleName,
    string Identifier,
    T OldValue,
    T NewValue
);
