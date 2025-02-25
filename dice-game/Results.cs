

public class Results
{
    public void showResultMessage(bool result, bool lastChance = false) {
        if (result == true)
        {
            Console.WriteLine("You won the game");
        }
        else
        {
            if (lastChance)
            {
                Console.WriteLine("You Lost");
            }
            else {
                Console.WriteLine("Wrong number");
            }
        }
    }
} 