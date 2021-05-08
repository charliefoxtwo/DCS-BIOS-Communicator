// ReSharper disable UnusedMember.Global
namespace DcsBios.Communicator.Configuration
{
    public class InputAction : BiosInput
    {
        public const string InterfaceType = "action";

        // TODO: enumify?
        public string Argument { get; set; } = null!;
    }
}