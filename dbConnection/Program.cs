using System.Globalization;
using dbConnection.Data;
using dbConnection.Models;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();
DataContextDapper dapper = new DataContextDapper(config);
DataContextEF entityFramwork = new DataContextEF(config);

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
    VideoCard = "RTX 2061"
};

entityFramwork.Add(myComputer);
entityFramwork.SaveChanges();

string sqlInsertComputer = @"INSERT INTO TutorialAppSchema.Computer(
    Motherboard,
    HasWifi,
    HasLTE,
    ReleaseDate,
    Price,
    VideoCard
) VALUES ('" + myComputer.Motherboard
    + "','" + myComputer.HasWifi
    + "','" + myComputer.HasLTE
    + "','" + myComputer.ReleaseDate?.ToString("yyyy-MM-dd")
    + "','" + myComputer.Price
    + "','" + myComputer.VideoCard
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
    Computer.VideoCard
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
    + "','" + singleComputer.VideoCard
    + "'");

}

var test = new FilerWriter();
test.WriteFile();
test.RunAutoMapper();
IEnumerable<Computer>? readComputers = test.ReadComputers();
if (readComputers != null)
{
    Console.WriteLine(readComputers);
    foreach (Computer computer in readComputers)
    { 
        string sql = @"INSERT INTO TutorialAppSchema.Computer (
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                + "','" + EscapeSingleQuote(computer?.VideoCard)
        + "')";
        Console.WriteLine(1 + sql);

        // dapper.ExecuteSql(sql);

        // entityFramwork.Add(computer);
    }
    // entityFramwork.SaveChanges(); 
}

string EscapeSingleQuote(string? input)
{
    if (input == null) return ""; 
    string output = input.Replace("'", "''");

    return output; 
}