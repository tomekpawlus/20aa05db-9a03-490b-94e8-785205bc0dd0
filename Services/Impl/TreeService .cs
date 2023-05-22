using _20aa05db_9a03_490b_94e8_785205bc0dd0.AppDb;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity;
using _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Interfaces;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Services.Impl
{
    public class TreeService : ITreeService
    {
        private readonly AppDbContext _dbContext;

        public TreeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Node> GetTree(string treeName)
        {
            var tree = _dbContext.Nodes.Where(n => n.TreeName == treeName).ToList();

            if (tree.Count == 0)
            {
                tree = CreateTree(treeName);
            }

            return tree;
        }

        private List<Node> CreateTree(string treeName)
        {

            var rootNode = new Node { Name = "Root", TreeName = treeName };


            _dbContext.Nodes.AddRange(new[] { rootNode });
            _dbContext.SaveChanges();

            return new List<Node> { rootNode };
        }
    }
}
