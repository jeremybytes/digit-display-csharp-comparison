using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace digits 
{
    public class FileLoader 
    {
        public static (List<Record>, List<Record>) GetData(string filename, int offset, int count) 
        {
            var contents = File.ReadAllLines(filename).Where(s => s.Trim().Length > 0).Skip(1);
            List<Record> data = new();
            foreach (string line in contents) 
            {
                var split = SplitRawData(line);
                var record = ParseRecord(split);
                data.Add(record);
            }

            var (training, validation) = SplitDataSets(data, offset, count);
            return (training, validation);
        }

        private static List<int> SplitRawData(string data) 
        {
            List<int> results = new();
            var items = data.Split(',');
            foreach (var item in items) 
            {
                if (!int.TryParse(item, out int i))
                {
                    continue;
                }
                results.Add(i);
            }
            return results;
        }

        private static Record ParseRecord(List<int> data) 
        {
            var value = data[0];
            List<int> image = new();
            foreach(int item in data.Skip(1))
            {
                image.Add(item);
            }

            return new Record() { Value = value, Image = image };
        }

        private static (List<Record>, List<Record>) SplitDataSets(List<Record> data, int offset, int count)
        {
            var training = data.GetRange(0, offset);
            training.AddRange(data.GetRange(offset+count, (data.Count() - offset - count)));
            var validation = data.GetRange(offset, count);
            return (training, validation);
        }
    }
}