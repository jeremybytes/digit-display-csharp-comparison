namespace digits;

public abstract class Classifier
{
    public string Name { get; set; }
    public Record[] TrainingData { get; set; }
    public abstract int Algorithm(int input, int test);

    public Classifier(string name, Record[] trainingData)
    {
        Name = name;
        TrainingData = trainingData;
    }

    public Prediction Predict(Record input)
    {
        int[] inputImage = input.Image;
        int best_total = int.MaxValue;
        Record best = new(0, new int[0]);
        foreach (Record candidate in TrainingData)
        {
            int total = 0;
            int[] candidateImage = candidate.Image;
            for (int i = 0; i < 784; i++)
            {
                int diff = Algorithm(inputImage[i], candidateImage[i]);
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

public class ManhattanClassifier : Classifier
{
    public ManhattanClassifier(Record[] training_data) :
        base("Manhattan Classifier", training_data)
    {
    }

    public override int Algorithm(int input, int test)
    {
        return Math.Abs(input - test);
    }
}

public class EuclideanClassifier : Classifier
{
    public EuclideanClassifier(Record[] training_data)
        : base("Euclidean Classifier", training_data)
    {
    }

    public override int Algorithm(int input, int test)
    {
        var diff = input - test;
        return diff * diff;
    }
}
