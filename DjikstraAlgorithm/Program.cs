using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjikstraAlgorithm
{
    internal class Program
    {
        static Graph graph = new Graph();
        static InputFile input = new InputFile("C:/Djikstra/RuteIndonesia.xlsx");
        static OutputFile output = new OutputFile("C:/Djikstra/Hasil1.xlsx");
        static void Main(string[] args)
        {
            graph.V = input.GetVertexData(37, 33);
            graph.Djikstra(0, output);
            output.WriteResult(0, graph.V);
            Console.ReadLine();
        }
    }
}
