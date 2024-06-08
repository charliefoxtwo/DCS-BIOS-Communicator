using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using DcsBios.Communicator.DataParsers;

namespace Benchmark;

[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
public class DataParserBenchmarkTests
{
    private readonly StringParser _stringParser = new(0, 2, "ID");
    private readonly IntegerParser _intParser = new(8, 3, "ID");

    private const int TestStringData = 'a' | ('b' << 8);
    private const int ClearStringData = ' ' | (' ' << 8);

    [Benchmark]
    public void TestString()
    {
        _stringParser.AddData(0, TestStringData);
        _stringParser.AddData(0, ClearStringData);
    }

    private const int TestIntData = 16;
    private const int ClearIntData = 0;

    [Benchmark]
    public void TestInt()
    {
        _intParser.AddData(0, TestIntData);
        _intParser.AddData(0, ClearIntData);
    }
}
