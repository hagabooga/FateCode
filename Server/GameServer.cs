using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using static Godot.GD;
using Utility;
using System.Linq;

namespace Server
{
    public class GameServer : EzServer<GameServer>
    {
        private readonly StateProcessing stateProcessing;
        private readonly PlayerVerification playerVerification;
        private readonly HubConnection hubConnection;

        public GameServer(StateProcessing stateProcessing,
                      PlayerVerification playerVerification,
                      HubConnection hubConnection,
                      ServerOptions<GameServer> options) :
                      base(options, null, null)
        {
            this.stateProcessing = stateProcessing;
            this.playerVerification = playerVerification;
            this.hubConnection = hubConnection;

            peerConnected += playerVerification.Start;

            playerVerification.verificationFailed += id => network.DisconnectPeer(id);

        }

        public override void _Ready()
        {
            base._Ready();
            GetTree().NetworkPeer = network;
        }

        [Remote]
        public void ReceiveToken()
        {
            Print("LAOSDLAOSD");
        }
    }
}