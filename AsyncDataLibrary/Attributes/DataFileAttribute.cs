namespace AsyncDataLibrary.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DataFileAttribute : Attribute
{
    public DataFileAttribute(string fileName)
    {
        FileName = fileName;
    }

    public string FileName { get; }
}
