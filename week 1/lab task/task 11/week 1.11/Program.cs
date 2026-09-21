using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num;
            int sum = 0;
            do
            {

                Console.Write("Enter Number: ");
                num = int.Parse(Console.ReadLine());
                sum = sum + num;
            }
            while (num != -1);
                sum = sum + 1;
                Console.WriteLine("The total sum is {0}", sum);
                Console.Read();

            
        }
    }
}
