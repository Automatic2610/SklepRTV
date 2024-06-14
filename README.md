SklepRTV
SklepRTV to aplikacja napisana w C# .NET Core, która umożliwia użytkownikom zarządzanie zakupami w sklepie elektronicznym. Aplikacja oferuje funkcje podobne do typowego sklepu RTV/AGD, takie jak zakup towarów i zarządzanie zamówieniami.

Spis treści: 
1. Kontrolery
2. Klasy modeli
3. Tworzenie bazy danych
4. Migracje
5. Plik połączenia z bazą danych

1. Kontrolery
Kontrolery obsługują żądania HTTP i odpowiedzi. Są punktami wejściowymi dla interakcji użytkownika z aplikacją.

1.1.ProductController
Zarządza listami produktów, szczegółami i funkcjami wyszukiwania.

1.2.OrderController
Obsługuje tworzenie zamówień, ich przeglądanie i zarządzanie.

1.3.CustomerController
Zarządza danymi klientów i ich profilami.

1.4.AdminController
Zarządza funkcjami administracyjnymi takimi jak zarządzanie użytkownikami, produktami, przeglądanie raportów i statystyk.

1.5.CartController
Obsługuje operacje związane z koszykiem zakupowym, w tym dodawanie i usuwanie produktów oraz wyświetlanie zawartości koszyka.

1.6.CategoryController
Zarządza kategoriami produktów, w tym dodawanie nowych kategorii, edytowanie i wyświetlanie listy kategorii.

1.7.HomeController
Obsługuje główne strony aplikacji, takie jak strona główna, kontaktowa i o nas.

1.8.ManagerController
Zarządza funkcjami menedżerskimi, takimi jak zarządzanie oddziałami sklepu, przeglądanie i analizowanie zamówień oraz zarządzanie pracownikami.

1.9.OrderController
Obsługuje operacje związane z zamówieniami, takie jak tworzenie nowych zamówień, wyświetlanie szczegółów zamówienia i zarządzanie zamówieniami.

1.10.ProductController
Zarządza operacjami związanymi z produktami, w tym pobieranie listy produktów, wyświetlanie szczegółów i wyszukiwanie produktów.

1.11.UserController
Zarządza danymi użytkowników, w tym rejestracja nowych użytkowników, edytowanie profili i wyświetlanie listy użytkowników.

2. Modele klas

2.1.AddressDetails
Reprezentuje szczegóły adresowe, takie jak ulica, miasto i inne informacje adresowe.

2.2.Branch
Reprezentuje oddział sklepu, w tym nazwę i lokalizację oddziału.

2.3.Cart
Reprezentuje koszyk zakupowy, zawierający identyfikator klienta oraz listę produktów w koszyku.

2.4.CartItem
Reprezentuje pozycję w koszyku, zawierającą identyfikator produktu i ilość.

2.5.Category
Reprezentuje kategorię produktu, zawierającą nazwę kategorii.

2.6.ContactDetails
Reprezentuje szczegóły kontaktowe, takie jak numer telefonu i adres e-mail.

2.7.Country
Reprezentuje kraj, zawierający nazwę kraju.

2.8.Customer
Reprezentuje klienta sklepu, zawierający identyfikator, imię, nazwisko i adres e-mail klienta.

2.9.JobPosition
Reprezentuje pozycję zawodową, zawierającą tytuł stanowiska.

2.10.Order
Reprezentuje zamówienie klienta, zawierające identyfikator zamówienia, identyfikator klienta, datę zamówienia oraz listę zamówionych produktów.

2.11.OrderItem
Reprezentuje pozycję w zamówieniu, zawierającą identyfikator produktu i ilość.

2.12.Product
Reprezentuje produkt dostępny w sklepie, zawierający identyfikator, nazwę, cenę i inne właściwości produktu.

2.13.User
Reprezentuje użytkownika aplikacji, zawierający identyfikator, nazwę użytkownika i hasło.

2.14.UserType
Reprezentuje typ użytkownika (np. admin, klient), zawierający nazwę typu użytkownika.

2.15.Warehouse
Reprezentuje magazyn, zawierający lokalizację magazynu.

2.16.Worker
Reprezentuje pracownika sklepu, zawierający identyfikator, imię, nazwisko i identyfikator stanowiska. 

3. Tworzenie bazy danych
Aby utworzyć bazę danych:

3.1. Zainstaluj niezbędne pakiety:
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

3.2. Skonfiguruj kontekst bazy danych:
public class SklepRTVContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_Connection_String_Here");
    }
}

3.3. Zaktualizuj plik Startup.cs, aby uwzględnić kontekst:
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<SklepRTVContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    services.AddControllersWithViews();
}

4. Migracje
Aby zarządzać migracjami:

4.1. Utwórz początkową migrację:
dotnet ef migrations add InitialCreate

4.2. Zastosuj migrację do bazy danych:
dotnet ef database update

5. Plik połączenia z bazą danych
Ciąg połączenia do bazy danych jest zazwyczaj przechowywany w pliku appsettings.json.

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "SklepRTV": "Server=(localdb)\\mssqllocaldb;Database=SklepRTV;Trusted_Connection=True;",
    "IdentityDbContextConnection": "Server=(localdb)\\mssqllocaldb;Database=SklepRTV.MVC;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
