using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventOfCode
{
    class Program
    {
        static List<int[]> split = new List<int[]>();
        static List<int> infiniteOnwers = new List<int>() {-1};
        static int[] upperCorner = new int[] { 1000, 1000 };
        static int[] lowerCorner = new int[] { 0, 0 };

        public class Point
        {
            List<int> distances = new List<int>();

            public int x;
            public int y;
            public int owner = -2;

            public Point(int x,int y)
            {
                this.x = x;
                this.y = y;

                foreach(int[] ia in split)
                {
                    distances.Add(Math.Abs(x - ia[0]) + Math.Abs(y - ia[1]));
                }

                if (!distances.Any(t => distances.Count(z => t == z) == 2))
                    owner = distances.FindIndex(t => t == distances.Min());
                else
                    owner = -1;

                if ((x < upperCorner[0] || y < upperCorner[1] || x > lowerCorner[0] || y > lowerCorner[1]) && !infiniteOnwers.Contains(owner))
                {
                    infiniteOnwers.Add(owner);
                }
            }
                       
        }

        static void part1()
        {
            string[] input = File.ReadAllLines(@"D:/weBored.txt");
            split = input.Select(x => x.Split(',').Select(y => int.Parse(y)).ToArray()).ToList();
            List<Point> plane = new List<Point>();
            int[] biggest = new int[split.Count];


            for (int i = 0; i < biggest.Length; i++)
            {
                biggest[i] = 0;
            }
            for (int i = 0; i < split.Count; i++)
            {
                if (upperCorner[0] > split[i][0])
                    upperCorner[0] = split[i][0];
                if (upperCorner[1] > split[i][1])
                    upperCorner[1] = split[i][1];
                if (lowerCorner[0] < split[i][0])
                    lowerCorner[0] = split[i][0];
                if (lowerCorner[1] < split[i][1])
                    lowerCorner[1] = split[i][1];
            }
            for (int i = upperCorner[1] - 1; i <= lowerCorner[1] + 1; i++)
            {
                for (int x = upperCorner[0] - 1; x <= lowerCorner[0] + 1; x++)
                {
                    plane.Add(new Point(x, i));
                }
            }

            foreach (Point p in plane.Where(x => !infiniteOnwers.Contains(x.owner)))
            {
                biggest[p.owner]++;
            }


            foreach (int[] ia in split)
            {
                Console.WriteLine($"x : {ia[0]}\ty: {ia[1]}");
            }

            Console.Write("\nInfinite owners :");
            foreach (int i in infiniteOnwers.OrderBy(x => x))
            {

                Console.Write($" {i}");
            }

            Console.WriteLine($"\n\nUpper :{upperCorner[0] + "," + upperCorner[1]}\tLower : {lowerCorner[0] + "," + lowerCorner[1]} ");
            Console.WriteLine($"\nBiggest plane : {biggest.Max()}");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            part1();
        }
    }
}
