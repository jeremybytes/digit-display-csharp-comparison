namespace digits
{
    public record Record (int Value, List<int> Image);
    public record Prediction (Record Actual, Record Predicted);
}