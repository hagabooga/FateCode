using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using static Godot.GD;
using Utility;
using System.Linq;

namespace Server
{
    public class PlayerVerification : EzNode
    {
        public event Handlers.PlayerId verificationFailed;

        readonly Timer verificationExpiration = new Timer();
        readonly Timer tokenExpiration = new Timer();

        private readonly StateProcessing stateProcessing;

        public Dictionary AwaitingVerification { get; } = new Dictionary();
        public Dictionary ExpectedTokens { get; } = new Dictionary();

        public PlayerVerification(StateProcessing stateProcessing)
        {
            this.stateProcessing = stateProcessing;
        }

        public void Start(int playerId)
        {
            Print("Starting verification.");
            Dictionary timeStamp = new Dictionary();
            timeStamp["timeStamp"] = OS.GetUnixTime();
            AwaitingVerification[playerId] = timeStamp;
            RpcId(playerId, "FetchToken");
        }

        public async void Verify(int playerId, string token)
        {
            var result = Error.Failed;
            string username = "";
            while (OS.GetUnixTime() - ulong.Parse(token.Right(64)) <= 30)
            {
                if (ExpectedTokens.Contains(token))
                {
                    result = Error.Ok;
                    Print($"{playerId} successful verification.");
                    // Create player container
                    username = (string)ExpectedTokens[token];
                    AwaitingVerification.Remove(playerId);
                    ExpectedTokens.Remove(playerId);
                    break;
                }
                await Task.Delay(1000);
            }
            ReturnTokenVerificationResults(playerId, result, username);
            if (result != Error.Ok)
            {
                AwaitingVerification.Remove(playerId);
                verificationFailed?.Invoke(playerId);
            }
        }

        public void ReturnTokenVerificationResults(int playerId, Error result, string username)
        {
            if (result == Error.Ok)
            {
                stateProcessing.ConnectedPlayers[playerId] = username;
                Print($"Spawning player {username} {playerId}");
                // INFO NEEDS TO BE GET FROM DATABASE
                // print(username)
                // var basic = database.player_basics.select(username)
                // var scene_to_load = Enums.SCENE_TEST_MAP
                // print(basic)
                // if not basic:
                // 	scene_to_load = Enums.SCENE_CREATE_CHARACTER
                // 	rpc_id(
                // 		player_id,
                // 		"return_token_verification_results",
                // 		ERR_DOES_NOT_EXIST,
                // 		state_processing.logged_in_players,
                // 		scene_to_load
                // 	)
                // else:
                // 	basic.loc = Vector2.ONE * (randi() % 100 - 50)
                // 	spawn_player(player_id, database.player_basics.select(username), result)
                RpcId(playerId,
                      nameof(Client.PlayerVerification.ReceiveTokenVerificationResult),
                      result,
                      stateProcessing.LoggedInPlayers);

            }
            else
            {
                RpcId(playerId,
                      nameof(Client.PlayerVerification.ReceiveTokenVerificationResult),
                      result,
                      null);
            }
        }

        public void VerificationExpirationTimeout()
        {
            if (AwaitingVerification.IsEmpty())
            {
                return;
            }
            var currentTime = OS.GetUnixTime();
            AwaitingVerification.Keys.Cast<int>().ForEach(playerId =>
            {
                var startTime = (ulong)((Dictionary)AwaitingVerification[playerId])["timeStamp"];
                if (currentTime - startTime >= 30)
                {
                    AwaitingVerification.Remove(playerId);
                    var connectedPeers = GetTree().GetNetworkConnectedPeers();
                    if (connectedPeers.Contains(playerId))
                    {
                        ReturnTokenVerificationResults(playerId, Error.Failed, null);
                        verificationFailed?.Invoke(playerId);
                    }
                }
            });
            Print($"Awaiting verifications: {AwaitingVerification}");
        }

        public void TokenExpirationTimeout()
        {
            if (ExpectedTokens.IsEmpty())
            {
                return;
            }
            var currentTime = OS.GetUnixTime();
            ExpectedTokens.Keys.Cast<string>().ForEach(token =>
            {
                var tokenTime = ulong.Parse(token.Right(64));
                if (currentTime - tokenTime >= 30)
                {
                    ExpectedTokens.Remove(token);
                }
            });
        }

        [Remote]
        public void ReceiveToken(string token)
        {
            var playerId = GetTree().GetRpcSenderId();
            Verify(playerId, token);
            Print($"Verifying {playerId} with token {token}.");
        }

        [Remote]
        public void ReceiveCreateAccountRequest(Dictionary data)
        {
            Print("Receive create account request.");
        }

        [Remote]
        public void ClientReady()
        {
            var playerId = GetTree().GetRpcSenderId();
            Print("Client ready.");
            //     yield(get_tree().create_timer(0.0001), "timeout")
            // var stats = database.player_stats.select(state_processing.connected_players[player_id])
            // stats.loc = Vector2.ZERO
            // rpc_id(0, "spawn_player", player_id)
        }
    }
}