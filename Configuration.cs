using CommandLine;

namespace digits
{
    public class Configuration
    {
        [Option(shortName: 'o', longName: "offset", Required = false, HelpText = "Offset in the data set (default: 1000)", Default = 1000)]
        public int Offset { get; set; }

        [Option(shortName: 'c', longName: "count", Required = false, HelpText = "Number of records to process (default: 100)", Default = 100)]
        public int Count { get; set; }

        [Option(longName: "classifier", Required = false, HelpText = "Classifier to use (default: 'euclidean')", Default = "euclidean")]
        public string Classifier { get; set; }

        [Option(shortName: 't', longName: "tasks", Required = false, HelpText = "Number of tasks to use (default: 6)", Default = 6)]
        public int Tasks { get; set; }
    }
}