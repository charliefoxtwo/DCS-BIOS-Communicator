namespace DcsBios.Communicator.Configuration;

public record OutputInteger : BiosOutput
{
    public static string OutputType => "integer";

    public ushort Mask { get; set; }
    public ushort MaxValue { get; set; }
    public byte ShiftBy { get; set; }
}
