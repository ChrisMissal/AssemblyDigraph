namespace SampleProject.Calculators;

public class ParticleInteractionCalculator
{
    /// <summary>
    /// Computes the total energy after a particle collision.
    /// </summary>
    /// <param name="mass1">Mass of the first particle (kg).</param>
    /// <param name="velocity1">Velocity of the first particle (m/s).</param>
    /// <param name="mass2">Mass of the second particle (kg).</param>
    /// <param name="velocity2">Velocity of the second particle (m/s).</param>
    /// <returns>Total kinetic energy in Joules.</returns>
    public double ComputeCollisionEnergy(double mass1, double velocity1, double mass2, double velocity2)
    {
        var kineticEnergy1 = 0.5 * mass1 * velocity1 * velocity1;
        var kineticEnergy2 = 0.5 * mass2 * velocity2 * velocity2;
        return kineticEnergy1 + kineticEnergy2;
    }

    /// <summary>
    /// Computes the decay time of a particle given its lifetime.
    /// </summary>
    /// <param name="lifetime">The particle's mean lifetime in seconds.</param>
    /// <returns>A random decay time based on an exponential distribution.</returns>
    public double ComputeDecayTime(double lifetime)
    {
        var random = new Random();
        return -lifetime * Math.Log(random.NextDouble());
    }
}
