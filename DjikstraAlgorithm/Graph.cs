using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DjikstraAlgorithm
{
    internal class Graph
    {
        public Vertex[] V;
        public void Djikstra(int start, OutputFile output)
        {
            int current = start;
            V[current].Distance = 0;
            Visit(current);
            while (true)
            {
                output.WriteIteration(current, V);
                current = NextNode();
                if (current == -1) break;
                Visit(current);
            }
        }
        private void Visit(int current)
        {
            Vertex u = V[current];
            Vertex v;
            u.Visited = true;
            foreach (KeyValuePair<int, int> edge in u.Edges)
            {
                v = V[edge.Key];
                if (!v.Visited && u.Distance + edge.Value < v.Distance)
                {
                    v.Distance = u.Distance + edge.Value;
                    v.Previous = current;
                }
            }
        }
        private int NextNode()
        {
            int minDistance = int.MaxValue;
            int nextNode = -1;
            for (int i = 0; i < V.Length; i++)
            {
                if (!V[i].Visited && V[i].Distance < minDistance)
                {
                    minDistance = V[i].Distance;
                    nextNode = i;
                }
            }
            return nextNode;
        }
    }
}
