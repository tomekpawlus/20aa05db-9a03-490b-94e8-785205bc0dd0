using _20aa05db_9a03_490b_94e8_785205bc0dd0.AppDb;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Exceptions;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Impl
{
    public class TreeNodeService : ITreeNodeService
    {
        private readonly AppDbContext _dbContext;
        private readonly IJournalService _journalService;

        public TreeNodeService(AppDbContext dbContext, IJournalService journalService)
        {
            _dbContext = dbContext;
            _journalService = journalService;
        }

        public async Task<Node> CreateNodeAsync(string treeName, int parentNodeId, string nodeName)
    {
            var eventId = Guid.NewGuid().ToString();

            var parentNode = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.Id == parentNodeId);
        if (parentNode == null)
        {
            throw new SecureException("Invalid parent node ID", eventId);
        }

        var existingNode = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.ParentId == parentNodeId && n.Name == nodeName);
        if (existingNode != null)
        {
            throw new SecureException("Node name must be unique across siblings", eventId);
        }

        var newNode = new Node { TreeName = treeName, ParentId = parentNodeId, Name = nodeName };
        _dbContext.Nodes.Add(newNode);
        await _dbContext.SaveChangesAsync();
        return newNode;
    }

        public async Task DeleteNodeAsync(string treeName, int nodeId)
        {
            var node = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.Id == nodeId);
            if (node == null)
            {
                var exceptionMessage = $"Node with ID {nodeId} not found.";
                throw new Exception(exceptionMessage);
            }

            if (node.Children != null && node.Children.Count > 0)
            {
                var exceptionMessage = "You have to delete all children nodes first";
                var eventId = Guid.NewGuid().ToString();
                var exception = new SecureException(exceptionMessage, eventId);
                _journalService.LogException(null, null, exception.StackTrace);
                throw exception;
            }

            _dbContext.Nodes.Remove(node);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RenameNodeAsync(string treeName, int nodeId, string newName)
        {
            var node = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.Id == nodeId);
            if (node == null)
            {
                var exceptionMessage = $"Node with ID {nodeId} not found.";
                throw new Exception(exceptionMessage);
            }

            var parentNode = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.Id == node.ParentId);
            if (parentNode != null)
            {
                var existingNode = _dbContext.Nodes.FirstOrDefault(n => n.TreeName == treeName && n.ParentId == node.ParentId && n.Name == newName && n.Id != node.Id);
                if (existingNode != null)
                {
                    var exceptionMessage = "Node name must be unique across siblings.";
                    var eventId = Guid.NewGuid().ToString();
                    throw new SecureException(exceptionMessage, eventId);
                }
            }

            node.Name = newName;
            await _dbContext.SaveChangesAsync();
        }
    }
}
