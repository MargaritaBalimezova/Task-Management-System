using TaskManagement.Exceptions;
using TaskManagement.Validations;

public static class Validator
{
    public static void ValidateIntRange(int value, int min, int max, string message)
    {
        if (value < min || value > max)
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateDoubleRange(double value, double min, double max, string message)
    {
        if (value < min || value > max)
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateArgumentIsNotNull(object arg, string field)
    {
        if (arg == null)
        {
            throw new InvalidUserInputException(string.Format(Constants.NULL_ERROR_MSG, field));
        }
    }

    public static void ValidateStringLength(string toValidate, int minLen, int maxLen, string field)
    {
        if (toValidate.Length < minLen || toValidate.Length > maxLen)
        {
            throw new InvalidUserInputException(string.Format(Constants.STRING_LEN_ERROR_MSG, field, minLen, maxLen));
        }
    }
}