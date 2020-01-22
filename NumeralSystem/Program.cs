using System;

namespace NumericalSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            string newInput = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("Hello ! Enter your number : ");
                    newInput = Console.ReadLine();
                    newNumericalConverter newConverter = new newNumericalConverter(newInput);
                    Console.WriteLine(newConverter.PrintDecimalForm);
                    Console.WriteLine(newConverter.PrintBinaryForm);
                    Console.WriteLine(newConverter.PrintOctalForm);
                    Console.WriteLine(newConverter.PrintHexForm);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}