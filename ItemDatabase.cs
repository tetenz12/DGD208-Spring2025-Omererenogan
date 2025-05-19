using System.Collections.Generic;

namespace PetShelterSim
{
    class ItemDatabase
    {
        public static List<Item> Items = new List<Item>
        {
            new Item { Name = "Mama Kabı", HungerBoost = 20, Duration = 2, CompatiblePetType = PetType.Dog },
            new Item { Name = "Fare Oyuncağı", FunBoost = 15, Duration = 1, CompatiblePetType = PetType.Cat },
            new Item { Name = "Kuş Yemi", HungerBoost = 10, Duration = 2, CompatiblePetType = PetType.Bird },
            new Item { Name = "Tünek", SleepBoost = 25, Duration = 3, CompatiblePetType = PetType.Bird }
        };
    }
}