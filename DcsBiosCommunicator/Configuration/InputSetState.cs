namespace DcsBios.Communicator.Configuration;

public record InputSetState : BiosInput
{
    public const string Interface = "set_state";

    public int MaxValue { get; set; }
}
