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
        public PlayerVerification()
        {
        }

        [Remote]
        public void FetchToken()
        {
            RpcId(1, nameof(Server.PlayerVerification.ReceiveToken));
        }

        [Remote]
        public void ReceiveTokenVerificationResult(Error result)
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