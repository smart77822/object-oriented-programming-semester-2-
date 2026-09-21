using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            float marks;
            Console.Write("Enter the Marks: ");
            input = Console.ReadLine();
            marks = float.Parse(input);
            if (marks > 50)
            {
                Console.WriteLine("You are Passed");
            }
            else
            {

                Console.WriteLine("You are Failed");
            }
            Console.Read();
        }
    }
}
