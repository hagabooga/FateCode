using Godot;

namespace Handlers
{
    public delegate void Message(string msg);
    public delegate void Result(Error result);
    public delegate void ResultToken(Error result, string token);
    public delegate void CurrentMax(int current, int max);
    public delegate void PlayerId(int playerId);
    public delegate void Id(int id);
    public delegate void IdResultToken(int playerId, Error result, string token);
    public delegate void IdResult(int playerId, Error result);
}