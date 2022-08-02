using Godot;
using SimpleInjector;
using Utility;
using static Godot.GD;
using System.Reflection;
using System.Linq;

namespace Client
{
    public class GameServer : EzClient<GameServer>
    {
        [Export] public string Token { get; set; }

        public GameServer(ClientOptions<GameServer> options) :
                          base(options, false)
        {
        }

        public void ConnectToServer(string ipOverride = null)
        {
            CreateClient(ipOverride);
            GetTree().NetworkPeer = network;
        }

    }
}