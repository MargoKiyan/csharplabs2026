using System.Text.Encodings.Web;
using System.Text.Json;
using AsyncDataLibrary.Interfaces;

namespace AsyncDataLibrary.Infrastructure;

public class JsonDataSerializer : IDataSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public string Serialize<T>(List<T> items)
    {
        return JsonSerializer.Serialize(items, _options);
    }

    public List<T> Deserialize<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (JsonException)
        {
            return new List<T>();
        }
    }

    public Task<string> SerializeAsync<T>(List<T> items)
    {
        return Task.FromResult(Serialize(items));
    }

    public Task<List<T>> DeserializeAsync<T>(string json)
    {
        return Task.FromResult(Deserialize<T>(json));
    }
}
