// Data Collector class
// This is the model/subject class
// Uses specific observer interfaces for each data
// Adds flexibility, closed to modification, open to extension
class DataCollector
{
    private double temperature;
    private double pH;
    private List<ITempObserver> tempObservers = new List<ITempObserver>();
    private List<IPHObserver> pHObservers = new List<IPHObserver>();

    // ==== Temperature observer methods ==== //
    public void AddTempObserver(ITempObserver observer)
    {
        tempObservers.Add(observer);
    }

    public void RemoveTempObserver(ITempObserver observer)
    {
        tempObservers.Remove(observer);
    }

    private void NotifyTempObservers()
    {
        /**
        /* This is where the inversion of control happens, the subject calls the observers and not the opposite.
        /* Normally, if this was implemented as a library, the observers would call subject's methods to get the data.
        /* Same happens on NotifyPHObservers, but for PH data.
        **/
        foreach (var observer in tempObservers)
        {
            observer.UpdateTemperature(temperature);
        }
    }

    public void SetTemperature(double temp)
    {
        temperature = temp;
        NotifyTempObservers();
    }

    // ==== PH observer methods ==== //
    public void AddPHObserver(IPHObserver observer)
    {
        pHObservers.Add(observer);
    }

    public void RemovePHObserver(IPHObserver observer)
    {
        pHObservers.Remove(observer);
    }

    public void NotifyPHObservers()
    {
        foreach (var observer in pHObservers)
        {
            observer.UpdatePH(pH);
        }
    }

    public void SetPH(double pHValue)
    {
        pH = pHValue;
        NotifyPHObservers();
    }

}

// Temperature observer interface
interface ITempObserver
{
    void UpdateTemperature(double temp);
}

// PH observer interface
interface IPHObserver
{
    void UpdatePH(double pH);
}

// UNIFESP observes both temperature and PH, so it implements both interfaces
class UNIFESPObserver : ITempObserver, IPHObserver
{
    public void UpdateTemperature(double temp)
    {
        Console.WriteLine($"UNIFESP Observer: Temperature updated to {temp}°C");
    }

    public void UpdatePH(double pH)
    {
        Console.WriteLine($"UNIFESP Observer: PH updated to {pH}");
    }
}

// UFRGS only observes temperature, so it implements only the temperature observer interface
class UFRGSObserver : ITempObserver
{
    public void UpdateTemperature(double temp)
    {
        Console.WriteLine($"UFRGS Observer: Temperature updated to {temp}°C");
    }
}

// USP only observes pH, so it implements only the pH observer interface
class USPObserver : IPHObserver
{
    public void UpdatePH(double pH)
    {
        Console.WriteLine($"USP Observer: PH updated to {pH}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Observer Pattern Demo: Data Collector ===\n");

        // Subject
        DataCollector collector = new DataCollector();

        // Observers
        UNIFESPObserver unifesp = new UNIFESPObserver();
        UFRGSObserver ufrgs = new UFRGSObserver();
        USPObserver usp = new USPObserver();

        Console.WriteLine("Registering observers...");
        collector.AddTempObserver(unifesp);
        collector.AddPHObserver(unifesp);
        collector.AddTempObserver(ufrgs);
        collector.AddPHObserver(usp);

        Console.WriteLine("\nPublishing first set of measurements:");
        collector.SetTemperature(23.5);
        collector.SetPH(7.1);

        Console.WriteLine("\nRemoving UFRGS from temperature notifications...");
        collector.RemoveTempObserver(ufrgs);

        Console.WriteLine("\nPublishing second set of measurements:");
        collector.SetTemperature(25.0);
        collector.SetPH(6.8);

        Console.WriteLine("\nDemo finished.");
    }
}