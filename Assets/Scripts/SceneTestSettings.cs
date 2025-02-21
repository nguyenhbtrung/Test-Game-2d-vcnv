using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public static class SceneTestSettings
{
    private static readonly string filePath = Path.Combine(Application.dataPath, "Editor/Data/SceneTestSettings.json");

    public static int SceneIndex
    {
        get
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<SceneSettings>(json);
                return settings.SceneIndex;
            }
            return 0; // Giá trị mặc định nếu không tìm thấy tệp
        }
        set
        {
            var settings = new SceneSettings { SceneIndex = value };
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

public class SceneSettings
{
    public int SceneIndex { get; set; }
}
