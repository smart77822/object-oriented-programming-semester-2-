using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            float num;
            Console.Write("Enter a float Number: ");
            input = Console.ReadLine();
            num = float.Parse(input);
            Console.WriteLine(num);
            Console.Read();
        }
    }
}
