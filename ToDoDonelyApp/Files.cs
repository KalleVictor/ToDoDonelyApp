using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoDonelyApp
{
    internal class Files
    {
        //Saving the tasklist
        public static void SaveTasks(List<Project> tasklist, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(tasklist, options);
            File.WriteAllText(filePath, jsonString);
        }
        //Loading the tasklist
        public static List<Project> LoadTasks(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Project>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Project>>(jsonString) ?? new List<Project>();
        }
    }
}
