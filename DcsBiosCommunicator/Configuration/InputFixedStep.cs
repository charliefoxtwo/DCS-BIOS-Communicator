namespace DcsBios.Communicator.Configuration;

public record InputFixedStep : BiosInput
{
    public const string Interface = "fixed_step";
    public const string Increment = "INC";
    public const string Decrement = "DEC";
}
