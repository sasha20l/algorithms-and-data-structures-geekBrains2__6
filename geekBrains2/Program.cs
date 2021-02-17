using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekBrain2_6
{
    class Program
    {
        public static int grafN = 5;
        static void Main(string[] args)
        {
            Graf[,] mas = new Graf[grafN, grafN];
            Random rnd = new Random();
            int saveRand = -10;
            int randNums = -10;
            for (int i = 0; i < grafN; i++)
            {
                for (int j = 0; j < grafN; j++)
                {
                    while (randNums == saveRand)
                    {
                        randNums = rnd.Next(1, 10);
                    }
                    saveRand = randNums;
                    if (randNums > 10)
                    {
                        mas[i, j] = new Graf();
                        mas[i, j].weight = 0;
                        if (mas[i, j].grafNumber == -100) mas[i, j].grafNumber = i + 1;
                        if (mas[i, j].intersectionWithGraph == -100) mas[i, j].intersectionWithGraph = j + 1;
                        Console.Write("(" + mas[i, j].grafNumber + "-" + mas[i, j].intersectionWithGraph + ") " + mas[i, j].weight + "\t"); //(Отношение графа друг к другу) Вес этого отдельного графа"
                        continue;
                    }
                    mas[i, j] = new Graf();
                    mas[i, j].weight = rnd.Next(1, 10);
                    if (mas[i, j].grafNumber == -100) mas[i, j].grafNumber = i + 1;
                    if (mas[i, j].intersectionWithGraph == -100) mas[i, j].intersectionWithGraph = j + 1;
                    Console.Write("(" + mas[i, j].grafNumber + "-" + mas[i, j].intersectionWithGraph + ") " + mas[i, j].weight + "\t"); //(Отношение графа друг к другу) Вес этого отдельного графа"
                }
                Console.WriteLine();
            }

            test(mas, mas[0, 0]);
            Console.ReadLine();
        }
        public static void test(Graf[,] mas, Graf grafGeneral)
        {
            int sumWeight;
            Graf grafMinWeight;
            int sumWeightMin = 1000;
            int indexGraf = 0;

            foreach (Graf gr in mas)
            {
                if (gr == grafGeneral) break;
                indexGraf++;
            }
            for (int i = 0; i < grafN; i++)
            {
                try
                {
                    if (mas[indexGraf, i] == null || mas[indexGraf, i] == grafGeneral || mas[indexGraf, i].attended == true) continue;
                }
                catch
                {
                    break;
                }
                sumWeight = grafGeneral.weight + mas[indexGraf, i].weight;
                mas[indexGraf, i].sumWeight = sumWeight;
                mas[indexGraf, i].attended = true;
                Console.WriteLine("");
                Console.Write("(" + mas[indexGraf, i].grafNumber + "-" + mas[indexGraf, i].intersectionWithGraph + ") " + mas[indexGraf, i].sumWeight + "- общий вес до графа" + "\t"); // (Отношение графа друг к другу)Вес всех графов до этого"
                test(mas, mas[indexGraf, i]);
            }


        }
        public class Graf
        {
            public int grafNumber = -100;
            public int intersectionWithGraph = -100;
            public int sumWeight;
            public bool attended = false;
            public int weight;
        }
    }
}