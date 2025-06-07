using System;

public class StatChangedEventArgs : EventArgs
{
    public Pet Pet { get; }

    public StatChangedEventArgs(Pet pet)
    {
        Pet = pet;
    }
}