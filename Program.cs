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

            for (int i = 0; i < 10; i++)
            {
                var display = Display.GetImagesAsString(training[i].Image, validation[i].Image);
                Console.WriteLine(display);
            }
        }
    }
}
