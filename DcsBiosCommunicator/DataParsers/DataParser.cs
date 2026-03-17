namespace DcsBios.Communicator.DataParsers;

public abstract class DataParser<T>(
    in ushort address,
    in string biosCode,
    in string moduleName,
    T defaultValue
)
{
    public ushort Address { get; } = address;

    public string BiosCode { get; } = biosCode;

    public string ModuleName { get; } = moduleName;

    public T CurrentValue { get; protected set; } = defaultValue;

    public abstract void AddData(in ushort address, in ushort data);
}
