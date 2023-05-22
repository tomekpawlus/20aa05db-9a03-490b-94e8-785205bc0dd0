using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces
{
    public interface ITreeNodeService
    {
        Task<Node> CreateNodeAsync(string treeName, int parentNodeId, string nodeName);
        Task DeleteNodeAsync(string treeName, int nodeId);
        Task RenameNodeAsync(string treeName, int nodeId, string newName);
    }
}
