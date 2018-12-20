public enum NoiseLevel
{
    None, Low, Mid, Hig
}

public interface IHearable
{
    NoiseLevel GetNoiseLevel();
}

