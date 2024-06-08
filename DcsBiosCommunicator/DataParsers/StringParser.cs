using System;

namespace DcsBios.Communicator.DataParsers;

public sealed class StringParser : DataParser<string>
{
    public bool DataReady => _bufferFilledBits == _bufferSizeBits;

    public byte Length { get; }
    private readonly byte[] _buffer;
    private readonly long _bufferSizeBits;
    private long _bufferFilledBits;

    private readonly ushort _baseAddress;

    public StringParser(in ushort address, in byte length, in string biosCode) : base(address, biosCode)
    {
        if (length is < 1 or > 64)
            throw new ArgumentOutOfRangeException(nameof(length), length,
                $"Length of {biosCode} should be between 1 and 64, inclusive.");

        Length = length;
        _buffer = new byte[length];
        _bufferSizeBits = (2L << (length - 1)) - 1;
        _baseAddress = address;
    }

    private bool SetCharacter(int index, byte b)
    {
        _buffer[index] = b;

        if (!DataReady)
        {
            _bufferFilledBits |= index > 0 ? 2L << (index - 1) : 1L;
        }

        return index + 1 == Length;
    }

    /// <summary>
    /// Builds upon the existing string data
    /// </summary>
    /// <param name="address">BIOS Address the data came from</param>
    /// <param name="data">Data received at the provided BIOS address</param>
    /// <returns></returns>
    public override void AddData(in ushort address, in ushort data)
    {
        var b1 = (byte) (data & 0xFF);
        var b2 = (byte) (data >> 8);

        var offset = address - _baseAddress;

        var done = SetCharacter(offset, b1);
        if (!done)
        {
            SetCharacter(offset + 1, b2);
        }

        if (!DataReady) return;

        var newValue = System.Text.Encoding.UTF8.GetString(_buffer);
        if (newValue != CurrentValue)
        {
            CurrentValue = newValue;
        }
    }
}
