using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using static Godot.GD;
using Utility;
using System.Linq;

public class Server : EzServer<Server>
{
    private readonly StateProcessing stateProcessing;
    private readonly PlayerVerification playerVerification;
    private readonly HubConnection hubConnection;

    public Server(StateProcessing stateProcessing,
                  PlayerVerification playerVerification,
                  HubConnection hubConnection,
                  ServerOptions<Server> options) :
                  base(options, null, null)
    {
        this.stateProcessing = stateProcessing;
        this.playerVerification = playerVerification;
        this.hubConnection = hubConnection;
        this.playerVerification.Connect(nameof(PlayerVerification.VerificationFailed),
                                        this,
                                        nameof(DisconnectPeer));
    }

    private void DisconnectPeer(int playerId)
    {
        network.DisconnectPeer(playerId);
    }
}