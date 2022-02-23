using digits;
using digit_console;
using System.Threading.Channels;
using CommandLine;
using System.Text;
using System.Diagnostics;

int offset = 0;
int count = 10;
string classifier_option = "";
int threads = 6;

CommandLine.Parser.Default.ParseArguments<Configuration>(args)
    .WithParsed(c =>
    {
        offset = c.Offset;
        count = c.Count;
        threads = c.Threads;
        classifier_option = c.Classifier.ToLower()
        switch
        {
            "euclidean" => "euclidean",
            "manhattan" => "manhattan",
            _ => "euclidean",
        };
    }).WithNotParsed(c =>
    {
        Environment.Exit(0);
    });

threads = Environment.ProcessorCount;

List<Prediction> errors = new();

var sw = Stopwatch.StartNew();

var (training, validation) = FileLoader.GetData("train.csv", offset, count);
Console.Clear();
Console.WriteLine($"Data Load Complete...{sw.ElapsedMilliseconds} ms");

sw.Restart();

Classifier classifier = classifier_option
    switch
{
    "manhattan" => new Classifier<ManhattanAlgo>("Manhattan Classifier", training),
    "euclidean" or _ => new Classifier<EuclideanAlgo>("Euclidean Classifier", training),
};

var channel = Channel.CreateUnbounded<Prediction>();

var listener = Listen(channel.Reader, errors);
var producer = Produce(channel.Writer, classifier, validation, threads);

await producer;
await listener;

var elapsed = sw.Elapsed;

PrintSummary(classifier, offset, count, elapsed, errors.Count);
Console.WriteLine("Press any key to show errors...");
Console.ReadKey();

foreach (var item in errors)
{
    DisplayImages(item, true);
}

PrintSummary(classifier, offset, count, elapsed, errors.Count);


static async Task Produce(ChannelWriter<Prediction> writer,
    Classifier classifier, Record[] validation, int threads)
{
    await Parallel.ForEachAsync(
        validation,
        new ParallelOptions() { MaxDegreeOfParallelism = threads },
        async (imageData, token) =>
        {
            var result = classifier.Predict(new(imageData.Value, imageData.Image));
            await writer.WriteAsync(result, token);
        });

    writer.Complete();
}

static async Task Listen(ChannelReader<Prediction> reader,
    List<Prediction> log)
{
    await foreach (Prediction prediction in reader.ReadAllAsync())
    {
        DisplayImages(prediction, false);
        if (prediction.Actual.Value != prediction.Predicted.Value)
        {
            log.Add(prediction);
        }
    }
}

static void DisplayImages(Prediction prediction, bool scroll)
{
    if (!scroll)
    {
        Console.SetCursorPosition(0, 0);
    }
    
    var output = new StringBuilder("Actual: ");
    output.Append(prediction.Actual.Value);
    output.Append(' ', 47);
    output.Append(" | Predicted: ");
    output.Append(prediction.Predicted.Value);
    output.AppendLine();
    Display.AppendImagesAsString(output, prediction.Actual.Image, prediction.Predicted.Image);
    output.AppendLine();
    output.Append('=', 115);

    Console.WriteLine(output);
}

static void PrintSummary(Classifier classifier, int offset, int count, TimeSpan elapsed, int total_errors)
{
    Console.WriteLine($"Using {classifier.Name} -- Offset: {offset}   Count: {count}");
    Console.WriteLine($"Total time: {elapsed}");
    Console.WriteLine($"Total errors: {total_errors}");
}
