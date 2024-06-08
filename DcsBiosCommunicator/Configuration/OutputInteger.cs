namespace DcsBios.Communicator.Configuration;

public record OutputInteger : BiosOutput
{
    public static string OutputType => "integer";

    public int Mask { get; set; }
    public int MaxValue { get; set; }
    public int ShiftBy { get; set; }
}
