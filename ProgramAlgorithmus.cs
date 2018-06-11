using namespaceStuktur;
using namespaceUtility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace namespaceAlgorithmus
{
    class Algorithmus
    {
        public void zeitOfAlgorithmus(string path, String methode, int startId, int endI, bool directed)
        {
            Console.WriteLine(methode);

            Algorithmus algorithmus = new Algorithmus();

            Graph graph = Parse.getGraphByFile(path, directed);

            DateTime befor = System.DateTime.Now;

            if (methode == "fordFulkerson")
            {
                algorithmus.fordFulkerson(graph, startId, endI);
            }

            DateTime after = System.DateTime.Now;
            TimeSpan ts = after.Subtract(befor);
            Console.WriteLine("\n\n{0}s \n", ts.TotalSeconds);
        }

        public void fordFulkerson(Graph graph, int startId, int endId)
        {

            List<Fluss> flussList = new List<Fluss>();

            Fluss fluss = BFS(graph,startId, endId);

            flussList.Add(fluss);

            Graph residualGraph = createResidualGraph(graph, fluss);

            double sum = 0;

            while (fluss != null)
            {
                sum = sum + fluss.capacity;

                fluss = BFS(residualGraph, startId, endId);

                if (fluss!=null)
                {
                    flussList.Add(fluss);

                    residualGraph = createResidualGraph(residualGraph, fluss);
                }
                
            }

          
            Console.WriteLine("sum:" + Math.Round(sum, 6));

        }

        public Graph createResidualGraph(Graph graph, Fluss fluss)
        {
            foreach (Edge e in graph.edgeList.ToArray())
            {
                if (fluss.edgeList.Contains(e))
                {
                    e.capacity = e.capacity - fluss.capacity;

                    if (e.capacity == 0)
                    {
                        graph.removeEdge(e);
                    }
                    graph.createOrUpdateEdge(e.endNode, e.startNode, fluss.capacity);

                }
            }

            return graph;
        }


        public Fluss BFS(Graph graph, int startId, int endId)
        {
            graph.reset();
           
            Queue<Node> queue = new Queue<Node>();

            Node node = graph.nodeList[startId];
            node.visited = true;
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                node = queue.Dequeue();

                foreach (Node n in node.nodeList)
                {
                    if (!n.visited)
                    {
                        queue.Enqueue(n);
                        n.visited = true;
                        n.previousNode = node;
                    }
                }
            }

            Fluss fluss = getflussFromID(graph, endId);
            return fluss;
        }

        public Fluss getflussFromID(Graph graph, int endId)
        {
            List<Edge> edges = new List<Edge>();
            findVater(graph.nodeList[endId], graph, edges);

            Fluss fluss = null;
            if (edges != null && edges.Count > 0)
            {
                fluss = new Fluss(edges);
            }
            return fluss;
        }


        public void findVater(Node node, Graph graph, List<Edge> edges)
        {
            if (node.previousNode != null)
            {
                Edge e = graph.findEdge(node.previousNode, node);
                edges.Add(e);

                findVater(node.previousNode, graph, edges);
            }
            else
            {
                return;
            }
        }
    }
}