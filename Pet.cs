using System;
using System.Threading.Tasks;

public class Pet
{
    public string Name { get; }
    public PetType Type { get; }
    public int Hunger { get; private set; } = 50;
    public int Sleep { get; private set; } = 50;
    public int Fun { get; private set; } = 50;

    public event EventHandler<StatChangedEventArgs> StatChanged;

    public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

    public Pet(string name, PetType type)
    {
        Name = name;
        Type = type;
        StartDecay();
    }

    private async void StartDecay()
    {
        while (IsAlive)
        {
            await Task.Delay(5000);
            Hunger--;
            Sleep--;
            Fun--;
            StatChanged?.Invoke(this, new StatChangedEventArgs(this));
        }
    }

    public void ApplyItem(Item item)
    {
        Hunger = Math.Min(Hunger + item.HungerBoost, 100);
        Sleep = Math.Min(Sleep + item.SleepBoost, 100);
        Fun = Math.Min(Fun + item.FunBoost, 100);
    }
}