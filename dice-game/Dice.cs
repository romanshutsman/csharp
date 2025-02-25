
public class Dice
{
    public int Roll()
    {
        
        Random randomator = new Random();
        int dice = randomator.Next(1, 7);
        Console.WriteLine("Rolled Dice...");
        return dice;
    }
}