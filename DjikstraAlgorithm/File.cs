using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DjikstraAlgorithm
{
    internal class File
    {
        protected Excel.Application app;
        public Excel.Workbook Book;
        public File(string filename)
        {
            app = new Excel.Application();
            Book = app.Workbooks.Open(filename);
        }
        ~File()
        {
            app.Quit();
        }
    }
    class InputFile : File
    {
        public InputFile(string filename) : base(filename) { }
        public Vertex[] GetVertexData(int countV, int countE)
        {
            Vertex[] vertices = new Vertex[countV];
            Excel.Range cells = Book.Sheets[1].Cells;

            for(int i = 0; i < countV; i++)
            {
                vertices[i] = new Vertex(cells[i + 3, 2].Value.ToString());
            }
            for(int i = 3; i < countE+3; i++)
            {
                int u = (int)cells[i, 4].Value;
                int v = (int)cells[i, 5].Value;
                int w = (int)cells[i, 6].Value;
                vertices[u].Edges.Add(v, w);
                vertices[v].Edges.Add(u, w);
            }

            return vertices;
        }
    }
    class OutputFile : File
    {
        public OutputFile(string filename) : base(filename) { }
        
        public void WriteResult(int start, Vertex[] vertices)
        {
            Excel.Range cells = Book.Sheets[1].Cells;
            for(int i= 0; i < vertices.Length; i++)
            {
                cells[i + 2, 1] = i;
                cells[i + 2, 2] = vertices[i].Name;
                cells[i + 2, 3] = vertices[i].Distance;
                int cur = i;
                Stack<int> path = new Stack<int>();
                while (cur != start)
                {
                    path.Push(cur);
                    cur = vertices[cur].Previous;
                }
                path.Push(start);
                WritePath(i, path);
            }
        }
        private void WritePath(int destination, Stack<int> path)
        {
            Excel.Range cells = Book.Sheets[1].Cells;
            int i = 4;
            foreach (int v in path)
            {
                cells[destination + 2, i] = v;
                i++;
            }
            app.Visible = true;
            app.UserControl = true;
        }
        private int n = 3;
        public void WriteIteration(int current, Vertex[] vertices)
        {
            Excel.Range cells = Book.Sheets[2].Cells;
            int i = 3;
            cells[n, 1] = n - 3;
            cells[n, 2] = current;
            foreach(Vertex v in vertices)
            {
                if(v.Distance==int.MaxValue)
                    cells[n, i] = "INF";
                else
                    cells[n, i] = v.Distance;
                cells[n, i + 1] = v.Previous;
                i+=2;
            }
            n++;
        }
        ~OutputFile()
        {
            Book.Save();
            Book.Close();
        }

    }
}
