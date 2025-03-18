using dbConnection.Data;
using dbConnection.Models;

DataContextDapper dapper = new DataContextDapper();
DataContextEF entityFramwork = new DataContextEF();

string sqlQuerySelectDate = "SELECT GETDATE()";
DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlQuerySelectDate);

Console.WriteLine(rightNow);

Computer myComputer = new Computer()
{
    Motherboard = "Z6910",
    HasWifi = true,
    HasLTE = false,
    ReleaseDate = DateTime.Now,
    Price = 3342.3242m,
    Videocard = "RTX 2061"
};

entityFramwork.Add(myComputer);
entityFramwork.SaveChanges();

string sqlInsertComputer = @"INSERT INTO TutorialAppSchema.Computer(
    Motherboard,
    HasWifi,
    HasLTE,
    ReleaseDate,
    Price,
    Videocard
) VALUES ('" + myComputer.Motherboard
    + "','" + myComputer.HasWifi
    + "','" + myComputer.HasLTE
    + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
    + "','" + myComputer.Price
    + "','" + myComputer.Videocard
+ "')";
Console.WriteLine(sqlInsertComputer);

// int result = databaseConn.Execute(sqlInsertComputer);


string sqlSelectComputers = @"
SELECT
    Computer.ComputerId,
    Computer.Motherboard,
    Computer.HasWifi,
    Computer.HasLTE,
    Computer.ReleaseDate,
    Computer.Price,
    Computer.Videocard
FROM TutorialAppSchema.Computer
";

IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelectComputers);
IEnumerable<Computer>? computersEF = entityFramwork.Computer?.ToList<Computer>();
// List<Computer> computers = databaseConn.Query<Computer>(sqlSelectComputers).ToList();

foreach (Computer singleComputer in computers)
{
    Console.WriteLine("'" +
        singleComputer.ComputerId
    + "','" + singleComputer.Motherboard
    + "','" + singleComputer.HasWifi
    + "','" + singleComputer.HasLTE
    + "','" + singleComputer.ReleaseDate
    + "','" + singleComputer.Price
    + "','" + singleComputer.Videocard
    + "'" );
}