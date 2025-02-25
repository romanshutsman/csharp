public class GuessDice
{
    private int _rolledDice;
    private int _ussersGuess;
    public GuessDice(int rolledDice, int usersGuess)
    {
        _rolledDice = rolledDice;
        _ussersGuess = usersGuess;
    }

    public bool Guess() { return _rolledDice == _ussersGuess; }
}
