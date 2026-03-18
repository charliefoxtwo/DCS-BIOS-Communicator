using System;
using System.ComponentModel.DataAnnotations;

namespace DcsBios.Communicator.DataParsers;

public sealed class StringParser(
    in ushort address,
    [Range(1, 64)] in byte length,
    in string biosCode,
    in string moduleName
) : DataParser<string>(address, biosCode, moduleName, string.Empty)
{
    public bool DataReady { get; private set; }

    public byte Length { get; } = length;
    private readonly char[] _buffer = new char[length];
    private readonly long _bufferSizeBits = (2L << (length - 1)) - 1;
    private long _bufferFilledBits;

    private readonly ushort _baseAddress = address;

    private bool SetCharacter(int index, byte b)
    {
        // if b is the default character, then it will match the _buffer[index] value if it hasn't been set yet
        // so we check _bufferFilledBits - if it hasn't been set yet, then this is new data
        var hasChange =
            _buffer[index] != b || (b == 0 && (_bufferFilledBits & BufferBit(index)) == 0);
        _buffer[index] = (char)b;

        if (!DataReady && hasChange)
        {
            _bufferFilledBits |= BufferBit(index);
            DataReady = _bufferFilledBits == _bufferSizeBits;
        }

        return hasChange;
    }

    private static long BufferBit(int index) => index > 0 ? 2L << (index - 1) : 1L;

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

        CurrentValue = new string(_buffer);
    }
}
