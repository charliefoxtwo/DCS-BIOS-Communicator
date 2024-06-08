using System.Collections.Generic;
using System.Linq;
using DcsBios.Communicator.DataParsers;

namespace DcsBios.Communicator;

internal class IntegerHandler
{
    public int Address { get; }

    public IList<IntegerParser> MaskShifts { get; }

    public IntegerHandler(in int address, IEnumerable<IntegerParser> maskShifts)
    {
        Address = address;
        MaskShifts = maskShifts.ToList();
    }
}
