using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DcsBios.Communicator.Configuration;
using NUnit.Framework;

namespace DcsBiosCommunicatorTest;

public class ConfigurationTests
{
    private readonly AircraftBiosConfiguration _hornetConfiguration;
    private readonly AircraftBiosConfiguration _testConfiguration;

    public ConfigurationTests()
    {
        const string goodPath = "Resources/Good";
        var r = AircraftBiosConfiguration
            .AllConfigurations("AircraftAliases.json", null, goodPath)
            .Result;
        _hornetConfiguration = r.Single(c => c.AircraftName == "FA-18C_hornet");
        _testConfiguration = r.Single(c => c.AircraftName == "Tests");
    }

    // a bad configuration should not throw or crash
    [Test]
    public async Task TestBadConfiguration()
    {
        const string badPath = "Resources/Bad";
        var r = await AircraftBiosConfiguration.AllConfigurations(
            "AircraftAliases.json",
            null,
            badPath
        );
        Assert.IsEmpty(r);
    }

    [Test]
    public void TestAircraftName()
    {
        Assert.AreEqual(_hornetConfiguration.AircraftName, "FA-18C_hornet");
    }

    [Test]
    public void TestAliases()
    {
        var expectedAliases = new HashSet<string> { "EA-18G", "FA-18C_hornet", "FA-18E", "FA-18F" };
        Assert.That(
            _hornetConfiguration.Aliases.Intersect(expectedAliases).ToHashSet(),
            Is.EquivalentTo(expectedAliases)
        );
    }

