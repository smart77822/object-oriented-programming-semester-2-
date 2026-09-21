using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            float length;
            float area;
            Console.Write("Enter the Length: ");
            input = Console.ReadLine();
            length = float.Parse(input);
            area = length * length;
            Console.WriteLine("Area is: ");
            Console.WriteLine(area);
            Console.Read();
        }
    }
}
