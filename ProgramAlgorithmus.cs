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

           // List<Edge> edges = dijkstra(graph, startId, endId);
            List<Edge> edges = MooreBellmanFord(graph, startId, endId);

            Fluss fluss = new Fluss(edges);

            flussList.Add(fluss);

            Graph residualGraph = createResidualGraph(graph, fluss);

            while (fluss != null)
            {

                //edges = dijkstra(graph, startId, endId);
                edges = MooreBellmanFord(graph, startId, endId);

                if (edges != null&& edges.Count>0)
                {
                    fluss = new Fluss(edges);

                    flussList.Add(fluss);

                    residualGraph = createResidualGraph(graph, fluss);
                }
                else
                {
                    fluss = null;
                }
            }

            double sum = 0;

            foreach (Fluss f in flussList)
            {
                sum = sum + f.capacity;
            }

            Console.WriteLine("sum:" + sum);

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



        public List<Edge> dijkstra(Graph graph, int startId, int endId)
        {
            graph.reset();

            List<Node> visitList = new List<Node>();
            List<Node> unVisitList = new List<Node>();

            //init
            foreach (Node node in graph.nodeList)
            {
                if (node.id == startId)
                {
                    node.weight = 0;
                    visitList.Add(node);
                }
                else
                {
                    node.weight = Double.MaxValue;
                    unVisitList.Add(node);
                }
            }


            Node minNode = graph.nodeList[startId];

            bool noPath = false;

            while (minNode.id != endId)
            {
                foreach (Edge e in minNode.edgeList)
                {
                    double weight = minNode.weight + e.capacity;

                    if (weight < e.endNode.weight)
                    {
                        e.endNode.weight = weight;
                        e.endNode.previousNode = e.startNode;
                    }
                }

                minNode = findMinNode(unVisitList);

                if (minNode == null)
                {
                    noPath = true;
                    break;
                }

                visitList.Add(minNode);
                unVisitList.Remove(minNode);
            }

            if (noPath)
            {
                return null;
            }else{
                List<Edge> edges = new List<Edge>();
                findVater(graph.nodeList[endId], graph, edges);

                return edges;
            }


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

        public Node findMinNode(List<Node> unVisitList)
        {
            double min = Double.MaxValue;
            Node node = null;
            foreach (Node n in unVisitList)
            {
                if (n.weight < min)
                {
                    min = n.weight;
                    node = n;
                }
            }

            return node;
        }




        public List<Edge> MooreBellmanFord(Graph graph, int startId, int endId)
        {
            graph.reset();

            //init
            foreach (Node node in graph.nodeList)
            {
                if (node.id == startId)
                {
                    node.weight = 0;
                }
                else
                {
                    node.weight = Double.MaxValue;
                }
            }

            for (int i = 0; i < graph.nodeList.Count; i++)
            {
                foreach (Edge e in graph.edgeList)
                {
                    double weight = e.startNode.weight + e.capacity;

                    if (e.startNode.weight < double.MaxValue && weight < e.endNode.weight)
                    {
                        e.endNode.previousNode = e.startNode;
                        e.endNode.weight = weight;
                    }
                }
            }

            List<Edge> edges = new List<Edge>();
            findVater(graph.nodeList[endId], graph, edges);

            return edges;
        }
    }
}