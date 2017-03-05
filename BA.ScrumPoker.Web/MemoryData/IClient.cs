using BA.ScrumPoker.Entities;

namespace BA.ScrumPoker.MemoryData
{
    public interface IClient
    {
        Client Join(string userName, string roomId);
        Client Get(string roomId, int clientId);
        Client Vote(string roomId, int clientId, int voteValue);
    }
}