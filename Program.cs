using namespaceAlgorithmus;
using System;

namespace FordFulkerson
{
    class Program
    {

        static void Main(string[] args)
        {
            Algorithmus algorithmus = new Algorithmus();
            algorithmus.zeitOfAlgorithmus(@"../../fluss/Fluss.txt", "fordFulkerson",0,7, true);
            algorithmus.zeitOfAlgorithmus(@"../../fluss/Fluss2.txt", "fordFulkerson",0,7, true);
            algorithmus.zeitOfAlgorithmus(@"../../fluss/G_1_2.txt", "fordFulkerson",0,7, true);


            Console.WriteLine("\n");
            Console.ReadLine();
        }

    }
}
