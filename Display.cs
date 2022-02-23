using System.Text;

namespace digit_console;

public class Display
{
    public static void AppendImagesAsString(StringBuilder sb, int[] image1, int[] image2)
    {
        string[] first = GetImageAsStrings(image1);
        string[] second = GetImageAsStrings(image2);

        for (int i = 0; i < first.Length; i++)
        {
            sb.Append(first[i]);
            sb.Append(" | ");
            sb.Append(second[i]);
            sb.AppendLine();
        }
    }

    public static string[] GetImageAsStrings(int[] image)
    {
        List<string> lines = new();
        StringBuilder line = new();

        for (int i = 0; i < image.Length; i++)
        {
            var output_char = GetDisplayCharForPixel(image[i]);
            line.Append(output_char);
            line.Append(output_char);

            if (i % 28 == 0)
            {
                lines.Add(line.ToString());
                line.Clear();
            }
        }
        return lines.ToArray();
    }

    private static char GetDisplayCharForPixel(int pixel)
    {
        return pixel switch
        {
            > 16 and < 32 => '.',
            >= 32 and < 64 => ':',
            >= 64 and < 160 => 'o',
            >= 160 and < 224 => 'O',
            >= 224 => '@',
            _ => ' ',
        };
    }
}
