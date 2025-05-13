
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WindowsFormsApp1
{
    public class DataManager
    {
        public List<Group> Groups { get; set; } = new List<Group>();

        private readonly string filePath = "data.json";

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                Groups = JsonSerializer.Deserialize<List<Group>>(json) ?? new List<Group>();
            }
        }

        public void SaveData()
        {
            var json = JsonSerializer.Serialize(Groups, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
