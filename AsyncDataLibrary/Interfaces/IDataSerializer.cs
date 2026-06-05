namespace AsyncDataLibrary.Interfaces;

public interface IDataSerializer
{
    string Serialize<T>(List<T> items);
    List<T> Deserialize<T>(string json);

    Task<string> SerializeAsync<T>(List<T> items);
    Task<List<T>> DeserializeAsync<T>(string json);
}
