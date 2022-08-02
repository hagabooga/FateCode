using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using static Godot.GD;
using Utility;
using System.Linq;

namespace Server
{
    public class HubConnection : EzClient<HubConnection>
    {
        readonly PlayerVerification playerVerification;

        public HubConnection(PlayerVerification playerVerification,
                             ClientOptions<HubConnection> options) :
                             base(options)
        {
            this.playerVerification = playerVerification;
        }

        [Remote]
        public void ReceiveLoginToken(string token, string username)
        {
            Print($"Receive login token: {token} {username}");
            playerVerification.ExpectedTokens[token] = username;
        }
    }
}