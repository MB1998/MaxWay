using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaxWay
{
    class Program //алгоритм поиска максимального пути от вершины к основе треугольника. 
    {              //треугольник передается программе файлом input.txt
        static int[,] readInput(string inputFileWay)//cчитывает квадратную матрицу (в том числе нижнюю треугольную матрицу)
        {
            string[] str = System.IO.File.ReadAllLines(inputFileWay);
            int len = str.Length;
            int[,] matrix = new int[len,len];
            for(int i = 0; i < len; i++)
            {
                string[] s = str[i].Split(' ');
                for(int j = 0; j < s.Length; j++) { matrix[i, j] = Int32.Parse(s[j]); }
                for(int j = s.Length; j < len; j++) { matrix[i, j] = 0; }
            }
            return matrix;
        }

        static void WriteMatrix(int[,] matrix)//выводит квадратную матрицу на экран
        {
            for(int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                string s = "";
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                    s += String.Format("{0,4:##}", (matrix[i, j]).ToString()) + ' ';
                Console.WriteLine(s);
            }
        }

        static List<int> FindMaxWay(int[,] matrix)//ищет максимальный путь от вершины до основы треугольника и отдает в виде списка вершин
        {
            int countLine = (int)Math.Sqrt(matrix.Length);
            int[,] _matrix = new int[countLine, countLine];
            for (int i = 0; i < countLine; i++)
                for (int j = 0; j < countLine; j++)
                    _matrix[i, j] = matrix[i, j];
            for (int i = 1; i < countLine; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int a = _matrix[i, j] + _matrix[i - 1, j], b = (j != 0) ? (_matrix[i, j] + _matrix[i - 1, j - 1]) : a;
                    _matrix[i, j] = (a >= b) ? a : b;
                }
            }

            int maxSum = _matrix[countLine - 1, 0], maxIndex = 0;
            for (int i = 1; i < countLine; i++)
                if (_matrix[countLine - 1, i] > maxSum)
                {
                    maxSum = _matrix[countLine - 1, i];
                    maxIndex = i;
                }

            List<int> way = new List<int>();
            way.Add(matrix[countLine - 1, maxIndex]);
            for (int i = countLine - 2; i >= 0; i--)
            {
                int p1 = matrix[i, maxIndex], p2 = (maxIndex != 0) ? matrix[i, maxIndex - 1] : p1;
                maxIndex = (p1 >= p2) ? maxIndex : (maxIndex - 1);
                way.Add((p1 >= p2) ? p1 : p2);
            }

            way.Reverse();
            string s = "";
            foreach (var n in way)
                s += n + " + ";
            Console.WriteLine(s + "=" + maxSum.ToString());
            return way;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Решение для первого треугольника: ");
            List<int> way = FindMaxWay(readInput(@"input.txt"));
            Console.WriteLine("Решение для второго треугольника: ");
            List<int> way1 = FindMaxWay(readInput(@"input1.txt"));
            Console.ReadLine();
        }
    }
}
