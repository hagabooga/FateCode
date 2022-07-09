using Godot;

namespace Handlers
{
    public delegate void Message(string msg);
    public delegate void Result(Error result);
    public delegate void ResultToken(Error result, string token);
}