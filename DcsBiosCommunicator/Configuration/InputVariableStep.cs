namespace DcsBios.Communicator.Configuration;

public record InputVariableStep : InputSetState
{
    public new const string Interface = "variable_step";

    public int SuggestedStep { get; set; }
}
