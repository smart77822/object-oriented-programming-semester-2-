using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._5
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string input;
            int num;

            Console.Write("Enter a Number: ");
            input = Console.ReadLine();
            num = int.Parse(input);
            Console.WriteLine(num);
            Console.Read();
        }
    }
}
