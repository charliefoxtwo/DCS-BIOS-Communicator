using System.ComponentModel.DataAnnotations;

namespace DcsBios.Communicator.DataParsers;

public sealed class StringParser(
    in ushort address,
    [Range(1, 64)] in byte length,
    in string biosCode,
    in string moduleName
) : DataParser<string>(address, biosCode, moduleName, string.Empty)
{
    public bool DataReady => _bufferFilledBits == _bufferSizeBits;

    public byte Length { get; } = length;
    private readonly byte[] _buffer = new byte[length];
    private readonly long _bufferSizeBits = (2L << (length - 1)) - 1;
    private long _bufferFilledBits;

    private readonly ushort _baseAddress = address;

    private bool SetCharacter(int index, byte b)
    {
        var hasChange = _buffer[index] != b;
        _buffer[index] = b;

        if (!DataReady && hasChange)
        {
            _bufferFilledBits |= index > 0 ? 2L << (index - 1) : 1L;
        }

        return hasChange;
    }

    /// <summary>
    /// Builds upon the existing string data
    /// </summary>
    /// <param name="address">BIOS Address the data came from</param>
    /// <param name="data">Data received at the provided BIOS address</param>
    /// <returns></returns>
    public override void AddData(in ushort address, in ushort data)
    {
        var b1 = (byte)(data & 0xFF);
        var b2 = (byte)(data >> 8);

        var offset = address - _baseAddress;

        var hasChange = SetCharacter(offset, b1);
        if (offset + 1 != Length)
        {
            hasChange |= SetCharacter(offset + 1, b2);
        }

        if (!DataReady || !hasChange)
        {
            return;
        }

        var newValue = System.Text.Encoding.UTF8.GetString(_buffer);
        if (newValue != CurrentValue)
        {
            CurrentValue = newValue;
        }
    }
}
