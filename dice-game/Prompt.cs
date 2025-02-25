public class Prompt
{
    public int EnteredValue { get; private set; }

    public void Show()
    {
        Console.WriteLine("Enter a value from 1 to 6:");
        string usersInput = Console.ReadLine();
        int.TryParse(usersInput, out int EnteredValue);
    }
}
