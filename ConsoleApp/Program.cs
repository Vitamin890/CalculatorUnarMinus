using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console calculator with unary minus. Exemple -2+13-(2*2)/3");
            Console.Write("Write expression -> ");
            string str = Console.ReadLine();

            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Bye!");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string value = Calculator.Calculate(str);
            Console.WriteLine("Answer: {0}", value);
            Console.ReadKey();
        }
    }
    
}
