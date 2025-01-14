Console.WriteLine("Simple Calculator!");

Console.WriteLine("Please enter first number:");
string firstString = Console.ReadLine();
int firstNumber = int.Parse(firstString);

Console.WriteLine("Please enter second number:");
string secondString = Console.ReadLine();
int secondNumber = int.Parse(secondString);

Console.WriteLine("Please Enter the action...(enter fist letter)");
Console.WriteLine("[A]dd");
Console.WriteLine("[S]ubtract");
Console.WriteLine("[M]ultiply");

string userAction = Console.ReadLine();

var sign = "";
var result = 0;

if (userAction == "A" || userAction == "a") {
    sign = "+";    
    result = Add(firstNumber, secondNumber);
} else if (userAction == "S" || userAction == "s"){
    sign = "-";    
    result = Subtract(firstNumber, secondNumber);
} else if (userAction == "M" || userAction == "m"){
    sign = "*";    
    result = Multiply(firstNumber, secondNumber);
} else {
    Console.WriteLine("Invalid Option ..");    
    CloseApp();
    return;
};

Console.WriteLine(firstNumber + sign + secondNumber + " = " + result);

int Add(int a, int b) { return a + b; };
int Subtract(int a, int b) { return a - b; };
int Multiply(int a, int b) { return a * b; };

void CloseApp(){
    Console.WriteLine("Please click on any key to close the program...");
    Console.ReadKey();
};

CloseApp();