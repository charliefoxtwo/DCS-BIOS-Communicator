using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcsBios.Communicator.Configuration;
using NUnit.Framework;

namespace DcsBiosCommunicatorTest
{
    public class ConfigurationTests
    {
        private readonly AircraftBiosConfiguration _hornetConfiguration;
        public ConfigurationTests()
        {
            const string goodPath = "Resources/Good";
            var r = AircraftBiosConfiguration.AllConfigurations("AircraftAliases.json", null, goodPath).Result;
            _hornetConfiguration = r.Single();
        }

        // a bad configuration should not throw or crash
        [Test]
        public async Task TestBadConfiguration()
        {
            const string badPath = "Resources/Bad";
            var r = await AircraftBiosConfiguration.AllConfigurations("AircraftAliases.json", null, badPath);
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
            CollectionAssert.AreEquivalent(expectedAliases, _hornetConfiguration.Aliases.Intersect(expectedAliases).ToHashSet());
        }


        private readonly Dictionary<string, (List<BiosInput> Inputs, List<BiosOutput> Outputs)> _io = new()
            {
                ["AMPCD_BRT_CTL"] = (
                    new List<BiosInput>
                    {
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 65535,
                        },
                        new InputVariableStep
                        {
                            Interface = "variable_step",
                            MaxValue = 65535,
                            SuggestedStep = 3200,

                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29920,
                            Mask = 65535,
                            MaxValue = 65535,
                            ShiftBy = 0,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["AMPCD_CONT_SW"] = (
                    new List<BiosInput>
                    {
                        new InputFixedStep
                        {
                            Interface = "fixed_step",
                        },
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 2,
                        }
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29804,
                            Mask = 3072,
                            MaxValue = 2,
                            ShiftBy = 10,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["AMPCD_PB_01"] = (
                    new List<BiosInput>
                    {
                        new InputFixedStep
                        {
                            Interface = "fixed_step",
                        },
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 1,
                        },
                        new InputAction
                        {
                            Argument = "TOGGLE",
                            Interface = "action",
                        }
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29798,
                            Mask = 32768,
                            MaxValue = 1,
                            ShiftBy = 15,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["FIRE_APU_LT"] = (
                    new List<BiosInput>(),
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29708,
                            Mask = 4,
                            MaxValue = 1,
                            ShiftBy = 2,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["APU_CONTROL_SW"] = (
                    new List<BiosInput>
                    {
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 1
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29890,
                            Mask = 256,
                            MaxValue = 1,
                            ShiftBy = 8,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["ENGINE_CRANK_SW"] = (
                    new List<BiosInput>
                    {
                        new InputFixedStep
                        {
                            Interface = "fixed_step",
                        },
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 2,
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29890,
                            Mask = 1536,
                            MaxValue = 2,
                            ShiftBy = 9,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["CLOCK_ELAPSED_MINUTES"] = (
                    new List<BiosInput>(),
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29968,
                            Mask = 65535,
                            MaxValue = 65535,
                            ShiftBy = 0,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["EJECTION_HANDLE_SW"] = (
                    new List<BiosInput>
                    {
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 1,
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29902,
                            Mask = 16384,
                            MaxValue = 1,
                            ShiftBy = 14,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["BLEED_AIR_KNOB"] = (
                    new List<BiosInput>
                    {
                        new InputFixedStep
                        {
                            Interface = "fixed_step",
                        },
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 3,
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29894,
                            Mask = 768,
                            MaxValue = 3,
                            ShiftBy = 8,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["HUD_VIDEO_CONTROL_SW"] = (
                    new List<BiosInput>
                    {
                        new InputFixedStep
                        {
                            Interface = "fixed_step",
                        },
                        new InputSetState
                        {
                            Interface = "set_state",
                            MaxValue = 2,
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29740,
                            Mask = 12288,
                            MaxValue = 2,
                            ShiftBy = 12,
                            Suffix = string.Empty,
                            Type = "integer",
                        },
                    }
                ),
                ["IFEI_BINGO"] = (
                    new List<BiosInput>(),
                    new List<BiosOutput>
                    {
                        new OutputString
                        {
                            Address = 29800,
                            MaxLength = 5,
                            Suffix = string.Empty,
                            Type = "string",
                        },
                    }
                ),
                ["RADALT_HEIGHT"] = (
                    new List<BiosInput>
                    {
                        new InputVariableStep
                        {
                            Interface = "variable_step",
                            MaxValue = 65535,
                            SuggestedStep = 3200,
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputInteger
                        {
                            Address = 29974,
                            Mask = 65535,
                            MaxValue = 65535,
                            ShiftBy = 0,
                            Suffix = "_KNOB_POS",
                            Type = "integer",
                        },
                    }
                ),
                ["FAKE_CONTROL"] = (
                    new List<BiosInput>
                    {
                        new InputUnknown
                        {
                            Interface = "this_does_not_exist",
                        },
                    },
                    new List<BiosOutput>
                    {
                        new OutputUnknown
                        {
                            Address = 29975,
                            Type = "this_does_not_exist_either",
                        },
                    }
                ),
            };

        [Theory]
        [TestCase("AMPCD", "AMPCD_BRT_CTL", "limited_dial", null, null)]
        [TestCase("AMPCD", "AMPCD_CONT_SW", "selector", "none", "3_position_switch")]
        [TestCase("AMPCD", "AMPCD_PB_01", "selector", "none", "push_button")]
        [TestCase("APU Fire Warning Extinguisher Light", "FIRE_APU_LT", "led", null, null)]
        [TestCase("Auxiliary Power Unit Panel", "APU_CONTROL_SW", "toggle_switch", null, null)]
        [TestCase("Auxiliary Power Unit Panel", "ENGINE_CRANK_SW", "selector", "first_and_last", "rocker_switch")]
        [TestCase("Clock", "CLOCK_ELAPSED_MINUTES", "analog_gauge", null, null)]
        [TestCase("Ejection Seat", "EJECTION_HANDLE_SW", "toggle_switch", null, null)]
        [TestCase("Environment Control System Panel", "BLEED_AIR_KNOB", "selector", "none", "infinite_rotary")]
        [TestCase("HUD Control Panel", "HUD_VIDEO_CONTROL_SW", "selector", "none", "limited_rotary")]
        [TestCase("Integrated Fuel/Engine Indicator (IFEI)", "IFEI_BINGO", "display", null, null)]
        [TestCase("Radar Altimeter", "RADALT_HEIGHT", "analog_dial", null, null)]
        [TestCase("Radar Altimeter", "FAKE_CONTROL", "analog_dial", null, null)]
        public void ReadGoodConfiguration(string categoryName, string controlName, string controlType, string momentaryPositions, string phsyicalVariant)
        {
            var cat = _hornetConfiguration[categoryName];
            var ctrl = cat[controlName];
            Assert.AreEqual(categoryName, ctrl.Category);
            Assert.AreEqual(controlName, ctrl.Identifier);
            Assert.AreEqual(controlType, ctrl.ControlType);
            Assert.AreEqual(momentaryPositions, ctrl.MomentaryPositions);
            Assert.AreEqual(phsyicalVariant, ctrl.PhysicalVariant);

            var (inputs, outputs) = _io[controlName];

            CollectionAssert.AreEquivalent(inputs, ctrl.Inputs);
            CollectionAssert.AreEquivalent(outputs, ctrl.Outputs);
        }
    }
}
