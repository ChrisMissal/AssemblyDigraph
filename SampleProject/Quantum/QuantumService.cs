using SampleProject.Quantum;
using SampleProject.Calculators;
using System.Numerics;

namespace SampleProject.QuantumServices;

public class QuantumService
{
    private readonly WaveFunctionCalculator _waveFunctionCalculator;
    private readonly EnergyCalculator _energyCalculator;
    private readonly ParticleInteractionCalculator _particleInteractionCalculator;

    public QuantumService()
    {
        _waveFunctionCalculator = new WaveFunctionCalculator();
        _energyCalculator = new EnergyCalculator();
        _particleInteractionCalculator = new ParticleInteractionCalculator();
    }

    /// <summary>
    /// Computes the probability of finding a particle in a specific state.
    /// </summary>
    /// <param name="waveFunction">The wave function of the particle.</param>
    /// <returns>The probability of the particle being in the state.</returns>
    public double ComputeParticleStateProbability(Complex waveFunction)
    {
        return _waveFunctionCalculator.ComputeProbability(waveFunction);
    }

    /// <summary>
    /// Computes the energy level of an electron in an atom for a given quantum number.
    /// </summary>
    /// <param name="atom">The atom (currently only supports Hydrogen-like atoms).</param>
    /// <param name="quantumNumber">The principal quantum number (n).</param>
    /// <returns>The energy level in Joules.</returns>
    public double ComputeEnergyLevel(Atom atom, int quantumNumber)
    {
        if (atom.Element != "Hydrogen")
            throw new NotSupportedException("Only Hydrogen-like atoms are supported for energy level calculations.");

        return _energyCalculator.ComputeHydrogenEnergyLevel(quantumNumber);
    }

    /// <summary>
    /// Computes the energy of a photon emitted or absorbed during an energy level transition.
    /// </summary>
    /// <param name="initialQuantumNumber">Initial energy level (n_i).</param>
    /// <param name="finalQuantumNumber">Final energy level (n_f).</param>
    /// <returns>The photon energy in Joules.</returns>
    public double ComputePhotonEnergyTransition(int initialQuantumNumber, int finalQuantumNumber)
    {
        var initialEnergy = _energyCalculator.ComputeHydrogenEnergyLevel(initialQuantumNumber);
        var finalEnergy = _energyCalculator.ComputeHydrogenEnergyLevel(finalQuantumNumber);

        return Math.Abs(initialEnergy - finalEnergy); // Energy is positive
    }

    /// <summary>
    /// Simulates a collision between two particles and computes the total energy.
    /// </summary>
    /// <param name="particle1">The first particle.</param>
    /// <param name="velocity1">The velocity of the first particle (m/s).</param>
    /// <param name="particle2">The second particle.</param>
    /// <param name="velocity2">The velocity of the second particle (m/s).</param>
    /// <returns>The total kinetic energy in Joules.</returns>
    public double SimulateCollision(Particle particle1, double velocity1, Particle particle2, double velocity2)
    {
        return _particleInteractionCalculator.ComputeCollisionEnergy(particle1.Mass, velocity1, particle2.Mass, velocity2);
    }

    /// <summary>
    /// Simulates the decay of a particle and computes its decay time.
    /// </summary>
    /// <param name="particle">The particle undergoing decay.</param>
    /// <param name="lifetime">The mean lifetime of the particle in seconds.</param>
    /// <returns>A random decay time in seconds.</returns>
    public double SimulateParticleDecay(Particle particle, double lifetime)
    {
        return _particleInteractionCalculator.ComputeDecayTime(lifetime);
    }

    /// <summary>
    /// Normalizes a set of wave functions for a system of particles.
    /// </summary>
    /// <param name="waveFunctions">An array of wave functions.</param>
    /// <returns>The normalized wave functions.</returns>
    public Complex[] NormalizeWaveFunctions(Complex[] waveFunctions)
    {
        return _waveFunctionCalculator.Normalize(waveFunctions);
    }

    /// <summary>
    /// Lists all particles in an atom and their respective properties.
    /// </summary>
    /// <param name="atom">The atom to analyze.</param>
    /// <returns>A list of particle descriptions.</returns>
    public IEnumerable<string> ListAtomParticles(Atom atom)
    {
        return atom.Particles.Select(p =>
            $"{p.Name}: Mass={p.Mass} MeV/c^2, Charge={p.Charge}");
    }
}
