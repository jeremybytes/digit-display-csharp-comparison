namespace digits;

public abstract class Classifier
{
    protected Classifier(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
    public abstract Prediction Predict(Record input);
}

public class Classifier<TAlgo> : Classifier where TAlgo : struct, IAlgo
{
    public Record[] TrainingData { get; }
    
    public Classifier(string name, Record[] trainingData) : base(name)
    {
        TrainingData = trainingData;
    }

    public override Prediction Predict(Record input)
    {
        TAlgo algo = default;
        int[] inputImage = input.Image;
        int best_total = int.MaxValue;
        Record best = new(0, new int[0]);

        foreach (Record candidate in TrainingData)
        {
            int total = 0;
            int[] candidateImage = candidate.Image;

            for (int i = 0; i < inputImage.Length; i++)
            {
                int diff = algo.Calculate(inputImage[i], candidateImage[i]);
                total += diff;
            }
            if (total < best_total)
            {
                best_total = total;
                best = candidate;
            }
        }

        return new Prediction(input, best);
    }
}

public interface IAlgo
{
    int Calculate(int input, int test);
}

public struct ManhattanAlgo : IAlgo
{
    public int Calculate(int input, int test)
    {
        return Math.Abs(input - test);
    }
}

public struct EuclideanAlgo : IAlgo
{
    public int Calculate(int input, int test)
    {
        var diff = input - test;
        return diff * diff;
    }
}
