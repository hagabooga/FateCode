namespace Utility
{
    public class ClientOptions<T>
    {
        public string Ip { get; }
        public int Port { get; }

        public ClientOptions(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }
    }
}