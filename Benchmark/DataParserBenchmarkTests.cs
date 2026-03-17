using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using DcsBios.Communicator.DataParsers;

namespace Benchmark;

[SimpleJob(RuntimeMoniker.Net10_0, baseline: true)]
public class DataParserBenchmarkTests
{
    private readonly StringParser _stringParser = new(0, 2, "ID", "CommonData");
    private readonly StringParser _stringParserLong = new(0, 8, "ID", "CommonData");
    private readonly IntegerParser _intParser = new(8, 3, "ID", "CommonData");

    private const int TestStringData = 'a' | ('b' << 8);
    private const int ClearStringData = ' ' | (' ' << 8);

    /// <summary>
    /// Writes data to a short string.
    /// </summary>
    [Benchmark]
    public void TestString()
    {
        _stringParser.AddData(0, TestStringData);
        _stringParser.AddData(0, ClearStringData);
    }

    /// <summary>
    /// Writes data to a longer string.
    /// </summary>
    [Benchmark]
    public void TestStringLong()
    {
        _stringParserLong.AddData(0, TestStringData);
        _stringParserLong.AddData(2, TestStringData);
        _stringParserLong.AddData(4, TestStringData);
        _stringParserLong.AddData(6, TestStringData);
        _stringParserLong.AddData(0, ClearStringData);
        _stringParserLong.AddData(2, ClearStringData);
        _stringParserLong.AddData(4, ClearStringData);
        _stringParserLong.AddData(6, ClearStringData);
    }

    /// <summary>
    /// Writes the same data to a short string twice.
    /// </summary>
    [Benchmark]
    public void TestStringWithDuplicateData()
    {
        _stringParser.AddData(0, TestStringData);
        _stringParser.AddData(0, TestStringData);
    }

    /// <summary>
    /// Writes the same data to a long string twice.
    /// </summary>
    [Benchmark]
    public void TestStringWithDuplicateDataLong()
    {
        _stringParserLong.AddData(0, TestStringData);
        _stringParserLong.AddData(2, TestStringData);
        _stringParserLong.AddData(4, TestStringData);
        _stringParserLong.AddData(6, TestStringData);
        _stringParserLong.AddData(0, TestStringData);
        _stringParserLong.AddData(2, TestStringData);
        _stringParserLong.AddData(4, TestStringData);
        _stringParserLong.AddData(6, TestStringData);
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
