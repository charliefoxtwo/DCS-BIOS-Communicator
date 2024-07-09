namespace DcsBios.Communicator.DataParsers;

public abstract class DataParser<T>(in ushort address, in string biosCode)
{
    public ushort Address { get; } = address;

    public string BiosCode { get; } = biosCode;

    public T CurrentValue { get; protected set; } = default!;

    public abstract void AddData(in ushort address, in ushort data);
}
