using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodFinance.Services
{
    public static class LocalStorageService
    {
    static string dataFile = Path.Combine(FileSystem.AppDataDirectory, "data.json");
        static string settingsFile = Path.Combine(FileSystem.AppDataDirectory, "settings.json");

     public static async Task SaveDataAsync<T>(T data)
        {
            try
  {
       var json = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(dataFile, json);
            }
       catch (Exception ex)
        {
          Console.WriteLine($"Error saving data: {ex.Message}");
          }
      }

    public static async Task<T?> LoadDataAsync<T>()
        {
       try
     {
        if (!File.Exists(dataFile)) return default;
    var json = await File.ReadAllTextAsync(dataFile);
    return JsonSerializer.Deserialize<T>(json);
   }
         catch (Exception ex)
   {
         Console.WriteLine($"Error loading data: {ex.Message}");
          return default;
  }
        }

        public static async Task SaveSettingsAsync<T>(T settings)
        {
 try
            {
        var json = JsonSerializer.Serialize(settings);
    await File.WriteAllTextAsync(settingsFile, json);
          }
 catch (Exception ex)
      {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        public static async Task<T?> LoadSettingsAsync<T>() where T : new()
        {
      try
            {
    if (!File.Exists(settingsFile)) return new T();
    var json = await File.ReadAllTextAsync(settingsFile);
    return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
   catch (Exception ex)
        {
  Console.WriteLine($"Error loading settings: {ex.Message}");
       return new T();
            }
   }
    }
}
