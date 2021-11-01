using System.Collections.Generic;

namespace digits
{
    public class Display 
    {
        public static string GetImagesAsString(List<int> image1, List<int> image2)
        {
            var first_image = GetImageAsString(image1);
            var first = first_image.Split("\n");
            var second_image = GetImageAsString(image2);
            var second = second_image.Split("\n");
            string result = "";
            for (int i = 0; i < 28; i++) 
            {
                result += first[i];
                result += " | ";
                result += second[i];
                result += "\n";
            }
            return result;
        }

        public static string GetImageAsString(List<int> image)
        {
            string result = "";
            int count = 0;
            foreach(int pixel in image) {
                if (count % 28 == 0 && count != 0)
                {
                    result += "\n";
                }
                var output_char = GetDisplayCharForPixel(pixel);
                result += output_char;
                result += output_char;
                count++;
            }
            result += "\n";
            return result;
        }

        private static char GetDisplayCharForPixel(int pixel)
        {
            switch (pixel)
            {
                case var low when low > 16 && low < 32:
                    return '.';
                case var mid when mid >= 32 && mid < 64:
                    return ':';
                case var high when high >= 64 && high < 160:
                    return 'o';
                case var reallyHigh when reallyHigh >= 160 && reallyHigh < 224:
                    return 'O';
                case var reallyReallyHigh when reallyReallyHigh >= 224:
                    return '@';
                default:
                    return ' ';
            }
        }
    }
}