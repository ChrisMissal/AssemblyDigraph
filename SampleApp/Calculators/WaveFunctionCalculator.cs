using System.Numerics;

namespace SampleApp.Calculators;

public class WaveFunctionCalculator
{
    /// <summary>
    /// Computes the probability of finding a particle in a specific state.
    /// </summary>
    /// <param name="waveFunction">The wave function (complex amplitude).</param>
    /// <returns>The probability as a double.</returns>
    public double ComputeProbability(Complex waveFunction)
    {
        // Probability is the square of the amplitude's magnitude
        return waveFunction.Magnitude * waveFunction.Magnitude;
    }

    /// <summary>
    /// Normalizes a wave function to ensure its total probability equals 1.
    /// </summary>
    /// <param name="waveFunctions">Array of wave functions.</param>
    /// <returns>Normalized wave functions.</returns>
    public Complex[] Normalize(Complex[] waveFunctions)
    {
        var total = waveFunctions.Sum(wf => wf.Magnitude * wf.Magnitude);
        return waveFunctions.Select(wf => wf / Math.Sqrt(total)).ToArray();
    }
}
