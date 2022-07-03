using System;
using Godot;
using static Godot.GD;

namespace Utility
{
    public abstract class EzClient<T> : EzNode
    {
        public event Action connectedToServer, connectionFailed, serverDisconnected;

        protected readonly ClientOptions<T> options;
        protected readonly bool connectOnReady;
        protected readonly X509Certificate certificate;

        protected MultiplayerAPI multiplayerApi;
        protected NetworkedMultiplayerENet network;

        public EzClient(ClientOptions<T> options,
                        bool connectOnReady = true,
                        X509Certificate certificate = null)
        {
            this.options = options;
            this.connectOnReady = connectOnReady;
            this.certificate = certificate;
        }

        public override void _Ready()
        {
            base._Ready();
            if (!connectOnReady)
            {
                return;
            }
            CreateClient();
        }

        protected void CreateClient(string ipOverride = null)
        {
            connectedToServer = null;
            connectionFailed = null;
            serverDisconnected = null;
            multiplayerApi = new MultiplayerAPI();
            network = new NetworkedMultiplayerENet();
            if (certificate != null)
            {
                network.UseDtls = true;
                network.DtlsVerify = false;
                network.SetDtlsCertificate(certificate);
            }
            CustomMultiplayer = multiplayerApi;
            CustomMultiplayer.RootNode = this;
            CustomMultiplayer.Connect("connected_to_server", this, nameof(OnConnectedToServer));
            CustomMultiplayer.Connect("connection_failed", this, nameof(OnConnectionFailed));
            CustomMultiplayer.Connect("server_disconnected", this, nameof(OnServerDisconnected));
            network.CreateClient(ipOverride ?? options.Ip, options.Port);
            CustomMultiplayer.NetworkPeer = network;
            Print($"Client {Name} started.");
        }

        public override void _Process(float delta)
        {
            if (CustomMultiplayer == null || !CustomMultiplayer.HasNetworkPeer())
            {
                return;
            }
            CustomMultiplayer.Poll();
        }

        private void OnConnectedToServer()
        {
            Print($"Client {Name} has connected to the server.");
            connectedToServer?.Invoke();
        }

        private void OnConnectionFailed()
        {
            Print($"Client {Name}: Connection to server has failed.");
            CustomMultiplayer = null;
            connectionFailed?.Invoke();
        }

        private void OnServerDisconnected()
        {
            Print($"{Name}: Server disconnected.");
            serverDisconnected?.Invoke();
        }
    }
}