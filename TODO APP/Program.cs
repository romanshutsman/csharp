
string userOption;
List<string> todos = new List<string>();

do
{
    InitApp();
} while (!CheckIfValidOption(userOption));

void InitApp()
{
    PrintOptions();
    userOption = Console.ReadLine().ToUpper();
    PrintMessage(userOption);
    if (CheckIfValidOption(userOption)) {
        HandleOption(userOption);
    };
};

void PrintOptions()
{
    Console.WriteLine("");
    Console.WriteLine("What do you want to do?");

    Console.WriteLine("[S]ee all todos!");
    Console.WriteLine("[A]dd todo!");
    Console.WriteLine("[R]emove todo!");
    Console.WriteLine("[E]xit");
};

void PrintMessage(string? option)
{
    if (option == null)
    {
        Console.WriteLine("Cannot be empty");
    }

    if (!CheckIfValidOption(option))
    {
        Console.WriteLine("Invalid option.");
    }

};

bool CheckIfValidOption(string? option)
{
    return new List<string> { "S", "A", "R", "E" }.Contains(option);

};

void HandleOption(string option) {
    if (option == "S") ShowAllTodos();
    if (option == "A") AddNewTodo();
    if (option == "R") RemoveTodo();
    if (option == "E") Exit();
}

void ShowAllTodos()
{
    for (var i = 0; i < todos.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {todos[i]}");
    }
    if (todos.Count == 0) {
        Console.WriteLine("No ToDos.");
    }
    InitApp();
}
void AddNewTodo()
{
    Console.WriteLine("Please enter a name of new ToDO");

    var newToDo = Console.ReadLine();
    if (todos.Contains(newToDo))
    {
        Console.WriteLine($"{newToDo} alreadyExist. Please Enter a new Name.");
        AddNewTodo();
    }
    else {
        todos.Add(newToDo);
        Console.WriteLine("Added.");
        InitApp();
    }
}
void RemoveTodo()
{
    if (todos.Count == 0)
    {
        Console.WriteLine("No ToDos to Remove.");
        InitApp();
    }
    else
    {
        Console.WriteLine("Please enter an index of todo to remove: ");
       
        var stringIndex = Console.ReadLine();
        int.TryParse(stringIndex, out int index);
        if (index < 1 || todos.Count < index)
        {
            Console.WriteLine("Wrong Index");
            RemoveTodo();
        } else
        {
            todos.RemoveAt(index - 1);
            Console.WriteLine("Removed.");
            InitApp();
        }
    }

}
void Exit()
{
    Environment.Exit(0);
}

Console.ReadKey();