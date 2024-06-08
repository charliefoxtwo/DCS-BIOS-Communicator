using System.Threading.Tasks;

namespace DcsBios.Communicator;

public interface IBiosSendClient
{
    Task Send(string address, string data);
}
