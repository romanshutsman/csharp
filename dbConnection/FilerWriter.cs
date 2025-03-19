using System.Text.Json;
using dbConnection.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class FilerWriter {

    public void WriteFile() {
        // File.WriteAllText("log.txt", "Hello");
        using StreamWriter openFile = new("log.txt", append: true);
        openFile.WriteLine("Hello;");

        openFile.Close();

        Console.WriteLine(File.ReadAllText("log.txt"));
    }

    public IEnumerable<Computer>? ReadComputers() {
            string computersJson = File.ReadAllText("Computers.json");

            Console.WriteLine(computersJson);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            // Console.WriteLine(computers);
            // if (computers != null) {
            //     foreach (Computer computer in computers) {
            //         // Console.WriteLine(computer.Motherboard);
            //     }
            // }
            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
            if (computersNewtonSoft != null) {
                foreach (Computer computer in computersNewtonSoft) {
                    Console.WriteLine(computer.Motherboard);
                }
            }

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);
            // File.WriteAllText("computersCopyNewtonsoft.json", computersCopyNewtonsoft);
            return computersSystem;
    }
}