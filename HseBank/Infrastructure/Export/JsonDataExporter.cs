using System.Text.Json;

namespace HseBank.Infrastructure.Export;

public class JsonDataExporter
{
    public void ExportToJson(object data, string fileName)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
        Console.WriteLine($"JSON файл сохранен в {Path.GetFullPath(fileName)}");
    }
}