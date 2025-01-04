namespace SampleApp.Calculators;

public class EnergyCalculator
{
    private const double PlanckConstant = 6.62607015e-34; // In Joule-seconds

    /// <summary>
    /// Computes the energy of a photon given its frequency.
    /// </summary>
    /// <param name="frequency">The photon's frequency in Hz.</param>
    /// <returns>The energy in Joules.</returns>
    public double ComputePhotonEnergy(double frequency)
    {
        return PlanckConstant * frequency;
    }

    /// <summary>
    /// Computes the energy level of an electron in a hydrogen atom.
    /// </summary>
    /// <param name="n">The principal quantum number (n).</param>
    /// <returns>The energy in Joules.</returns>
    public double ComputeHydrogenEnergyLevel(int n)
    {
        const double RydbergConstant = 2.18e-18; // In Joules
        return -RydbergConstant / (n * n);
    }
}
