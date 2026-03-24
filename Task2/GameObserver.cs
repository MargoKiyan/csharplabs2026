namespace Task2;

class GameObserver
{
    public static void Main(string[] args)
    {
        Player player = new Player(100);

        UIHealthBar ui = new UIHealthBar();
        SoundSystem sound = new SoundSystem();
        AchievementSystem achievements = new AchievementSystem();
        GameLogger logger = new GameLogger();

        player.DamageTaken += ui.OnDamageTaken;
        player.DamageTaken += sound.OnDamageTaken;
        player.DamageTaken += achievements.OnDamageTaken;
        player.DamageTaken += logger.OnDamageTaken;

        player.TakeDamage(10);
        player.TakeDamage(30);
        player.TakeDamage(25);
        player.TakeDamage(15);
        player.TakeDamage(20);
    }
}

public class Player
{
    public delegate void DamageTakenEventHandler(int damage, int currentHP);

    public event DamageTakenEventHandler DamageTaken;

    private int _hp;

    public Player(int initialHP)
    {
        _hp = initialHP;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp < 0)
            _hp = 0;

        Console.WriteLine($"\nPlayer takes {damage} damage");

        DamageTaken?.Invoke(damage, _hp);
    }
}

public class UIHealthBar
{
    public void OnDamageTaken(int damage, int currentHP)
    {
        Console.WriteLine($"UIHealthBar: HP = {currentHP}");
    }
}

public class SoundSystem
{
    public void OnDamageTaken(int damage, int currentHP)
    {
        Console.WriteLine("SoundSystem: Play damage sound");

        if (currentHP <= 20)
        {
            Console.WriteLine("SoundSystem: CRITICAL HP sound");
        }
    }
}

public class AchievementSystem
{
    private bool halfHealthUnlocked = false;
    private bool firstDeathUnlocked = false;

    public void OnDamageTaken(int damage, int currentHP)
    {
        if (!halfHealthUnlocked && currentHP <= 50)
        {
            halfHealthUnlocked = true;
            Console.WriteLine("Achievement: Half Health");
        }
        if (!firstDeathUnlocked && currentHP <= 0)
        {
            firstDeathUnlocked = true;
            Console.WriteLine("Achievement: First Death");
        }
    }
}

public class GameLogger
{
    public void OnDamageTaken(int damage, int currentHP)
    {
        Console.WriteLine($"GameLogger: Damage = {damage}, Current HP = {currentHP}");
    }
}