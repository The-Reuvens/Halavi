using System;

public class OMath
{
    public static readonly Random rnd = new();

    /// <summary>
    /// Method <c>RandomDouble</c> generates an <c>int</c> between <c>min</c> and <c>max</c> (inclusive)
    /// </summary>
    public static int RandomInt(int min, int max)
    {
        return (rnd.Next() * (max - min)) - max;
    }

    /// <summary>
    /// Method <c>RandomDouble</c> generates a <c>double</c> between <c>min</c> and <c>max</c> (inclusive)
    /// </summary>
    public static double RandomDouble(double min, double max)
    {
        return (rnd.NextDouble() * (max - min)) - max;
    }

    /// <summary>
    /// Method <c>RangeInterpolation</c> interpolates between <c>leftValue</c> and <c>rightValue</c> according to a given value
    /// (not inclusive)
    /// </summary>
    public static double RangeInterpolation(double leftValue, double rightValue, double distance, double horizontalShift = 0, double inflectionFactor = 1)
    {
        return (leftValue - rightValue) / (1 + Math.Exp(inflectionFactor * distance + horizontalShift)) + rightValue;
    }
}
