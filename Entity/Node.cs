using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Entity
{
    public class Node
    {
        public int Id { get; set; }
        public string? TreeName { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Node? Parent { get; set; }
        public List<Node>? Children { get; set; }
    }
}
