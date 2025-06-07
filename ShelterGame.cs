using System;
using System.Threading.Tasks;

public class ShelterGame
{
    private PetManager petManager;

    public ShelterGame()
    {
        petManager = new PetManager();
    }

    public async Task RunAsync()
    {
        Console.Title = "Pet Shelter - Ömer Eren Oğan / 225040101";

        bool running = true;
        while (running)
        {
            ShowMenu();
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AdoptPet();
                    break;
                case "2":
                    await petManager.UseItemOnPetAsync();
                    break;
                case "3":
                    petManager.ShowAllPets();
                    break;
                case "4":
                    ShowEnding();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== PET SHELTER SIMULATOR ===");
        Console.WriteLine("Shelter Worker: Ömer Eren Oğan - 225040101");
        Console.WriteLine("1. Adopt New Pet");
        Console.WriteLine("2. Use Item");
        Console.WriteLine("3. Show Pet Statuses");
        Console.WriteLine("4. Complete Mission and Exit");
        Console.WriteLine("=============================");
    }

    private void AdoptPet()
    {
        Console.Write("Enter pet name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Select pet type:");
        Console.WriteLine("1. Dog");
        Console.WriteLine("2. Cat");
        Console.WriteLine("3. Bird");

        string input = Console.ReadLine();
        if (Enum.TryParse(input, out int typeIndex) &&
            Enum.IsDefined(typeof(PetType), typeIndex - 1))
        {
            PetType type = (PetType)(typeIndex - 1);
            Pet pet = new Pet(name, type);
            petManager.AdoptPet(pet);
        }
        else
        {
            Console.WriteLine("Invalid type.");
        }
    }

    private void ShowEnding()
    {
        Console.Clear();
        Console.WriteLine("Mission successfully completed!");
        Console.WriteLine("Final status of your pets:
");
        petManager.ShowAllPets();
        Console.WriteLine("
Press any key to exit...");
        Console.ReadKey();
    }
}