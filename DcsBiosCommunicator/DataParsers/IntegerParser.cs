using System;
using System.ComponentModel.DataAnnotations;

namespace DcsBios.Communicator.DataParsers;

public class IntegerParser(
    in ushort mask,
    [Range(0, 15)] in byte shift,
    in string biosCode,
    in string moduleName
) : DataParser<int>(0, biosCode, moduleName)
{
    private readonly ushort _mask = mask;
    private readonly byte _shift = shift;

    public override void AddData(in ushort address, in ushort data)
    {
        CurrentValue = (data & _mask) >> _shift;
    }
}
