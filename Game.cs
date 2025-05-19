using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShelterSim
{
    class Game
    {
        private List<Pet> pets = new List<Pet>();

        public async Task RunAsync()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                Console.Write("Seçiminiz: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AdoptPet();
                        break;
                    case "2":
                        await UseItemAsync();
                        break;
                    case "3":
                        ShowPets();
                        break;
                    case "4":
                        Console.WriteLine("Proje Sahibi: Ömer Eren Oğan - 225040101");
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }

                Console.WriteLine("\nDevam etmek için Enter'a basın...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== BARINAK GÖREVLİSİ OYUNU ===");
            Console.WriteLine("1. Hayvan Sahiplen");
            Console.WriteLine("2. Eşya Kullan");
            Console.WriteLine("3. Hayvanları Görüntüle");
            Console.WriteLine("4. Yapımcıyı Görüntüle");
            Console.WriteLine("5. Oyundan Çık");
        }

        private void AdoptPet()
        {
            Console.WriteLine("Sahiplenilecek hayvanı seçin: 1. Köpek 2. Kedi 3. Kuş");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
            {
                PetType type = (PetType)(choice - 1);
                string name = type switch
                {
                    PetType.Dog => "Karabaş",
                    PetType.Cat => "Mırmır",
                    PetType.Bird => "Maviş",
                    _ => "Hayvan"
                };

                pets.Add(new Pet(name, type));
                Console.WriteLine($"{name} sahiplendi!");
            }
            else
            {
                Console.WriteLine("Geçersiz hayvan türü.");
            }
        }

        private void ShowPets()
        {
            if (!pets.Any())
            {
                Console.WriteLine("Henüz hayvan yok.");
                return;
            }

            foreach (var pet in pets.ToList())
            {
                if (!pet.IsAlive)
                {
                    Console.WriteLine($"{pet.Name} öldü...");
                    pets.Remove(pet);
                    continue;
                }

                Console.WriteLine($"🐾 {pet.Name} ({pet.Type}) - Açlık: {pet.Hunger}, Uyku: {pet.Sleep}, Eğlence: {pet.Fun}");
            }
        }

        private async Task UseItemAsync()
        {
            if (!pets.Any())
            {
                Console.WriteLine("Hayvan yok.");
                return;
            }

            Console.WriteLine("Eşya kullanılacak hayvanı seçin:");
            for (int i = 0; i < pets.Count; i++)
                Console.WriteLine($"{i + 1}. {pets[i].Name}");

            if (!int.TryParse(Console.ReadLine(), out int petIndex) || petIndex < 1 || petIndex > pets.Count)
            {
                Console.WriteLine("Geçersiz seçim.");
                return;
            }

            Pet selectedPet = pets[petIndex - 1];

            var compatibleItems = ItemDatabase.Items
                .Where(i => i.CompatiblePetType == selectedPet.Type)
                .ToList();

            Console.WriteLine("Kullanılabilir eşyalar:");
            for (int i = 0; i < compatibleItems.Count; i++)
                Console.WriteLine($"{i + 1}. {compatibleItems[i].Name}");

            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > compatibleItems.Count)
            {
                Console.WriteLine("Geçersiz eşya.");
                return;
            }

            var item = compatibleItems[itemIndex - 1];
            Console.WriteLine($"{item.Name} kullanılıyor...");
            await item.UseAsync();
            selectedPet.ApplyItem(item);
            Console.WriteLine($"{item.Name} {selectedPet.Name} üzerinde kullanıldı.");
        }
    }
}