using System;
using DcsBios.Communicator.DataParsers;
using NUnit.Framework;

namespace DcsBiosCommunicatorTest;

public class DataParserTests
{
    [Test]
    public void Test2CharString()
    {
        var parser = new StringParser(0, 2, "ID");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("ab"));
    }

    [Test]
    public void TestInt()
    {
        var parser = new IntegerParser(8, 3, "ID");

        parser.AddData(0, 10);

        Assert.That(parser.CurrentValue, Is.EqualTo((10 & 8) >> 3));
    }
}
