using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using static Godot.GD;
using Utility;

public class StateProcessing : EzNode
{

    readonly Dictionary
        connectedPlayers = new Dictionary(),
        loggedInPlayers = new Dictionary(),
        playerStates = new Dictionary();

    Dictionary worldState = new Dictionary();
    ulong syncClockCounter = 0;

    public Dictionary ConnectedPlayers => connectedPlayers;
    public Dictionary LoggedInPlayers => loggedInPlayers;
    public Dictionary PlayerStates => playerStates;

    public StateProcessing()
    {

    }

    public override void _PhysicsProcess(float delta)
    {
        if (++syncClockCounter != 3)
        {
            return;
        }
        syncClockCounter = 0;
        if (!PlayerStates.IsEmpty())
        {
            worldState = PlayerStates.Duplicate(true);
            worldState.ForEach(palyerId =>
            {
                ((Dictionary)worldState[palyerId]).Remove("t");
                worldState["t"] = OS.GetSystemTimeMsecs();
                // worldState["enemies"] = map.enemydatas
                // Verifications
                // Anti-Cheat
                // Cuts
                // Physics checks
                // Anything else you have to do
                RpcUnreliableId(0, "ReceiveWorldState", worldState);
            });
        }
    }

    public void PlayerDisconnected(int playerId)
    {
        RpcId(0, "PlayerDisconnected", playerId);
    }

    public void DespawnPlayer(int playerId)
    {
        RpcId(0, "DespawnPlayer", playerId);
    }

    [Remote]
    void ReceivePlayerState(Dictionary playerState)
    {
        var playerId = GetTree().GetRpcSenderId();
        Print(playerState);
        if (PlayerStates.Contains(playerId))
        {
            if ((int)((Dictionary)PlayerStates[playerId])["t"] < (int)playerState["t"])
            {
                PlayerStates[playerId] = playerState;
            }
        }
        else
        {
            PlayerStates[playerId] = playerState;
        }
    }
}