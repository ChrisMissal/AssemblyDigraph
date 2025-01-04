namespace SampleProject.Quantum;

public class Atom
{
    public int AtomicNumber { get; set; } // Number of protons
    public string Element { get; set; }
    public List<Particle> Particles { get; set; } = new();
}

public abstract class Particle
{
    public string Name { get; set; }
    public double Mass { get; set; } // In MeV/c^2
    public double Charge { get; set; } // In elementary charge units
}

public class Proton : Particle
{
    public Proton()
    {
        Name = "Proton";
        Mass = 938.272; // MeV/c^2
        Charge = +1;
    }
}

public class Neutron : Particle
{
    public Neutron()
    {
        Name = "Neutron";
        Mass = 939.565; // MeV/c^2
        Charge = 0;
    }
}

public class Electron : Particle
{
    public Electron()
    {
        Name = "Electron";
        Mass = 0.511; // MeV/c^2
        Charge = -1;
    }
}
