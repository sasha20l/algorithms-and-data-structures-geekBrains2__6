using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekBrain2_6
{
    class Program
    {
        public static Point[] points = new Point[10];
        public static Rebro[] rebra = new Rebro[9];
        public static Point BeginPoint = new Point();



        static void Main(string[] args)
        {
            int name = 1;
            Random rnd = new Random();
            points[0] = new Point(9999, false, "A");
            points[1] = new Point(9999, false, "B");
            points[1].predPoint = points[0];
            points[2] = new Point(9999, false, "C");
            points[2].predPoint = points[1];
            points[3] = new Point(9999, false, "D");
            points[3].predPoint = points[2];
            points[4] = new Point(9999, false, "E");
            points[4].predPoint = points[2];
            points[5] = new Point(9999, false, "D2");
            points[5].predPoint = points[4];
            points[6] = new Point(9999, false, "C2");
            points[6].predPoint = points[0];
            points[7] = new Point(9999, false, "E2");
            points[7].predPoint = points[0];
            points[8] = new Point(9999, false, "C3");
            points[8].predPoint = points[7];
            points[9] = new Point(9999, false, "D3");
            points[9].predPoint = points[7];

            rebra[0] = new Rebro(points[0], points[1], 2);
            rebra[1] = new Rebro(points[1], points[2], 1);
            rebra[2] = new Rebro(points[2], points[3], 5);
            rebra[3] = new Rebro(points[2], points[4], 1);
            rebra[4] = new Rebro(points[4], points[5], 3);
            rebra[5] = new Rebro(points[0], points[6], 5);
            rebra[6] = new Rebro(points[0], points[7], 7);
            rebra[7] = new Rebro(points[7], points[8], 1);
            rebra[8] = new Rebro(points[7], points[9], 3);

            BeginPoint = points[0];

            DekstraAlgorim Deks = new DekstraAlgorim(points, rebra);
            Deks.AlgoritmRun(BeginPoint);
            Deks.OneStep(points[0]);
            Deks.MinPath1(points[3]);

            List<String> myList = PrintGrath.PrintAllPoints(Deks);
            List<String> myList2 = PrintGrath.PrintAllMinPaths(Deks);

            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < myList2.Count; i++)
            {
                Console.WriteLine(myList2[i]);
            }

            Console.ReadLine();
        }
    }
            
    class DekstraException : ApplicationException
    {
        public DekstraException(string message) : base(message)
        {

        }
    }
    static class PrintGrath
    {
        public static List<string> PrintAllPoints(DekstraAlgorim da)
        {
            List<string> retListOfPoints = new List<string>();
            foreach (Point p in da.points)
            {
                retListOfPoints.Add(string.Format("point name={0}, point value={1}, predok={2}", p.Name, p.ValueMetka, p.predPoint.Name ?? "нет предка"));
            }
            return retListOfPoints;
        }
        public static List<string> PrintAllMinPaths(DekstraAlgorim da)
        {
            List<string> retListOfPointsAndPaths = new List<string>();
            foreach (Point p in da.points)
            {

                if (p != da.BeginPoint)
                {
                    string s = string.Empty;
                    foreach (Point p1 in da.MinPath1(p))
                    {
                        s += string.Format("{0} ", p1.Name);
                    }
                    retListOfPointsAndPaths.Add(string.Format("Point ={0},MinPath from {1} = {2}", p.Name, da.BeginPoint.Name, s));
                }

            }
            return retListOfPointsAndPaths;

        }
    }

    class Point
    {
        public float ValueMetka { get; set; }
        public string Name { get; private set; }
        public bool IsChecked { get; set; }
        public Point predPoint { get; set; }
        public object SomeObj { get; set; }
        public Point(int value, bool ischecked)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            predPoint = new Point();
        }
        public Point(int value, bool ischecked, string name)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            Name = name;
            predPoint = new Point();
        }
        public Point()
        {
        }
    }
    class Rebro
    {
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public float Weight { get; private set; }

        public Rebro(Point first, Point second, float valueOfWeight)
        {
            FirstPoint = first;
            SecondPoint = second;
            Weight = valueOfWeight;
        }
    }
    class DekstraAlgorim
    {

        public Point[] points { get; private set; }
        public Rebro[] rebra { get; private set; }
        public Point BeginPoint { get; private set; }

        public DekstraAlgorim(Point[] pointsOfgrath, Rebro[] rebraOfgrath)
        {
            points = pointsOfgrath;
            rebra = rebraOfgrath;
        }
        public void AlgoritmRun(Point beginp)
        {
            if (this.points.Count() == 0 || this.rebra.Count() == 0)
            {
                throw new DekstraException("Массив вершин или ребер не задан!");
            }
            else
            {
                BeginPoint = beginp;
                OneStep(beginp);
                foreach (Point point in points)
                {
                    Point anotherP = GetAnotherUncheckedPoint();
                    if (anotherP != null)
                    {
                        OneStep(anotherP);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public void OneStep(Point beginpoint)
        {
            foreach (Point nextp in Pred(beginpoint))
            {
                if (nextp.IsChecked == false)//не отмечена
                {
                    float newmetka = beginpoint.ValueMetka + GetMyRebro(nextp, beginpoint).Weight;
                    if (nextp.ValueMetka > newmetka)
                    {
                        nextp.ValueMetka = newmetka;
                        nextp.predPoint = beginpoint;
                    }
                    else
                    {

                    }
                }
            }
            beginpoint.IsChecked = true;//вычеркиваем
        }
        private IEnumerable<Point> Pred(Point currpoint)
        {
            IEnumerable<Point> firstpoints = from ff in rebra where ff.FirstPoint == currpoint select ff.SecondPoint;
            IEnumerable<Point> secondpoints = from sp in rebra where sp.SecondPoint == currpoint select sp.FirstPoint;
            IEnumerable<Point> totalpoints = firstpoints.Concat<Point>(secondpoints);
            return totalpoints;
        }
        private Rebro GetMyRebro(Point a, Point b)
        {//ищем ребро по 2 точкам
            IEnumerable<Rebro> myr = from reb in rebra where (reb.FirstPoint == a & reb.SecondPoint == b) || (reb.SecondPoint == a & reb.FirstPoint == b) select reb;
            if (myr.Count() > 1 || myr.Count() == 0)
            {
                throw new DekstraException("Не найдено ребро между соседями!");
            }
            else
            {
                return myr.First();
            }
        }
        private Point GetAnotherUncheckedPoint()
        {
            IEnumerable<Point> pointsuncheck = from p in points where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().ValueMetka;
                Point minPoint = pointsuncheck.First();
                foreach (Point p in pointsuncheck)
                {
                    if (p.ValueMetka < minVal)
                    {
                        minVal = p.ValueMetka;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }
        public List<Point> MinPath1(Point end)
        {
            List<Point> listOfpoints = new List<Point>();
            Point tempp = new Point();
            tempp = end;
            while (tempp != this.BeginPoint)
            {
                listOfpoints.Add(tempp);
                tempp = tempp.predPoint;
            }
            return listOfpoints;
        }
    }
}