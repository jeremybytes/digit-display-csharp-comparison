namespace digits;

public class FileLoader
{
    public static (Record[], Record[]) GetData(string filename, int offset, int count)
    {
        var contents = File.ReadLines(filename).Where(s => s.AsSpan().Trim().Length > 0).Skip(1);
        List<Record> data = new();
        foreach (string line in contents)
        {
            var split = SplitRawData(line);
            var record = new Record(split[0], split[1..]);
            data.Add(record);
        }

        var (training, validation) = SplitDataSets(data, offset, count);
        return (training.ToArray(), validation.ToArray());
    }

    private static int[] SplitRawData(string data)
    {
        List<int> results = new();
        int pos = 0;
        ReadOnlySpan<char> slice;

        while ((slice = Next(data, ',', ref pos)).Length > 0)
        {
            if (int.TryParse(slice, out int i))
            {
                results.Add(i);
            }
        }
        return results.ToArray();
    }

    private static (List<Record>, List<Record>) SplitDataSets(List<Record> data, int offset, int count)
    {
        var training = data.GetRange(0, offset);
        training.AddRange(data.GetRange(offset + count, (data.Count - offset - count)));
        var validation = data.GetRange(offset, count);
        return (training, validation);
    }

    public static List<List<Record>> ChunkData(List<Record> data, int chunks)
    {
        List<List<Record>> results = new();
        var chunk_size = data.Count / chunks;
        var remainder = data.Count % chunks;
        for (int i = 0; i < chunks; i++)
        {
            if (i != chunks - 1)
            {
                var chunk = data.GetRange(i * chunk_size, chunk_size);
                results.Add(chunk);
            }
            else
            {
                var chunk = data.GetRange(i * chunk_size, chunk_size + remainder);
                results.Add(chunk);
            }
        }
        return results;
    }

    private static ReadOnlySpan<char> Next(ReadOnlySpan<char> input, char sep, ref int pos)
    {
    Start:
        var slice = input[pos..];

        int idx = slice.IndexOf(sep);
        if (idx >= 0)
        {
            pos += idx + 1;
            if (idx == 0)
                goto Start;
            return slice[..idx];
        }
        pos = input.Length;
        return slice;
    }
}
