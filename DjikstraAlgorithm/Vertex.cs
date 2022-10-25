using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjikstraAlgorithm
{
    internal class Vertex
    {
        public string Name;
        public Dictionary<int, int> Edges { get; } = new Dictionary<int, int>();
        public int Distance = int.MaxValue;
        public bool Visited = false;
        public int Previous;

        public Vertex(string name) { Name = name; }
    }
}
