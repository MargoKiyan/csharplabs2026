namespace Task1;

class SmartHomeObserver
{
    public static void Main(string[] args)
    {
        TemperatureSensor sensor = new TemperatureSensor();

        Display display = new Display();
        AirConditioner ac = new AirConditioner();
        SecuritySystem security = new SecuritySystem();

        sensor.TemperatureChanged += display.OnTemperatureChanged;
        sensor.TemperatureChanged += ac.OnTemperatureChanged;
        sensor.TemperatureChanged += security.OnTemperatureChanged;

        sensor.SetTemperature(15);
        sensor.SetTemperature(20);
        sensor.SetTemperature(27);
        sensor.SetTemperature(42);
        sensor.SetTemperature(3);
    }
}

public class TemperatureSensor
{
    public delegate void TemperatureChangedEventHandler(double temperature);

    public event TemperatureChangedEventHandler TemperatureChanged;

    private double _temperature;

    public void SetTemperature(double temperature)
    {
        _temperature = temperature;
        Console.WriteLine($"\nTemperature changed to {_temperature}°C");

        TemperatureChanged?.Invoke(_temperature);
    }
}

public class Display
{
    public void OnTemperatureChanged(double temperature)
    {
        Console.WriteLine($"Display: Current temperature is {temperature}°C");
    }
}

public class AirConditioner
{
    public void OnTemperatureChanged(double temperature)
    {
        if (temperature < 17)
        {
            Console.WriteLine("AirConditioner: Heating ON");
        }
        else if (temperature <= 25)
        {
            Console.WriteLine("AirConditioner: AC OFF");
        }
        else
        {
            Console.WriteLine("AirConditioner: Cooling ON");
        }
    }
}

public class SecuritySystem
{
    public void OnTemperatureChanged(double temperature)
    {
        if (temperature > 40)
        {
            Console.WriteLine("SecuritySystem: WARNING! Overheating detected!");
        }
        if (temperature < 5)
        {
            Console.WriteLine("SecuritySystem: WARNING! Freezing risk detected!");
        }
    }
}