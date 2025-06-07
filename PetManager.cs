using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PetManager
{
    private List<Pet> pets = new List<Pet>();

    public void AdoptPet(Pet pet)
    {
        pets.Add(pet);
        pet.StatChanged += OnPetStatChanged;
        Console.WriteLine($"{pet.Name} was successfully adopted!");
    }

    public void ShowAllPets()
    {
        if (!pets.Any())
        {
            Console.WriteLine("No pets adopted yet.");
            return;
        }

        foreach (var pet in pets)
        {
            Console.WriteLine($"🐾 {pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}");
        }
    }

    public async Task UseItemOnPetAsync()
    {
        if (!pets.Any())
        {
            Console.WriteLine("No pets available.");
            return;
        }

        Console.WriteLine("Select a pet to use an item on:");
        for (int i = 0; i < pets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pets[i].Name}");
        }

        if (!int.TryParse(Console.ReadLine(), out int petChoice) || petChoice < 1 || petChoice > pets.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Pet selectedPet = pets[petChoice - 1];

        var compatibleItems = ItemDatabase.AllItems
            .Where(i => i.CompatiblePetType == null || i.CompatiblePetType == selectedPet.Type)
            .ToList();

        Console.WriteLine("Available items:");
        for (int i = 0; i < compatibleItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {compatibleItems[i].Name}");
        }

        if (!int.TryParse(Console.ReadLine(), out int itemChoice) || itemChoice < 1 || itemChoice > compatibleItems.Count)
        {
            Console.WriteLine("Invalid item.");
            return;
        }

        Item selectedItem = compatibleItems[itemChoice - 1];
        Console.WriteLine($"Using {selectedItem.Name}...");
        await selectedItem.UseAsync();
        selectedPet.ApplyItem(selectedItem);
        Console.WriteLine("Item used successfully!");
    }

    private void OnPetStatChanged(object sender, StatChangedEventArgs e)
    {
        if (!e.Pet.IsAlive)
        {
            Console.WriteLine($"{e.Pet.Name} has passed away...");
            pets.Remove(e.Pet);
        }
    }
}