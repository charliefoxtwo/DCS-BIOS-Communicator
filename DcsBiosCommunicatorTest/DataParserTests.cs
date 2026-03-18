using DcsBios.Communicator.DataParsers;
using NUnit.Framework;

namespace DcsBiosCommunicatorTest;

public class DataParserTests
{
    [Test]
    public void Test2CharString()
    {
        var parser = new StringParser(0, 2, "ID", "CommonData");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("ab"));
    }

    [Test]
    public void Test2CharStringChangeFirstCharacter()
    {
        var parser = new StringParser(0, 2, "ID", "CommonData");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("ab"));

        const int data2 = 'c' | ('b' << 8);

        parser.AddData(0, data2);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("cb"));
    }

    [Test]
    public void Test2CharStringChangeSecondCharacter()
    {
        var parser = new StringParser(0, 2, "ID", "CommonData");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("ab"));

        const int data2 = 'a' | ('c' << 8);

        parser.AddData(0, data2);

        Assert.That(parser.DataReady, Is.True);
        Assert.That(parser.CurrentValue, Is.EqualTo("ac"));
    }

    [Test]
    public void Test4CharString()
    {
        var parser = new StringParser(0, 4, "ID", "CommonData");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.Multiple(() =>
        {
            Assert.That(parser.DataReady, Is.False);
            Assert.That(parser.CurrentValue, Is.EqualTo(string.Empty));
        });

        // added to the same position - noop
        parser.AddData(0, data);

        Assert.Multiple(() =>
        {
            Assert.That(parser.DataReady, Is.False);
            Assert.That(parser.CurrentValue, Is.EqualTo(string.Empty));
        });

        parser.AddData(2, data);

        Assert.Multiple(() =>
        {
            Assert.That(parser.DataReady, Is.True);
            Assert.That(parser.CurrentValue, Is.EqualTo("abab"));
        });
    }

    [Test]
    public void Test4CharStringWithNullByte()
    {
        var parser = new StringParser(0, 4, "ID", "CommonData");

        const int data = 'a' | ('b' << 8);

        parser.AddData(0, data);

        Assert.Multiple(() =>
        {
            Assert.That(parser.DataReady, Is.False);
            Assert.That(parser.CurrentValue, Is.EqualTo(string.Empty));
        });

        const int data2 = '\0' | ('b' << 8);
        parser.AddData(2, data2);

        Assert.Multiple(() =>
        {
            Assert.That(parser.DataReady, Is.True);
            Assert.That(parser.CurrentValue, Is.EqualTo("ab\0b"));
        });
    }

    [Test]
    public void TestInt()
    {
        var parser = new IntegerParser(8, 3, "ID", "CommonData");

        parser.AddData(0, 10);

        Assert.That(parser.CurrentValue, Is.EqualTo((10 & 8) >> 3));
    }
}
