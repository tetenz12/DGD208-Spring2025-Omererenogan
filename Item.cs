using System.Threading.Tasks;

namespace PetShelterSim
{
    class Item
    {
        public string Name { get; set; }
        public int HungerBoost { get; set; }
        public int SleepBoost { get; set; }
        public int FunBoost { get; set; }
        public int Duration { get; set; }
        public PetType CompatiblePetType { get; set; }

        public async Task UseAsync()
        {
            await Task.Delay(Duration * 1000);
        }
    }
}