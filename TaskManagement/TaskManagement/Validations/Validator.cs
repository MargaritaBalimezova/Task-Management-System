using System;

public static class Validator
{
    public static void ValidateIntRange(int value, int min, int max, string message)
    {
        if (value < min || value > max)
        {
            throw new ArgumentException(message);
        }
    }

    public static void ValidateDoubleRange(double value, double min, double max, string message)
    {
        if (value < min || value > max)
        {
            throw new ArgumentException(message);
        }
    }

    public static void ValidateArgumentIsNotNull(object arg, string message)
    {
        if (arg == null)
        {
            throw new ArgumentException(message);
        }
    }
}