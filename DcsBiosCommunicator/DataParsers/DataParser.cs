namespace DcsBios.Communicator.DataParsers;

public abstract class DataParser<T>
{
    public ushort Address { get; }

    public string BiosCode { get; }

    public T CurrentValue { get; protected set; } = default!;

    protected DataParser(in ushort address, in string biosCode)
    {
        Address = address;
        BiosCode = biosCode;
    }

    public abstract void AddData(in ushort address, in ushort data);
}
