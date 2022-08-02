using Godot;
using SimpleInjector;
using Utility;
using static Godot.GD;
using System.Reflection;
using System.Linq;
using Godot.Collections;

namespace Client
{
    public class PlayerVerification : EzNode
    {
        private readonly GameServer server;

        public PlayerVerification(GameServer server)
        {
            this.server = server;
        }

        [Remote]
        public void FetchToken()
        {
            RpcId(1, nameof(Server.PlayerVerification.ReceiveToken), server.Token);
        }

        [Remote]
        public void ReceiveTokenVerificationResult(Error result, Array loggedInPlayers)
        {
            if (result == Error.Ok)
            {
                GetTree().ChangeScene("res://FateCode/Client/Maps/TestMap.tscn");
            }
            else if (result == Error.Failed)
            {
                Print("Token verificaiton results failed. Try again!");
            }
            else if (result == Error.DoesNotExist)
            {
                Print("Account creation.");
            }
        }
    }
}