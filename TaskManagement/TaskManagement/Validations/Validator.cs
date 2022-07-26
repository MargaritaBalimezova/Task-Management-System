using System;

public static class Validator
{
    private static string StringLenErrorMsg = "{0} must be between {1} and {2} characters!";
    private static string NullErrorMsg = "{0} cannot be null value!";

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

    public static void ValidateArgumentIsNotNull(object arg, string field)
    {
        if (arg == null)
        {
            throw new ArgumentException(string.Format(NullErrorMsg, field));
        }
    }

    public static void ValidateStringLength(string toValidate, int minLen, int maxLen, string field)
    {
        if(toValidate.Length < minLen || toValidate.Length > maxLen)
        {
            throw new ArgumentException(string.Format(StringLenErrorMsg, field, minLen, maxLen));
        }
    }
}