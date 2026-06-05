using AsyncDataLibrary.Infrastructure;
using AsyncDataLibrary.Models;
using AsyncDataLibrary.Repositories;
using AsyncDataLibrary.Services;

JsonDataSerializer serializer = new();
FileStorageProvider storageProvider = new();

JsonRepository<User> userRepository = new(storageProvider, serializer);
JsonRepository<Book> bookRepository = new(storageProvider, serializer);
JsonRepository<Order> orderRepository = new(storageProvider, serializer);

UserService userService = new(userRepository);
BookService bookService = new(bookRepository);
OrderService orderService = new(orderRepository);

ClearDemoData(userService, bookService, orderService);

Console.WriteLine("Практична робота №5");
Console.WriteLine("AsyncDataLibrary Demo");
Console.WriteLine("Онлайн-бібліотека / книжковий сервіс");
Console.WriteLine();

User syncUser = new()
{
    FullName = "Киян Маргарита",
    Email = "m.kyian@example.com"
};

Book syncBook = new()
{
    Title = "Місто",
    Author = "Валер'ян Підмогильний",
    Year = 1928,
    IsAvailable = true
};

Book secondSyncBook = new()
{
    Title = "Тіні забутих предків",
    Author = "Михайло Коцюбинський",
    Year = 1911,
    IsAvailable = true
};

Console.WriteLine("Синхронні операції:");
userService.Add(syncUser);
bookService.Add(syncBook);
bookService.Add(secondSyncBook);

Order syncOrder = new()
{
    UserId = syncUser.Id,
    BookId = syncBook.Id
};

orderService.Add(syncOrder);

syncBook.IsAvailable = false;
bookService.Update(syncBook);

User? foundSyncUser = userService.GetById(syncUser.Id);
Console.WriteLine($"Користувача знайдено за Id: {foundSyncUser?.FullName}");
PrintUsers("Користувачі після синхронного додавання:", userService.GetAll());
PrintBooks("Книги після синхронного додавання та оновлення:", bookService.GetAll());
PrintOrders("Замовлення після синхронного додавання:", orderService.GetAll());

Console.WriteLine();
Console.WriteLine("Асинхронні операції:");

User asyncUser = new()
{
    FullName = "Олена Петренко",
    Email = "olena.petrenko@example.com"
};

Book asyncBook = new()
{
    Title = "Кайдашева сім'я",
    Author = "Іван Нечуй-Левицький",
    Year = 1879,
    IsAvailable = true
};

Book temporaryBook = new()
{
    Title = "Тимчасова демонстраційна книга",
    Author = "Demo",
    Year = 2026,
    IsAvailable = true
};

await userService.AddAsync(asyncUser);
await bookService.AddAsync(asyncBook);

Order asyncOrder = new()
{
    UserId = asyncUser.Id,
    BookId = asyncBook.Id
};

await orderService.AddAsync(asyncOrder);

asyncOrder.Status = "Completed";
await orderService.UpdateAsync(asyncOrder);

await bookService.AddAsync(temporaryBook);
await bookService.DeleteAsync(temporaryBook.Id);

User? foundAsyncUser = await userService.GetByIdAsync(asyncUser.Id);
Console.WriteLine($"Користувача знайдено асинхронно за Id: {foundAsyncUser?.FullName}");
Console.WriteLine("Тимчасову книгу додано та видалено через асинхронні методи.");

List<Book> availableBooks = await bookService.GetAvailableBooksAsync();
List<Order> asyncUserOrders = await orderService.GetOrdersByUserIdAsync(asyncUser.Id);

PrintUsers("Усі демонстраційні користувачі:", userService.GetAll());
PrintBooks("Доступні книги після асинхронних операцій:", availableBooks);
PrintOrders("Замовлення асинхронного користувача:", asyncUserOrders);

Console.WriteLine();
Console.WriteLine("Дані збережені через бібліотеку у папку data.");

void ClearDemoData(UserService users, BookService books, OrderService orders)
{
    foreach (Order order in orders.GetAll())
    {
        orders.Delete(order.Id);
    }

    foreach (User user in users.GetAll())
    {
        users.Delete(user.Id);
    }

    foreach (Book book in books.GetAll())
    {
        books.Delete(book.Id);
    }
}

void PrintUsers(string title, List<User> users)
{
    Console.WriteLine(title);
    foreach (User user in users)
    {
        Console.WriteLine($"- {user.FullName}, email: {user.Email}, дата реєстрації: {user.RegisteredAt:yyyy-MM-dd HH:mm}");
    }
}

void PrintBooks(string title, List<Book> books)
{
    Console.WriteLine(title);
    foreach (Book book in books)
    {
        string availability = book.IsAvailable ? "доступна" : "видана";
        Console.WriteLine($"- \"{book.Title}\", {book.Author}, {book.Year}, статус: {availability}");
    }
}

void PrintOrders(string title, List<Order> orders)
{
    Console.WriteLine(title);
    foreach (Order order in orders)
    {
        Console.WriteLine($"- OrderId: {order.Id}, \nUserId: {order.UserId}, \nBookId: {order.BookId}, \nдата: {order.OrderDate:yyyy-MM-dd HH:mm}, \nстатус: {order.Status}");
    }
}
