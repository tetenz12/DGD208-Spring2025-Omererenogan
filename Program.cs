using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        ShelterGame game = new ShelterGame();
        await game.RunAsync();
    }
}