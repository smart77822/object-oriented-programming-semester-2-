using System;
using System.Text;

namespace bank_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Use Emoji
            Console.OutputEncoding = new UTF8Encoding(false);
            Console.InputEncoding = Encoding.UTF8;
            // TITLE NAME OF PROJECT
            Console.Title = "Bank Account Management System";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            bank bank = new bank();
            bank.start();
        }
    }
}
