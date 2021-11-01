using System;
using System.Linq;

namespace digits
{
    class Program
    {
        static void Main(string[] args)
        {
            var (training, validation) = FileLoader.GetData("train.csv", 1000, 3000);
            Console.WriteLine($"Training: {training.Count()} - Validation: {validation.Count()}");
        }
    }
}
