using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1._12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fnum, snum, tnum;
            Console.Write("Enter First Numbers:");
            fnum = int.Parse(Console.ReadLine());
            Console.Write("Enter Secon Numbers:");
            snum = int.Parse(Console.ReadLine());
            Console.Write("Enter Third Numbers:");
            tnum = int.Parse(Console.ReadLine());
            if (fnum > snum && fnum > tnum)
            {
                Console.WriteLine("First Number is Greater");
            }
            if (snum > fnum && snum > tnum)
            {
                Console.WriteLine("Second Number is Greater");
            }
        
            if (tnum > fnum && tnum > snum)
            {
                Console.WriteLine("Third Number is Greater");

            }

            
        }
    }
}