    private readonly Dictionary<string, (List<BiosInput> Inputs, List<BiosOutput> Outputs)> _io =
        new()
        {
            ["AMPCD_BRT_CTL"] = (
                [
                    new InputSetState { MaxValue = 65535 },
                    new InputVariableStep { MaxValue = 65535, SuggestedStep = 3200 },
                ],
                [
                    new OutputInteger
                    {
                        Address = 29920,
                        Mask = 65535,
                        MaxValue = 65535,
                        ShiftBy = 0,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["AMPCD_CONT_SW"] = (
                [new InputFixedStep(), new InputSetState { MaxValue = 2 }],
                [
                    new OutputInteger
                    {
                        Address = 29804,
                        Mask = 3072,
                        MaxValue = 2,
                        ShiftBy = 10,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["AMPCD_PB_01"] = (
                [new InputFixedStep(), new InputSetState { MaxValue = 1 }, new InputAction()],
                [
                    new OutputInteger
                    {
                        Address = 29798,
                        Mask = 32768,
                        MaxValue = 1,
                        ShiftBy = 15,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["FIRE_APU_LT"] = (
                [],
                [
                    new OutputInteger
                    {
                        Address = 29708,
                        Mask = 4,
                        MaxValue = 1,
                        ShiftBy = 2,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["APU_CONTROL_SW"] = (
                [new InputSetState { MaxValue = 1 }],
                [
                    new OutputInteger
                    {
                        Address = 29890,
                        Mask = 256,
                        MaxValue = 1,
                        ShiftBy = 8,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["ENGINE_CRANK_SW"] = (
                [new InputFixedStep(), new InputSetState { MaxValue = 2 }],
                [
                    new OutputInteger
                    {
                        Address = 29890,
                        Mask = 1536,
                        MaxValue = 2,
                        ShiftBy = 9,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["CLOCK_ELAPSED_MINUTES"] = (
                [],
                [
                    new OutputInteger
                    {
                        Address = 29968,
                        Mask = 65535,
                        MaxValue = 65535,
                        ShiftBy = 0,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["EJECTION_HANDLE_SW"] = (
                [new InputSetState { MaxValue = 1 }],
                [
                    new OutputInteger
                    {
                        Address = 29902,
                        Mask = 16384,
                        MaxValue = 1,
                        ShiftBy = 14,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["BLEED_AIR_KNOB"] = (
                [new InputFixedStep(), new InputSetState { MaxValue = 3 }],
                [
                    new OutputInteger
                    {
                        Address = 29894,
                        Mask = 768,
                        MaxValue = 3,
                        ShiftBy = 8,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["HUD_VIDEO_CONTROL_SW"] = (
                [new InputFixedStep(), new InputSetState { MaxValue = 2 }],
                [
                    new OutputInteger
                    {
                        Address = 29740,
                        Mask = 12288,
                        MaxValue = 2,
                        ShiftBy = 12,
                        Suffix = string.Empty,
                        Type = "integer",
                    },
                ]
            ),
            ["IFEI_BINGO"] = (
                [],
                [
                    new OutputString
                    {
                        Address = 29800,
                        MaxLength = 5,
                        Suffix = string.Empty,
                        Type = "string",
                    },
                ]
            ),
            ["RADALT_HEIGHT"] = (
                [new InputVariableStep { MaxValue = 65535, SuggestedStep = 3200 }],
                [
                    new OutputInteger
                    {
                        Address = 29974,
                        Mask = 65535,
                        MaxValue = 65535,
                        ShiftBy = 0,
                        Suffix = "_KNOB_POS",
                        Type = "integer",
                    },
                ]
            ),
            ["FAKE_CONTROL"] = (
                [new InputUnknown()],
                [new OutputUnknown { Address = 29975, Type = "this_does_not_exist_either" }]
            ),
            ["STRING_INPUT_CONTROL"] = (
                [new InputSetString()],
                [
                    new OutputString
                    {
                        Address = 29976,
                        MaxLength = 7,
                        Suffix = string.Empty,
                        Type = "string",
                    },
                ]
            ),
        };

    [Theory]
    [TestCase("AMPCD", "AMPCD_BRT_CTL", "limited_dial")]
    [TestCase("AMPCD", "AMPCD_CONT_SW", "selector")]
    [TestCase("AMPCD", "AMPCD_PB_01", "selector")]
    [TestCase("APU Fire Warning Extinguisher Light", "FIRE_APU_LT", "led")]
    [TestCase("Auxiliary Power Unit Panel", "APU_CONTROL_SW", "toggle_switch")]
    [TestCase("Auxiliary Power Unit Panel", "ENGINE_CRANK_SW", "selector")]
    [TestCase("Clock", "CLOCK_ELAPSED_MINUTES", "analog_gauge")]
    [TestCase("Ejection Seat", "EJECTION_HANDLE_SW", "toggle_switch")]
    [TestCase("Environment Control System Panel", "BLEED_AIR_KNOB", "selector")]
    [TestCase("HUD Control Panel", "HUD_VIDEO_CONTROL_SW", "selector")]
    [TestCase("Integrated Fuel/Engine Indicator (IFEI)", "IFEI_BINGO", "display")]
    [TestCase("Radar Altimeter", "RADALT_HEIGHT", "analog_dial")]
    [TestCase("Radar Altimeter", "FAKE_CONTROL", "analog_dial")]
    [TestCase("Radar Altimeter", "STRING_INPUT_CONTROL", "radio")]
    public void ReadGoodConfiguration(string categoryName, string controlName, string controlType)
    {
        var cat = _hornetConfiguration[categoryName];
        var ctrl = cat[controlName];
        Assert.AreEqual(categoryName, ctrl.Category);
        Assert.AreEqual(controlName, ctrl.Identifier);
        Assert.AreEqual(controlType, ctrl.ControlType);

        var (inputs, outputs) = _io[controlName];

        Assert.That(ctrl.Inputs, Is.EquivalentTo(inputs));
        Assert.That(ctrl.Outputs, Is.EquivalentTo(outputs));
    }

    [TestCase("DEPRECATED", "1.2.3", null, "MULTI_POS_SWITCH")]
    public void ReadDeprecated(
        string controlName,
        string? since,
        string? description,
        string? useInstead
    )
    {
        var cat = _testConfiguration["Deprecated"];
        var ctrl = cat[controlName];
        Assert.That(ctrl.Deprecated, Is.Not.Null);

        Debug.Assert(ctrl.Deprecated is not null);

        Assert.Multiple(() =>
        {
            Assert.That(ctrl.Deprecated.Since, Is.EqualTo(since));
            Assert.That(ctrl.Deprecated.Description, Is.EqualTo(description));
            Assert.That(ctrl.Deprecated.UseInstead, Is.EqualTo(useInstead));
        });
    }

    [TestCase("MULTI_POS_SWITCH", "LEFT", "OFF", "RIGHT")]
    public void ReadPositions(string controlName, params string[] positions)
    {
        var cat = _testConfiguration["Positions"];
        var ctrl = cat[controlName];
        Assert.That(ctrl.Positions, Is.EquivalentTo(positions));
    }

    [TestCase("LED_INDICATOR", "green")]
    public void ReadColor(string controlName, string color)
    {
        var cat = _testConfiguration["Colors"];
        var ctrl = cat[controlName];
        Assert.That(ctrl.Color, Is.EqualTo(color));
    }
}
