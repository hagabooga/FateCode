using Godot;
using System.Linq;
using System.Reflection;
using static Godot.GD;

namespace Utility
{
    public abstract class EzServer<T> : EzNode
    {
        public event Handlers.Id peerConnected, peerDisconnected;


        protected readonly NetworkedMultiplayerENet network = new NetworkedMultiplayerENet();
        protected readonly MultiplayerAPI multiplayerApi = new MultiplayerAPI();
        protected readonly ServerOptions<T> options;
        protected readonly X509Certificate certificate;
        protected readonly CryptoKey cryptoKey;

        public NetworkedMultiplayerENet Network => network;

        protected EzServer(ServerOptions<T> options,
                           X509Certificate certificate = null,
                           CryptoKey cryptoKey = null)
        {
            this.options = options;
            this.certificate = certificate;
            this.cryptoKey = cryptoKey;
        }

        public override void _Ready()
        {
            base._Ready();
            if (certificate != null && cryptoKey != null)
            {
                Network.UseDtls = true;
                Network.SetDtlsCertificate(certificate);
                Network.SetDtlsKey(cryptoKey);
            }
            else
            {
                Print($"EzServer: {Name} is not using certificate.");
            }
            CustomMultiplayer = multiplayerApi;
            CustomMultiplayer.RootNode = this;
            CustomMultiplayer.Connect("network_peer_connected", this, nameof(OnNetworkPeerConnected));
            CustomMultiplayer.Connect("network_peer_disconnected", this, nameof(OnNetworkPeerDisconnected));
            Network.CreateServer(options.Port, options.MaxClients);
            CustomMultiplayer.NetworkPeer = Network;
            Print($"Server {Name} started.");
        }

        public override void _Process(float delta)
        {
            if (CustomMultiplayer == null || !CustomMultiplayer.HasNetworkPeer())
            {
                return;
            }
            CustomMultiplayer.Poll();
        }

        protected virtual void OnNetworkPeerConnected(int id)
        {
            Print($"Server {Name}: Network peer {id} connected.");
            peerConnected?.Invoke(id);
        }

        protected virtual void OnNetworkPeerDisconnected(int id)
        {
            Print($"Server {Name}: Network peer {id} disconnected.");
            peerDisconnected?.Invoke(id);
        }
    }
}