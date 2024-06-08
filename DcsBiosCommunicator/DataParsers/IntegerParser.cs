using System;
using System.ComponentModel.DataAnnotations;

namespace DcsBios.Communicator.DataParsers;

public class IntegerParser : DataParser<int>
{
    private readonly ushort _mask;
    private readonly byte _shift;

    public IntegerParser(in ushort mask, [Range(0, 15)] in byte shift, in string biosCode) : base(default, biosCode)
    {
        _mask = mask;
        _shift = shift;
    }

    public override void AddData(in ushort address, in ushort data)
    {
        CurrentValue = (data & _mask) >> _shift;
    }
}
