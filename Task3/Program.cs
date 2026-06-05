using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string filePath = GetDataFilePath("author.json");

Author author = new()
{
    Name = "Леся Українка"
};

author.Books.Add(new Book { Title = "Лісова пісня", Author = author });
author.Books.Add(new Book { Title = "Contra spem spero!", Author = author });
author.Books.Add(new Book { Title = "Давня казка", Author = author });

Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
string json = JsonSerializer.Serialize(author, jsonOptions);
File.WriteAllText(filePath, json);

Console.WriteLine("Автор і книги серіалізовані у файл data/author.json.");
Console.WriteLine("Циклічне посилання усунуто за допомогою [JsonIgnore].");
Console.WriteLine();
Console.WriteLine("Серіалізований автор:");
Console.WriteLine($"Ім'я автора: {author.Name}");
foreach (Book book in author.Books)
{
    Console.WriteLine($"- {book.Title}");
}

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class Author
{
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = new();
}

class Book
{
    public string Title { get; set; } = string.Empty;

    [JsonIgnore]
    public Author Author { get; set; } = null!;
}
