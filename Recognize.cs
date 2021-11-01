using System;
using System.Collections.Generic;

namespace digits
{
    public abstract class Classifier {
        public string Name { get; set; }
        public List<Record> TrainingData { get; set; }
        public abstract int Algorithm(int input, int test);

        public Prediction Predict(Record input)
        {
            int best_total = int.MaxValue;
            Record best = new Record { Value = 0, Image = new List<int>() };
            foreach (Record candidate in TrainingData)
            {
                int total = 0;
                for (int i = 0; i < 784; i++)
                {
                    int diff = Algorithm(input.Image[i], candidate.Image[i]);
                    total += diff;
                }
                if (total < best_total)
                {
                    best_total = total;
                    best = candidate;
                }
            }

            return new Prediction { Actual = input, Predicted = best };
        }
    }

    public class ManhattanClassifier: Classifier 
    {
        public ManhattanClassifier(List<Record> training_data)
        {
            this.Name = "Manhattan Classifier";
            this.TrainingData = training_data;
        }

        public override int Algorithm(int input, int test)
        {
            return Math.Abs(input - test);
        }
    }

    public class EuclideanClassifier: Classifier
    {
        public EuclideanClassifier(List<Record> training_data)
        {
            this.Name = "Euclidean Classifier";
            this.TrainingData = training_data;
        }

        public override int Algorithm(int input, int test)
        {
            var diff = input - test;
            return diff * diff;
        }
    }
}