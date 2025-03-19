using System.Text.Json;
using AutoMapper;
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
            this.RunAutoMapper();
            this.PropertyMapper();
            return computersSystem;
    }

    public void RunAutoMapper() {
         string computersJson = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options => 
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.CPUCores, options => 
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasLTE, options => 
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.HasWifi, options => 
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.Motherboard, options => 
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.VideoCard, options => 
                        options.MapFrom(source => source.video_card))
                    .ForMember(destination => destination.ReleaseDate, options => 
                        options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, options => 
                        options.MapFrom(source => source.price));
            }));
            
            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if (computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                Console.WriteLine("Automapper Count: " +  computerResult.Count());
                // foreach (Computer computer in computerResult)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }
    }

    public void PropertyMapper() {
        string computersJson = File.ReadAllText("ComputersSnake.json");
        IEnumerable<Computer>? computersJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);
        if (computersJsonPropertyMapping != null)
        {
            Console.WriteLine("JSON Property Count: " + computersJsonPropertyMapping.Count());
            // foreach (Computer computer in computersJsonPropertyMapping)
            // {
            //     Console.WriteLine(computer.Motherboard);
            // }
        }
    }
}