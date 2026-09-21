using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter any number to sum :");
            int num = int.Parse(Console.ReadLine());
            int sum = 0;
            while(num != -1)
            {
                sum += num;
                Console.Write("Enter Number  (-1 to exit loop ): ");
                num = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("The total sum is {0}", sum);
            Console.Read();
        }
    }
}
