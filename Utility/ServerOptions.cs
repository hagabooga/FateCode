namespace Utility
{


    public class ServerOptions<T>
    {
        public int Port { get; }
        public int MaxClients { get; }

        public ServerOptions(int port, int maxClients)
        {
            Port = port;
            MaxClients = maxClients;
        }
    }

}