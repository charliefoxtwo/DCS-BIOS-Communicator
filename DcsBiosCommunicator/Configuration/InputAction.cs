// ReSharper disable UnusedMember.Global
namespace DcsBios.Communicator.Configuration;

public record InputAction : BiosInput
{
    public const string Interface = "action";

    public const string Argument = "TOGGLE";
}
