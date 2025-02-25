

public class Game
{
    private const int GAME_TRIES = 3;
    public void Play()
    {
        Console.WriteLine("Game Started...");
        var rolledDice = new Dice().Roll();
        runGameNTimes(rolledDice);
    }


    private void runGameNTimes(int rolledDice) {

        for (int i = 1; i <= GAME_TRIES; i++)
        {
            bool gameResult = playOnce(rolledDice, i == GAME_TRIES);
            if (gameResult == true) break;
        }
    }

    private bool playOnce(int rolledDice, bool lastChance)
    {
        var prompts = new Prompt();
        prompts.Show();
        var result = new GuessDice(rolledDice, prompts.EnteredValue).Guess();
        new Results().showResultMessage(result, lastChance);
        return result;
    }
}
