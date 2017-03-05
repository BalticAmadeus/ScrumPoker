using BA.ScrumPoker.Entities;

namespace BA.ScrumPoker.MemoryData
{
    public interface IRoom
    {
        Room Create();
        Room Get(string roomId, string secretKey);
        Room StartVoting(string roomId, string secretKey);
        Room StopVoting(string roomId, string secretKey);
        bool CanVote(string roomId);
        void Kick(string roomId, string secretKey, int clientId);
    }
}
