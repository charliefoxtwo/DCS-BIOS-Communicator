using System.Collections.Generic;
using System.Linq;
using DcsBios.Communicator.DataParsers;

namespace DcsBios.Communicator;

internal class IntegerHandler(in ushort address, IEnumerable<IntegerParser> maskShifts)
{
    public ushort Address { get; } = address;

    public IList<IntegerParser> MaskShifts { get; } = maskShifts.ToList();
}
