
using Bogus;
using GeneradorCompras.Models.Interface;
using GeneradorCompras.Models;

public class BookService : IBookService
{
    public List<Book> GetFakeBooks(int count)
    {
        var faker = new Faker<Book>()
            .RuleFor(b => b.BookId, f => f.IndexFaker + 1) // Asegúrate de que el nombre sea correcto
            .RuleFor(b => b.Title, f => f.Commerce.ProductName()) // Genera un nombre de producto como título
            .RuleFor(b => b.Author, f => f.Name.FullName())
            .RuleFor(b => b.Genre, f => f.Commerce.Department()) // Género aleatorio
            .RuleFor(b => b.Price, f => f.Random.Decimal(5, 100)); // Precio entre 5 y 100

        return faker.Generate(count);
    }
}