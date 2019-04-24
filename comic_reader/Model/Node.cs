using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comic_reader.Model
{
    public abstract class Node
    {
        protected Node(string name, Node parent, params Node[] childs)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Parent = parent;
            this.Childs = childs;
            this.MetaData = new Dictionary<string, string>();
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public Node Parent { get; set; }
        public Node[] Childs { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
    }
}
