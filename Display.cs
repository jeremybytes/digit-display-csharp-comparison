using System.Buffers;
using System.Runtime.InteropServices;

namespace digit_console;

public class Display
{
    public static void GetImagesChars(int[] image1, int[] image2, Span<char> span)
    {

        var first = image1.AsSpan();
        var second = image2.AsSpan();

        for (int i = 0; i < 28; i++)
        {
            foreach (var pixel in first.Slice(0, 28))
            {
                span[0] = span[1] = GetDisplayCharForPixel(pixel);
                span = span.Slice(2);
            }

            first = first.Slice(28);

            span[0] = ' ';
            span[1] = '|';
            span[2] = ' ';
            span = span.Slice(3);

            foreach (var pixel in second.Slice(0, 28))
            {
                span[0] = span[1] = GetDisplayCharForPixel(pixel);
                span = span.Slice(2);
            }

            second = second.Slice(28);

            span[0] = '\n';
            span = span.Slice(1);
        }
    }

    private static char GetDisplayCharForPixel(int pixel)
    {
        return pixel switch
        {
            var low when low > 16 && low < 32 => '.',
            var mid when mid >= 32 && mid < 64 => ':',
            var high when high >= 64 && high < 160 => 'o',
            var reallyHigh when reallyHigh >= 160 && reallyHigh < 224 => 'O',
            var reallyReallyHigh when reallyReallyHigh >= 224 => '@',
            _ => ' ',
        };
    }
}
