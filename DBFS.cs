using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixGraph
{
    class Edge {
        public Edge(int start,int finish,float weight) {
            this.start = start;
            this.finish = finish;
            this.weight = weight;
        }
        public int start;
        public int finish;
        public float weight;
    }
    class Class1
    {
        static int Main() {
            Console.WriteLine("HELLo");
            var A = buildIncedentMatrix(10);
            return 0;
        }
        static int[,] buildIncedentMatrix(int N) { 
            Random r = new Random();
            int[,] matrix = new int[N,N];
            for (int i = 0; i < N; i++) {
                //чтобы не было двойного присвоения сначала [1,2]->[2,1]
                for (int j = i+1; j < N; j++)
                {
                    matrix[j, i] = r.Next(0, 2);
                    matrix[i, j] = matrix[j, i];
                }
            }
            return matrix;
        }
        static float[,] genIncedentMatrixFromEdges(List<Edge> edges,bool oriented) {
            var maxEdgeNumber = edges.Select(t => Math.Max(t.start, t.finish)).Max();
            var result = new float[maxEdgeNumber, edges.Count];

            for (int i = 0; i < edges.Count; i++)
            {
                var edge = edges[i];
                result[edge.start - 1, i] = oriented ? -1 : edge.weight;
                result[edge.finish - 1, i] = oriented ? 1 : edge.weight;
            }

            return result;
        }
        static List<Edge> genIncedentEdges(float[,] matrix) { 
            List<Edge> result = new List<Edge> ();
            for (int i = 0; i < matrix.Length; i++) {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix[i,j]!=0)
                    {
                        result.Add(new Edge(i, j, matrix[i, j]));
                    }
                }
            }
            return result;
        }
        static bool DFS(int[,] matrix) { 
            Queue<int> q = new Queue<int>();
            bool[] visited = new bool[matrix.Length];
            q.Enqueue (0);
            visited[0] = true;
            while (q.Count != 0) { 
                int i = q.Dequeue ();
                for (int j = 0; j < matrix.Length; j++) {
                    if (matrix[i, j] != 0 && !visited[j]) { 
                        q.Enqueue (j);
                        visited[j] = true;
                    }
                }
            }
            for (int j = 0; j < matrix.Length; j++) 
                if (!visited[j])return false;
            return true;
        }
        static bool BFS(int[,] matrix) {
            bool[] visited = new bool[matrix.Length];
            BFSReq(ref matrix, ref visited, 0);
            for (int j = 0; j < matrix.Length; j++)
                if (!visited[j]) return false;
            return true;
        }
        static void BFSReq(ref int[,] matrix,ref bool[] visited,int i) {
            int n = matrix.Length;
            visited[i] = true;
            for (int j = 0; j < n; j++)
            {
                if (matrix[i,j]>0 && !visited[j])
                {
                    BFSReq(ref matrix, ref visited, j);
                }
            }
        }
    }
}
