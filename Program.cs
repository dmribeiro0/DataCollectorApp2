// Data Collector class
// This is the model / subject class
// Uses specific observer interfaces for each data
// Adds flexibility, closed to modification, open to extension
class DataCollector
{
    private double temperature;
    private double pH;
    private List<ITempObserver> tempObservers = new List<ITempObserver>();
    private List<IPHObserver> pHObservers = new List<IPHObserver>();

    // Temperature observer methods
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

    // PH observer methods
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