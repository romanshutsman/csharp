// See https://aka.ms/new-console-template for more information

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=DotNetCourseDatabase1;TrustServerCertificate=true;Trusted_Connection=false;User id=sa;Password=SQLConnect1!;";

IDbConnection dBConnection = new SqlConnection(connectionString);

string sqlQuerySelectDate = "SELECT GETDATE()";
DateTime rightNow = dBConnection.QuerySingle<DateTime>(sqlQuerySelectDate);

Console.WriteLine(rightNow);
