public class DoubleDataGenerator
{
    private Random _random = new Random();

    public async Task GenerateDoubles(int numberOfDoubles, double min, double max, string fileName)
    {
        List<string> lines = new List<string>(numberOfDoubles);
        for (int i = 0; i < numberOfDoubles; i++)
        {
            double multiplier = _random.NextDouble() * (max - min) + min;
            //double value = _random.NextDouble() * multiplier;
            //double value = Math.Round(_random.NextDouble() * (_random.Next(0, 2) == 0 ? -1 : 1) * 1000, decimalPlaces);
            lines.Add(multiplier.ToString());
        }

        await File.WriteAllLinesAsync(fileName, lines);
    }
}
