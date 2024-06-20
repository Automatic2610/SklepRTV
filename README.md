**SklepRTV**

**SklepRTV** to zaawansowana aplikacja napisana w C# .NET Core, która
umożliwia zarządzanie zakupami w sklepie elektronicznym. Aplikacja
obejmuje szeroki zakres funkcji typowych dla sklepów RTV/AGD, takich jak
zakup towarów, zarządzanie zamówieniami, oraz administracja sklepem.

**Spis** **treści**

> 1\. Konfiguracja Aplikacji SklepRTV 2. Kontrolery
>
> 3\. Modele klas 4. Migracje
>
> 5\. Widoki w aplikacji SklepRTV 
6\. ErrorViewModel
>
> 7\. Tworzenie i zarządzanie bazą danych
>
> **1.** **Konfiguracja** **Aplikacji** **SklepRTV**

Aplikacja SklepRTV jest uruchamiana i konfigurowana w pliku Program.cs.
Poniżej znajduje się opis poszczególnych etapów konfiguracji aplikacji.

Na początku konfigurowane są usługi aplikacji, które są dodawane do
kontenera Dependency Injection (DI). Obejmuje to usługi MVC, Entity
Framework Core do obsługi bazy danych, tożsamość ASP.NET Core do
zarządzania użytkownikami oraz sesję do przechowywania danych
użytkowników.

Następnie tworzona jest instancja aplikacji. Aplikacja jest
konfigurowana do obsługi różnych aspektów, takich jak przekierowania
HTTPS, obsługa plików statycznych, routing żądań HTTP oraz obsługa
błędów. W tej części definiowane są również trasy dla kontrolerów.

Aby zarządzać dostępem do różnych części aplikacji, tworzone są role
użytkowników, takie jak "Admin", "Manager" i "Member". Podczas
uruchamiania aplikacji tworzony jest również domyślny użytkownik
administracyjny, który ma pełne uprawnienia do zarządzania aplikacją.

Plik Program.cspełni kluczową rolę w konfiguracji i uruchomieniu
aplikacji SklepRTV. Zawiera wszystkie niezbędne kroki do skonfigurowania
usług, tras oraz ról i użytkowników, co zapewnia prawidłowe działanie
aplikacji.

> **2.** **Kontrolery**

Kontrolery w aplikacji pełnią rolę punktów wejściowych dla interakcji
użytkownika z systemem. Odpowiadają za obsługę żądań HTTP, komunikację z
modelem oraz zwracanie odpowiednich widoków.

**1.1.** **AdminController**

AdminController to kontroler w aplikacji **SklepRTV** przeznaczony do
zarządzania funkcjami administracyjnymi. Kontroler ten jest dostępny
tylko dla użytkowników z rolą "Admin", co zapewnia odpowiedni poziom
zabezpieczeń i kontroli dostępu.

**Atrybuty:**

> • \[Authorize(Roles = "Admin")\]: Ten atrybut oznacza, że wszystkie
> akcje w tym kontrolerze są dostępne tylko dla użytkowników, którzy
> mają przypisaną rolę "Admin".

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie głównej strony
> administracyjnej. Metoda ta zwraca widok, który może zawierać różne
> opcje administracyjne, takie jak zarządzanie użytkownikami,
> produktami, zamówieniami, itp.

using Microsoft.AspNetCore.Authorization; using
Microsoft.AspNetCore.Mvc;

namespace SklepRTV.MVC.Controllers {

> \[Authorize(Roles = "Admin")\]
>
> public class AdminController : Controller {
>
> public IActionResult Index() {
>
> return View(); }

} }

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml może zawierać różne opcje administracyjne, które są
dostępne tylko dla administratorów. Oto przykładowy kod widoku:

@{

ViewData\["Title"\] = "Panel Administracyjny"; }

\<h1\>Panel Administracyjny\</h1\>

\<p\>Witaj w panelu administracyjnym. Wybierz jedną z poniższych opcji,
aby zarządzać sklepem:\</p\>

\<ul\>

> \<li\>\<a href="/Admin/ManageUsers"\>Zarządzaj
> użytkownikami\</a\>\</li\> \<li\>\<a
> href="/Admin/ManageProducts"\>Zarządzaj produktami\</a\>\</li\>
> \<li\>\<a href="/Admin/ManageOrders"\>Zarządzaj
> zamówieniami\</a\>\</li\> \<li\>\<a href="/Admin/Reports"\>Raporty i
> statystyki\</a\>\</li\>

\</ul\>

AdminController zapewnia odpowiedni poziom zabezpieczeń dla funkcji
administracyjnych, umożliwiając administratorom zarządzanie różnymi
aspektami sklepu, podczas gdy dostęp do tych funkcji jest ograniczony
dla zwykłych użytkowników. Dzięki temu system pozostaje bezpieczny i
kontrolowany.

**1.2.** **CartController**

CartController to kontroler w aplikacji **SklepRTV** odpowiedzialny za
zarządzanie koszykiem zakupowym użytkownika. Kontroler ten pozwala
użytkownikom na dodawanie produktów do koszyka, usuwanie produktów oraz
przeglądanie zawartości koszyka. Dane koszyka są przechowywane w sesji
użytkownika, co umożliwia tymczasowe przechowywanie informacji o
zakupach podczas przeglądania sklepu.

**Konstruktor:**

> • CartController(ApplicationDbContext db): Inicjalizuje instancję
> kontrolera z kontekstem bazy danych ApplicationDbContext, umożliwiając
> dostęp do bazy danych.

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie zawartości koszyka.
> Pobiera dane koszyka z sesji, deserializuje je, a następnie przekazuje
> do widoku.

public IActionResult Index() {

> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if(cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();

return View(cart); }

> • AddToCart(Guid id, int quantity): Akcja dodająca produkt do koszyka.
> Pobiera produkt z bazy danych na podstawie identyfikatora, a następnie
> dodaje go do koszyka przechowywanego w sesji.

public IActionResult AddToCart(Guid id, int quantity) {

> var product = \_db.Products.FirstOrDefault(p =\> p.Id == id);
>
> if (product != null) {
>
> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.AddItem(product, quantity);
>
> cartJson = JsonSerializer.Serialize(cart);
> HttpContext.Session.SetString("Cart", cartJson);
>
> }

return RedirectToAction("Index"); }

> • RemoveFromCart(Guid id): Akcja usuwająca produkt z koszyka na
> podstawie identyfikatora produktu. Aktualizuje dane koszyka w sesji po
> usunięciu produktu.

public IActionResult RemoveFromCart(Guid id) {

> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.RemoveItem(id);
>
> cartJson = JsonSerializer.Serialize(cart);
> HttpContext.Session.SetString("Cart", cartJson);
>
> Debug.WriteLine(\$"Usunięto produkt z koszyka: {id}");

return RedirectToAction("Index"); }

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml wyświetla zawartość koszyka zakupowego, umożliwiając
użytkownikowi przeglądanie produktów dodanych do koszyka oraz
zarządzanie nimi.

@model SklepRTV.Model.Cart

\<h1\>Twój koszyk\</h1\>

@if (Model.Items.Any()) {

> \<table class="table"\> \<thead\>
>
> \<tr\> \<th\>Produkt\</th\> \<th\>Ilość\</th\> \<th\>Cena\</th\>
> \<th\>Łącznie\</th\> \<th\>Akcje\</th\>
>
> \</tr\>
>
> \</thead\> \<tbody\>
>
> @foreach (var item in Model.Items) {
>
> \<tr\> \<td\>@item.Product.name\</td\> \<td\>@item.Quantity\</td\>
>
> \<td\>@item.Product.price.ToString("c")\</td\>
> \<td\>@(item.Product.price \* item.Quantity).ToString("c")\</td\>
> \<td\>
>
> \<form asp-action="RemoveFromCart" method="post"\>
>
> \<input type="hidden" name="id" value="@item.Product.Id" /\> \<button
> type="submit" class="btn btn-danger"\>Usuń\</button\>
>
> \</form\> \</td\>
>
> \</tr\> }
>
> \</tbody\> \</table\> \<div\>
>
> \<h3\>Łączna wartość: @Model.CalculateTotal().ToString("c")\</h3\>
> \</div\>

} else {

\<p\>Twój koszyk jest pusty.\</p\> }

CartController zapewnia użytkownikom możliwość zarządzania swoimi
zakupami, umożliwiając dodawanie, usuwanie produktów oraz przeglądanie
zawartości koszyka. Przechowywanie danych koszyka w sesji pozwala na
tymczasowe przechowywanie informacji o zakupach podczas przeglądania
sklepu, co jest kluczowe dla wygody użytkowników i efektywności procesu
zakupowego.

**1.3.** **CatedoryController**

CategoryController to kontroler w aplikacji **SklepRTV** odpowiedzialny
za zarządzanie kategoriami produktów. Kontroler ten jest dostępny tylko
dla użytkowników z rolą "Admin", co zapewnia odpowiedni poziom
zabezpieczeń i kontroli dostępu.

**Konstruktor:**

> • CategoryController(ApplicationDbContext db): Inicjalizuje instancję
> kontrolera z kontekstem bazy danych ApplicationDbContext, umożliwiając
> dostęp do bazy danych.

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie listy kategorii.
> Pobiera wszystkie kategorie z bazy danych i przekazuje je do widoku.

public IActionResult Index() {

> var categories = \_db.Categories.ToList(); return View(categories);

}

> • Details(int id): Akcja wyświetlająca szczegóły wybranej kategorii na
> podstawie jej identyfikatora. Jeśli kategoria nie zostanie znaleziona,
> zwraca błąd 404.

public IActionResult Details(int id) {

> var category = \_db.Categories.FirstOrDefault(p =\> p.Id == id); if
> (category == null) return NotFound();

return View(category); }

> • Create(): Akcja wyświetlająca formularz do tworzenia nowej
> kategorii. Jest dostępna tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

> • Create(Category category): Akcja przetwarzająca dane z formularza
> tworzenia nowej kategorii. Dodaje nową kategorię do bazy danych, jeśli
> dane są prawidłowe, a następnie przekierowuje do listy kategorii.

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(Category category) {

> if (ModelState.IsValid) {
>
> \_db.Categories.Add(category); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(category); }

> • Edit(int id): Akcja wyświetlająca formularz do edytowania
> istniejącej kategorii na podstawie jej identyfikatora. Jest dostępna
> tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Edit(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();

return View(category); }

> • Edit(Category category): Akcja przetwarzająca dane z formularza
> edycji kategorii. Aktualizuje istniejącą kategorię w bazie danych,
> jeśli dane są prawidłowe, a następnie przekierowuje do listy
> kategorii.

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Category category) {

> if (ModelState.IsValid) {
>
> \_db.Categories.Update(category); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(category); }

> • Delete(int id): Akcja wyświetlająca formularz do usuwania
> istniejącej kategorii na podstawie jej identyfikatora. Jest dostępna
> tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Delete(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();

return View(category); }

> • DeletePOST(int id): Akcja przetwarzająca dane z formularza usuwania
> kategorii. Usuwa istniejącą kategorię z bazy danych, jeśli jest
> znaleziona, a następnie przekierowuje do listy kategorii.

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\] public
IActionResult DeletePOST(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();
> \_db.Categories.Remove(category);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml wyświetla listę kategorii, umożliwiając
administratorowi przeglądanie, edytowanie oraz usuwanie kategorii.

@model IEnumerable\<SklepRTV.Model.Category\>

\<h1\>Kategorie\</h1\>

\<p\>

\<a asp-action="Create" class="btn btn-primary"\>Dodaj nową
kategorię\</a\> \</p\>

\<table class="table"\> \<thead\>

> \<tr\> \<th\>Nazwa\</th\> \<th\>Akcje\</th\>
>
> \</tr\> \</thead\> \<tbody\>
>
> @foreach (var category in Model) {
>
> \<tr\> \<td\>@category.Name\</td\> \<td\>

\<a asp-action="Edit" asp-route-id="@category.Id" class="btn
btn-warning"\>Edytuj\</a\>

\<a asp-action="Details" asp-route-id="@category.Id" class="btn
btn-info"\>Szczegóły\</a\>

\<a asp-action="Delete" asp-route-id="@category.Id" class="btn
btn-danger"\>Usuń\</a\>

> \</td\> \</tr\>
>
> } \</tbody\>

\</table\>

CategoryController zapewnia administratorom pełną kontrolę nad
kategoriami produktów, umożliwiając tworzenie, edytowanie, przeglądanie
oraz usuwanie kategorii. Dzięki temu można łatwo zarządzać organizacją
produktów w sklepie, co poprawia doświadczenie użytkowników podczas
przeglądania oferty sklepu.

**1.4.** **HomeController**

HomeController to kontroler w aplikacji **SklepRTV** odpowiedzialny za
obsługę podstawowych stron aplikacji, takich jak strona główna, strona
prywatności oraz strona błędów. Kontroler ten jest publicznie dostępny i
nie wymaga autoryzacji użytkownika.

**Konstruktor:**

> • HomeController(ILogger\<HomeController\> logger): Inicjalizuje
> instancję kontrolera z loggerem, umożliwiając rejestrowanie zdarzeń i
> błędów.

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie strony głównej. Metoda
> ta zwraca widok Index.

public IActionResult Index() {

return View(); }

> • Privacy(): Akcja odpowiedzialna za wyświetlanie strony prywatności.
> Metoda ta zwraca widok Privacy.

public IActionResult Privacy() {

return View(); }

> • Error(): Akcja odpowiedzialna za wyświetlanie strony błędów. Używa
> modelu ErrorViewModel do przekazania informacji o błędzie do widoku.

\[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,
NoStore = true)\] public IActionResult Error()

{

return View(new ErrorViewModel { RequestId = Activity.Current?.Id ??
HttpContext.TraceIdentifier });

}

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml wyświetla zawartość strony głównej.

@{

ViewData\["Title"\] = "Home Page"; }

\<div class="text-center"\>

> \<h1 class="display-4"\>Welcome\</h1\>

\<p\>Welcome to SklepRTV - Your one-stop shop for electronics and home
appliances.\</p\> \</div\>

**Przykład** **widoku** **Privacy.cshtml:**

Widok Privacy.cshtml wyświetla informacje o polityce prywatności.

@{

ViewData\["Title"\] = "Privacy Policy"; }

\<h1\>Privacy Policy\</h1\>

\<p\>Your privacy is important to us. This page describes how we handle
your personal data.\</p\>

**Przykład** **widoku** **Error.cshtml:**

Widok Error.cshtml wyświetla informacje o błędach, które wystąpiły
podczas działania aplikacji.

@model SklepRTV.MVC.Models.ErrorViewModel

@{

ViewData\["Title"\] = "Error"; }

\<h1 class="text-danger"\>Error.\</h1\>

\<h2 class="text-danger"\>An error occurred while processing your
request.\</h2\>

@if (Model?.RequestId != null) {

> \<p\>
>
> \<strong\>Request ID:\</strong\> \<code\>@Model.RequestId\</code\>
> \</p\>

}

\<p\>

\<a asp-area="" asp-controller="Home" asp-action="Index"\>Back to
Home\</a\> \</p\>

HomeController zapewnia podstawowe funkcje nawigacyjne w aplikacji,
umożliwiając użytkownikom dostęp do strony głównej, informacji o
polityce prywatności oraz strony błędów. Dzięki temu użytkownicy mają
łatwy dostęp do kluczowych informacji oraz mogą być odpowiednio
poinformowani w przypadku wystąpienia błędów.

**1.5.** **OrderController**

OrderController to kontroler w aplikacji **SklepRTV** odpowiedzialny za
zarządzanie procesem składania zamówień. Kontroler ten umożliwia
użytkownikom przeglądanie koszyka, składanie zamówień oraz wyświetlanie
potwierdzenia zamówienia.

**Konstruktor:**

> • OrderController(ApplicationDbContext db): Inicjalizuje instancję
> kontrolera z kontekstem bazy danych ApplicationDbContext, umożliwiając
> dostęp do bazy danych.

**Akcje:**

> • Checkout(): Akcja odpowiedzialna za wyświetlanie strony finalizacji
> zakupu. Pobiera koszyk z sesji i przekazuje go do widoku. Jeśli koszyk
> jest pusty, przekierowuje do akcji PlaceOrder.

public ActionResult Checkout() {

> var cartJson = HttpContext.Session.GetString("Cart");
>
> Cart cart = cartJson != null ?
> JsonSerializer.Deserialize\<Cart\>(cartJson) : new Cart();
>
> if (!cart.Items.Any()) {
>
> return RedirectToAction("PlaceOrder"); }

return View(cart); }

> • PlaceOrder(string customerName, string customerEmail, AddressDetails
> customerAddress): Akcja odpowiedzialna za składanie zamówienia.
> Pobiera koszyk z sesji, tworzy nowe zamówienie oraz dodaje je do bazy
> danych. Następnie usuwa koszyk z sesji i przekierowuje do akcji
> OrderConfirmation.

\[HttpPost\]

public ActionResult PlaceOrder(string customerName, string
customerEmail, AddressDetails customerAddress)

{

> var cartJson = HttpContext.Session.GetString("Cart");
>
> Cart cart = cartJson != null ?
> JsonSerializer.Deserialize\<Cart\>(cartJson) : new Cart();
>
> if (cart.Items.Any()) {
>
> var order = new Order {
>
> Id = Guid.NewGuid(), OrderDate = DateTime.Now,
>
> CustomerName = customerName, CustomerEmail = customerEmail,
> CustomerAddress = customerAddress, TotalAmount = cart.CalculateTotal()
>
> };
>
> foreach (var item in cart.Items) {
>
> order.OrderItems.Add(new OrderItem {
>
> Id = Guid.NewGuid(), OrderId = order.Id, ProductId = item.Product.Id,
>
> ProductName = item.Product.name, Quantity = item.Quantity,
>
> Price = item.Product.price });
>
> }
>
> \_db.Orders.Add(order); \_db.SaveChanges();
>
> HttpContext.Session.Remove("Cart");
>
> return RedirectToAction("OrderConfirmation", new { orderId = order.Id
> }); }

return RedirectToAction("Index", "Cart"); }

> • OrderConfirmation(Guid orderId): Akcja odpowiedzialna za
> wyświetlanie potwierdzenia zamówienia. Pobiera zamówienie na podstawie
> jego identyfikatora i przekazuje je do widoku. Jeśli zamówienie nie
> zostanie znalezione, przekierowuje do akcji OrderSubmitingEnd.

public IActionResult OrderConfirmation(Guid orderId) {

> var order = \_db.Orders.FirstOrDefault(o =\> o.Id == orderId);
>
> if (order == null) {
>
> return RedirectToAction("OrderSubmitingEnd"); }

return View(order); }

> • OrderSubmitingEnd(): Akcja odpowiedzialna za wyświetlanie strony
> końcowej po złożeniu zamówienia. Może zawierać informacje
> potwierdzające zakończenie procesu zamówienia.

public IActionResult OrderSubmitingEnd() {

return View(); }

**Przykład** **widoku** **Checkout.cshtml:**

Widok Checkout.cshtml wyświetla zawartość koszyka oraz formularz do
wprowadzenia danych klienta.

@model SklepRTV.Model.Cart

\<h1\>Finalizacja Zakupu\</h1\>

\<form asp-action="PlaceOrder" method="post"\> \<div
class="form-group"\>

> \<label for="customerName"\>Imię i nazwisko\</label\>

\<input type="text" class="form-control" id="customerName"
name="customerName" required /\>

> \</div\>
>
> \<div class="form-group"\>
>
> \<label for="customerEmail"\>Email\</label\>

\<input type="email" class="form-control" id="customerEmail"
name="customerEmail" required /\>

> \</div\>
>
> \<div class="form-group"\>
>
> \<label for="customerAddress"\>Adres\</label\>

\<textarea class="form-control" id="customerAddress"
name="customerAddress" required\>\</textarea\>

> \</div\>
>
> \<h3\>Twoje produkty:\</h3\> \<table class="table"\>
>
> \<thead\> \<tr\>
>
> \<th\>Produkt\</th\> \<th\>Ilość\</th\> \<th\>Cena\</th\>
> \<th\>Łącznie\</th\>
>
> \</tr\> \</thead\> \<tbody\>
>
> @foreach (var item in Model.Items) {
>
> \<tr\> \<td\>@item.Product.name\</td\> \<td\>@item.Quantity\</td\>
>
> \<td\>@item.Product.price.ToString("c")\</td\>
> \<td\>@(item.Product.price \* item.Quantity).ToString("c")\</td\>
>
> \</tr\> }
>
> \</tbody\> \</table\> \<div\>
>
> \<h3\>Łączna wartość: @Model.CalculateTotal().ToString("c")\</h3\>
> \</div\>

\<button type="submit" class="btn btn-primary"\>Złóż
zamówienie\</button\> \</form\>

**Przykład** **widoku** **OrderConfirmation.cshtml:**

Widok OrderConfirmation.cshtml wyświetla szczegóły zamówienia oraz
potwierdzenie jego złożenia.

@model SklepRTV.Model.Order

\<h1\>Potwierdzenie Zamówienia\</h1\>

\<p\>Dziękujemy za złożenie zamówienia, @Model.CustomerName!\</p\>

\<p\>Twoje zamówienie zostało przyjęte. Poniżej znajdują się szczegóły
zamówienia:\</p\>

\<h3\>Szczegóły zamówienia\</h3\> \<ul\>

> \<li\>Numer zamówienia: @Model.Id\</li\> \<li\>Data zamówienia:
> @Model.OrderDate\</li\> \<li\>Email: @Model.CustomerEmail\</li\>

\<li\>Adres: @Model.CustomerAddress.city, @Model.CustomerAddress.street
@Model.CustomerAddress.houseNo/@Model.CustomerAddress.flatNo,
@Model.CustomerAddress.province\</li\>

\</ul\>

\<h3\>Twoje produkty:\</h3\> \<table class="table"\>

> \<thead\> \<tr\>
>
> \<th\>Produkt\</th\> \<th\>Ilość\</th\> \<th\>Cena\</th\>
> \<th\>Łącznie\</th\>
>
> \</tr\> \</thead\> \<tbody\>
>
> @foreach (var item in Model.OrderItems) {
>
> \<tr\> \<td\>@item.ProductName\</td\> \<td\>@item.Quantity\</td\>
>
> \<td\>@item.Price.ToString("c")\</td\> \<td\>@(item.Price \*
> item.Quantity).ToString("c")\</td\>
>
> \</tr\> }

\</tbody\> \</table\> \<div\>

\<h3\>Łączna wartość zamówienia: @Model.TotalAmount.ToString("c")\</h3\>
\</div\>

OrderController zapewnia użytkownikom możliwość składania zamówień oraz
przeglądania szczegółów złożonych zamówień. Proces finalizacji zakupu
obejmuje przeglądanie koszyka, podanie danych klienta, przetwarzanie
zamówienia oraz wyświetlanie potwierdzenia zamówienia. Dzięki temu
użytkownicy mają pełną kontrolę nad swoimi zakupami, a cały proces jest
prosty i intuicyjny.

**1.6.** **ProductController**

ProductController to kontroler w aplikacji **SklepRTV** odpowiedzialny
za zarządzanie produktami. Kontroler ten umożliwia przeglądanie
produktów, wyświetlanie szczegółów

produktu oraz funkcje administracyjne takie jak dodawanie, edytowanie i
usuwanie produktów.

**Konstruktor:**

> • ProductController(ApplicationDbContext db, IWebHostEnvironment
> environment): Inicjalizuje instancję kontrolera z kontekstem bazy
> danych ApplicationDbContext oraz środowiskiem hosta internetowego
> IWebHostEnvironment, umożliwiając dostęp do bazy danych oraz systemu
> plików.

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie listy produktów.
> Pobiera wszystkie produkty z bazy danych i przekazuje je do widoku.

public IActionResult Index() {

> var products = \_db.Products.ToList(); return View(products);

}

> • AdminIndex(): Akcja odpowiedzialna za wyświetlanie listy produktów w
> widoku administracyjnym. Pobiera wszystkie produkty z bazy danych i
> przekazuje je do widoku. Dostępna tylko dla użytkowników z rolą
> "Admin".

\[Authorize(Roles = "Admin")\] \[HttpGet\]

public IActionResult AdminIndex() {

> var products = \_db.Products.ToList(); return View(products);

}

> • Details(Guid id): Akcja wyświetlająca szczegóły wybranego produktu
> na podstawie jego identyfikatora. Jeśli produkt nie zostanie
> znaleziony, zwraca błąd 404.

public IActionResult Details(Guid id) {

> var product = \_db.Products.FirstOrDefault(p =\> p.Id == id); if
> (product == null) return NotFound();

return View(product); }

> • Create(): Akcja wyświetlająca formularz do tworzenia nowego
> produktu. Jest dostępna tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

> return View();

}

> • Create(Product product, IFormFile image): Akcja przetwarzająca dane
> z formularza tworzenia nowego produktu. Dodaje nowy produkt do bazy
> danych oraz zapisuje przesłany plik obrazu na serwerze. Dostępna tylko
> dla administratorów.

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public async Task\<IActionResult\> Create(Product product, IFormFile
image) {

> if (image == null \|\| image.Length == 0) {
>
> Debug.WriteLine("Błędny plik"); return Content("Błędny plik");
>
> }
>
> var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
> image.FileName);
>
> using (var stream = new FileStream(path, FileMode.Create)) {
>
> await image.CopyToAsync(stream); }
>
> product.image = \$"/{image.FileName}";
>
> if (ModelState.IsValid) {
>
> \_db.Products.Add(product); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }
>
> else {
>
> var errors = ModelState.Values.SelectMany(x =\> x.Errors); foreach
> (var error in errors)
>
> {
>
> Debug.WriteLine(\$"Błąd modelu: {error.ErrorMessage}"); }
>
> ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku"); }

return View(product); }

> • Edit(Guid id): Akcja wyświetlająca formularz do edytowania
> istniejącego produktu na podstawie jego identyfikatora. Jest dostępna
> tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Edit(Guid id) {

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound();

return View(product); }

> • Edit(Product product, IFormFile image): Akcja przetwarzająca dane z
> formularza edycji produktu. Aktualizuje istniejący produkt w bazie
> danych oraz zapisuje nowy plik obrazu, jeśli został przesłany.
> Dostępna tylko dla administratorów.

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Product product, IFormFile image) {

> if (ModelState.IsValid) {
>
> try {
>
> if (image != null) {
>
> var uploads = Path.Combine(\_environment.WebRootPath, "uploads"); if
> (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
>
> var filePath = Path.Combine(uploads, image.FileName);
>
> using (var fileStream = new FileStream(filePath, FileMode.Create)) {
>
> image.CopyToAsync(fileStream); }
>
> product.image = \$"/uploads/{image.FileName}"; }
>
> \_db.Products.Update(product); \_db.SaveChanges();
>
> }
>
> catch (Exception ex) {
>
> ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku"); return
> View(product);
>
> }
>
> return RedirectToAction("AdminIndex"); }

return View(product); }

> • Delete(Guid id): Akcja wyświetlająca formularz do usuwania
> istniejącego produktu na podstawie jego identyfikatora. Jest dostępna
> tylko dla administratorów.

\[Authorize(Roles = "Admin")\] public IActionResult Delete(Guid id)

{

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound();

return View(product); }

> • DeletePOST(Guid id): Akcja przetwarzająca dane z formularza usuwania
> produktu. Usuwa istniejący produkt z bazy danych, jeśli jest
> znaleziony, a następnie przekierowuje do widoku administracyjnego.
> Dostępna tylko dla administratorów.

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeletePOST(Guid id) {

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound(); \_db.Products.Remove(product);
>
> \_db.SaveChanges();

return RedirectToAction("AdminIndex"); }

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml wyświetla listę produktów dostępnych w sklepie.

@model IEnumerable\<SklepRTV.Model.Product\>

\<h1\>Produkty\</h1\>

\<table class="table"\> \<thead\>

> \<tr\> \<th\>Nazwa\</th\> \<th\>Cena\</th\> \<th\>Opis\</th\>
> \<th\>Akcje\</th\>
>
> \</tr\> \</thead\> \<tbody\>
>
> @foreach (var product in Model) {
>
> \<tr\> \<td\>@product.name\</td\>
>
> \<td\>@product.price.ToString("c")\</td\>
> \<td\>@product.description\</td\>
>
> \<td\>

\<a asp-action="Details" asp-route-id="@product.Id" class="btn
btn-info"\>Szczegóły\</a\>

> \</td\> \</tr\>
>
> } \</tbody\>

\</table\>

ProductController zapewnia użytkownikom oraz administratorom pełną
kontrolę nad zarządzaniem produktami w sklepie, umożliwiając
przeglądanie, dodawanie, edytowanie oraz usuwanie produktów. Dzięki temu
system zarządzania produktami jest intuicyjny i łatwy w obsłudze zarówno
dla administratorów, jak i użytkowników.

**1.7.** **UserController**

UserController to kontroler w aplikacji **SklepRTV** odpowiedzialny za
zarządzanie użytkownikami. Kontroler ten jest dostępny tylko dla
użytkowników z rolą "Admin", co zapewnia odpowiedni poziom zabezpieczeń
i kontroli dostępu. Kontroler umożliwia przeglądanie listy użytkowników,
zarządzanie rolami użytkowników oraz aktywacją/dezaktywacją kont
użytkowników.

**Konstruktor:**

> • UserController(UserManager\<IdentityUser\> userManager):
> Inicjalizuje instancję kontrolera z menedżerem użytkowników
> UserManager\<IdentityUser\>, umożliwiając dostęp do funkcji
> zarządzania użytkownikami.

**Akcje:**

> • Index(): Akcja odpowiedzialna za wyświetlanie listy użytkowników.
> Pobiera wszystkich użytkowników z bazy danych i przekazuje ich do
> widoku.

public IActionResult Index() {

> var users = \_userManager.Users.ToList(); return View(users);

}

> • Details(Guid id): Akcja wyświetlająca szczegóły wybranego
> użytkownika na podstawie jego identyfikatora. Jeśli użytkownik nie
> zostanie znaleziony, zwraca błąd 404.

\[HttpGet\]

public async Task\<IActionResult\> Details(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();

return View(user); }

> • AddMemberRole(Guid id): Akcja dodająca rolę "Member" do użytkownika.
> Jeśli użytkownik nie zostanie znaleziony, zwraca błąd 404.

public async Task\<IActionResult\> AddMemberRole(Guid id)

{

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> await \_userManager.AddToRoleAsync(user, "Member"); await
> \_userManager.UpdateAsync(user);

return RedirectToAction("Index"); }

> • AddManagerRole(Guid id): Akcja dodająca rolę "Manager" do
> użytkownika. Jeśli użytkownik nie zostanie znaleziony, zwraca błąd
> 404.

public async Task\<IActionResult\> AddManagerRole(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> await \_userManager.AddToRoleAsync(user, "Manager"); return
> RedirectToAction("Index");

}

> • AddAdminRole(Guid id): Akcja dodająca rolę "Admin" do użytkownika.
> Jeśli użytkownik nie zostanie znaleziony, zwraca błąd 404.

public async Task\<IActionResult\> AddAdminRole(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> await \_userManager.AddToRoleAsync(user, "Admin"); return
> RedirectToAction("Index");

}

> • DeactivateUser(Guid id): Akcja dezaktywująca konto użytkownika.
> Ustawia pole EmailConfirmed na false. Jeśli użytkownik nie zostanie
> znaleziony, zwraca błąd 404.

public async Task\<IActionResult\> DeactivateUser(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> user.EmailConfirmed = false;
>
> await \_userManager.UpdateAsync(user); return
> RedirectToAction("Index");

}

> • ActivateUser(Guid id): Akcja aktywująca konto użytkownika. Ustawia
> pole EmailConfirmed na true. Jeśli użytkownik nie zostanie znaleziony,
> zwraca błąd 404.

public async Task\<IActionResult\> ActivateUser(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> user.EmailConfirmed = true;
>
> await \_userManager.UpdateAsync(user); return
> RedirectToAction("Index");

}

**Przykład** **widoku** **Index.cshtml:**

Widok Index.cshtml wyświetla listę użytkowników z opcjami zarządzania
rolami oraz aktywacją/dezaktywacją kont użytkowników.

@model IEnumerable\<Microsoft.AspNetCore.Identity.IdentityUser\>

\<h1\>Lista Użytkowników\</h1\>

\<table class="table"\> \<thead\>

> \<tr\>
>
> \<th\>Nazwa Użytkownika\</th\> \<th\>Email\</th\> \<th\>Akcje\</th\>
>
> \</tr\> \</thead\> \<tbody\>
>
> @foreach (var user in Model) {
>
> \<tr\> \<td\>@user.UserName\</td\> \<td\>@user.Email\</td\> \<td\>

\<a asp-action="Details" asp-route-id="@user.Id" class="btn
btn-info"\>Szczegóły\</a\>

\<a asp-action="AddMemberRole" asp-route-id="@user.Id" class="btn
btn-primary"\>Dodaj rolę Member\</a\>

\<a asp-action="AddManagerRole" asp-route-id="@user.Id" class="btn
btn-primary"\>Dodaj rolę Manager\</a\>

\<a asp-action="AddAdminRole" asp-route-id="@user.Id" class="btn
btn-primary"\>Dodaj rolę Admin\</a\>

> @if (user.EmailConfirmed) {

\<a asp-action="DeactivateUser" asp-route-id="@user.Id" class="btn
btn-warning"\>Dezaktywuj\</a\>

> } else {

\<a asp-action="ActivateUser" asp-route-id="@user.Id" class="btn
btn-success"\>Aktywuj\</a\>

> } \</td\>
>
> \</tr\> }
>
> \</tbody\>

\</table\>

UserController zapewnia administratorom pełną kontrolę nad zarządzaniem
użytkownikami w aplikacji, umożliwiając przeglądanie, edytowanie,
zarządzanie rolami oraz aktywacją i dezaktywacją kont użytkowników.
Dzięki temu system zarządzania użytkownikami jest intuicyjny i łatwy w
obsłudze dla administratorów.

> **3.** **Modele** **klas**

Modele w aplikacji reprezentują struktury danych przechowywanych w bazie
danych. Każdy model jest odpowiednikiem tabeli w bazie danych i
definiuje właściwości, które dana tabela przechowuje.

**2.1.** **AddressDetails**

AddressDetails to model reprezentujący szczegóły adresowe w aplikacji
**SklepRTV**. Model ten zawiera informacje dotyczące adresu, takie jak
miasto, ulica, numer domu, numer mieszkania oraz prowincja. Model ten
jest używany do przechowywania adresów zarówno klientów, jak i
pracowników sklepu.

**Właściwości:**

> • city (string): Miasto, w którym znajduje się adres. • street
> (string): Ulica, na której znajduje się adres. • houseNo (int): Numer
> domu.
>
> • flatNo (int): Numer mieszkania.
>
> • province (string): Prowincja, w której znajduje się adres.

Model AddressDetails jest często używany jako część większego modelu
(np. Customer lub Worker), gdzie przechowuje szczegóły dotyczące adresu
przypisanego do danej osoby.

Przykład użycia w kontekście modelu Customer:

public class Customer {

> public Guid Id { get; set; }
>
> public string Name { get; set; } = default!;

public AddressDetails AddressDetails { get; set; } = default!; }

W powyższym przykładzie AddressDetails jest częścią modelu Customer, co
pozwala na przechowywanie informacji o adresie klienta bezpośrednio w
modelu klienta.

**2.2.** **Branch**

Branch to model reprezentujący oddział sklepu w aplikacji **SklepRTV**.
Model ten przechowuje informacje dotyczące lokalizacji oddziału, takie
jak miasto, ulica, numer domu, numer mieszkania, identyfikator kraju
oraz prowincja.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator oddziału. Wartość jest generowana
> automatycznie przy tworzeniu nowego oddziału.
>
> • city (string): Miasto, w którym znajduje się oddział. • street
> (string): Ulica, na której znajduje się oddział. • houseNo (int):
> Numer domu.
>
> • flatNo (int): Numer mieszkania.
>
> • countryId (int): Identyfikator kraju, w którym znajduje się oddział.
> • province (string): Prowincja, w której znajduje się oddział.

Model Branch jest używany do przechowywania informacji o różnych
oddziałach sklepu, co umożliwia zarządzanie wieloma lokalizacjami sklepu
w różnych regionach.

Przykład użycia w kontekście tworzenia nowego oddziału:

public void CreateBranch(string city, string street, int houseNo, int
flatNo, int countryId, string province)

{

> var branch = new Branch {
>
> city = city, street = street,
>
> houseNo = houseNo, flatNo = flatNo, countryId = countryId, province =
> province
>
> };
>
> // Dodanie oddziału do bazy danych \_context.Branches.Add(branch);
> \_context.SaveChanges();

}

W powyższym przykładzie tworzony jest nowy oddział z podanymi
szczegółami adresowymi, a następnie dodawany jest do bazy danych.

**2.3.** **Cart**

Cart to model reprezentujący koszyk zakupowy w aplikacji **SklepRTV**.
Model ten przechowuje listę produktów dodanych do koszyka przez
użytkownika oraz oferuje metody do zarządzania tymi produktami, takie
jak dodawanie, usuwanie i obliczanie łącznej wartości koszyka.

**Właściwości:**

> • Items (List\<CartItem\>): Lista produktów (pozycji) w koszyku.

**Metody:**

> • AddItem(Product product, int quantity): Metoda do dodawania produktu
> do koszyka. Jeśli produkt już istnieje w koszyku, metoda zwiększa jego
> ilość.
>
> • RemoveItem(Guid productId): Metoda do usuwania produktu z koszyka na
> podstawie jego identyfikatora.
>
> • CalculateTotal(): Metoda do obliczania łącznej wartości koszyka,
> bazując na cenie i ilości każdego produktu.

**Przykład** **użycia** **w** **kontrolerze:**

Dodawanie produktu do koszyka:

public IActionResult AddToCart(Guid id, int quantity) {

> var product = \_db.Products.FirstOrDefault(p =\> p.Id == id);
>
> if (product != null) {
>
> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.AddItem(product, quantity);
>
> cartJson = JsonSerializer.Serialize(cart);
> HttpContext.Session.SetString("Cart", cartJson);
>
> }

return RedirectToAction("Index"); }

Usuwanie produktu z koszyka:

public IActionResult RemoveFromCart(Guid id) {

> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.RemoveItem(id);
>
> cartJson = JsonSerializer.Serialize(cart);
> HttpContext.Session.SetString("Cart", cartJson);
>
> Debug.WriteLine(\$"Usunięto produkt z koszyka: {id}");

return RedirectToAction("Index"); }

Obliczanie łącznej wartości koszyka:

public ActionResult Checkout() {

> var cartJson = HttpContext.Session.GetString("Cart");
>
> Cart cart = cartJson != null ?
> JsonSerializer.Deserialize\<Cart\>(cartJson) : new Cart();
>
> if (!cart.Items.Any()) {
>
> return RedirectToAction("PlaceOrder"); }

return View(cart); }

Model Cart pozwala użytkownikowi na interakcję z koszykiem zakupowym,
zapewniając wygodne metody zarządzania produktami i obliczania wartości
koszyka, co jest kluczowe dla funkcjonalności sklepu internetowego.
Koszyk jest zarządzany w sesji użytkownika, co umożliwia tymczasowe
przechowywanie danych o zakupach podczas przeglądania sklepu.

**2.4.** **CartItem**

CartItem to model reprezentujący pojedynczą pozycję w koszyku zakupowym
w aplikacji **SklepRTV**. Model ten przechowuje informacje o produkcie
oraz ilości, która została dodana do koszyka.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator pozycji w koszyku.
>
> • Product (Product): Produkt, który został dodany do koszyka.
>
> • Quantity (int): Ilość produktu, która została dodana do koszyka.

Model CartItem jest używany jako część modelu Cart i przechowuje
szczegóły dotyczące produktów, które użytkownik dodał do swojego
koszyka.

**Przykład** **użycia** **w** **kontekście** **modelu** **Cart:**

Dodawanie produktu do koszyka:

public void AddItem(Product product, int quantity) {

> var existingItem = Items.Find(item =\> item.Product.Id == product.Id);
> if (existingItem != null)
>
> {
>
> existingItem.Quantity += quantity; }
>
> else {
>
> Items.Add(new CartItem {
>
> Id = Guid.NewGuid(), Product = product, Quantity = quantity
>
> }); }

}

Usuwanie produktu z koszyka:

public void RemoveItem(Guid productId) {

Items.RemoveAll(item =\> item.Product.Id == productId); }

**Przykład** **użycia** **w** **kontrolerze** **CartController:**

Dodawanie produktu do koszyka w kontrolerze:

public IActionResult AddToCart(Guid id, int quantity) {

> var product = \_db.Products.FirstOrDefault(p =\> p.Id == id);
>
> if (product != null) {
>
> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.AddItem(product, quantity);
>
> cartJson = JsonSerializer.Serialize(cart);
> HttpContext.Session.SetString("Cart", cartJson);
>
> }

return RedirectToAction("Index"); }

Usuwanie produktu z koszyka w kontrolerze:

public IActionResult RemoveFromCart(Guid id) {

> var cartJson = HttpContext.Session.GetString("Cart"); Cart cart;
>
> if (cartJson != null) cart =
> JsonSerializer.Deserialize\<Cart\>(cartJson); else cart = new Cart();
>
> cart.RemoveItem(id);
>
> cartJson = JsonSerializer.Serialize(cart);
>
> HttpContext.Session.SetString("Cart", cartJson);
>
> Debug.WriteLine(\$"Usunięto produkt z koszyka: {id}");

return RedirectToAction("Index"); }

Model CartItem umożliwia szczegółowe zarządzanie poszczególnymi
produktami w koszyku, przechowując informacje o każdym produkcie oraz
jego ilości, co jest kluczowe dla poprawnego obliczania wartości koszyka
i przetwarzania zamówień.

**2.5.** **Category**

Category to model reprezentujący kategorię produktów w aplikacji
**SklepRTV**. Model ten przechowuje informacje o kategoriach, które
umożliwiają organizację produktów w różne grupy tematyczne, co ułatwia
użytkownikom przeglądanie i znajdowanie interesujących ich produktów.

**Właściwości:**

> • Id (int): Unikalny identyfikator kategorii. Jest ustawiany
> automatycznie.
>
> • Name (string): Nazwa kategorii, opisująca rodzaj produktów zawartych
> w danej kategorii.

Model Category jest używany do grupowania produktów, co pozwala na
lepszą organizację oferty sklepu i ułatwia użytkownikom nawigację po
dostępnych produktach.

**Przykład** **użycia** **w** **kontrolerze** **CategoryController:**

Wyświetlanie listy kategorii:

public IActionResult Index() {

> var categories = \_db.Categories.ToList(); return View(categories);

}

Wyświetlanie szczegółów kategorii:

public IActionResult Details(int id) {

> var category = \_db.Categories.FirstOrDefault(p =\> p.Id == id); if
> (category == null) return NotFound();

return View(category); }

Tworzenie nowej kategorii:

\[Authorize(Roles = "Admin")\] public IActionResult Create()

{

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(Category category) {

> if (ModelState.IsValid) {
>
> \_db.Categories.Add(category); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(category); }

Edytowanie istniejącej kategorii:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();

return View(category); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Category category) {

> if (ModelState.IsValid) {
>
> \_db.Categories.Update(category); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(category); }

Usuwanie kategorii:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();

return View(category); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\] public
IActionResult DeletePOST(int id) {

> var category = \_db.Categories.FirstOrDefault(x =\> x.Id == id); if
> (category == null) return NotFound();
> \_db.Categories.Remove(category);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model Category odgrywa kluczową rolę w organizacji produktów w sklepie,
umożliwiając łatwe grupowanie i zarządzanie produktami według ich
kategorii, co poprawia doświadczenie użytkownika podczas przeglądania
oferty sklepu.

**2.6.** **ContactDetails**

ContactDetails to model reprezentujący szczegóły kontaktowe w aplikacji
**SklepRTV**. Model ten przechowuje informacje kontaktowe takie jak
adres e-mail oraz numer telefonu. Jest on używany do przechowywania
danych kontaktowych klientów, pracowników i innych użytkowników systemu.

**Właściwości:**

> • email (string): Adres e-mail użytkownika.
>
> • phone (string): Numer telefonu użytkownika.

Model ContactDetails jest często używany jako część większego modelu
(np. Customer lub Worker), gdzie przechowuje szczegóły kontaktowe
przypisane do danej osoby.

**Przykład** **użycia** **w** **kontekście** **modelu** **Customer:**

public class Customer {

> public Guid Id { get; set; }
>
> public string Name { get; set; } = default!;

public ContactDetails ContactDetails { get; set; } = default!; }

**Przykład** **użycia** **w** **kontrolerze** **CustomerController:**

Tworzenie nowego klienta z danymi kontaktowymi:

\[HttpPost\]

public IActionResult Create(Customer customer) {

> if (ModelState.IsValid) {
>
> \_db.Customers.Add(customer); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(customer); }

Edytowanie danych kontaktowych klienta:

\[HttpPost\]

public IActionResult Edit(Customer customer) {

> if (ModelState.IsValid) {
>
> \_db.Customers.Update(customer); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(customer); }

Wyświetlanie szczegółów klienta:

public IActionResult Details(Guid id) {

> var customer = \_db.Customers.FirstOrDefault(c =\> c.Id == id); if
> (customer == null) return NotFound();

return View(customer); }

Model ContactDetails zapewnia strukturę do przechowywania i zarządzania
informacjami kontaktowymi użytkowników, co jest niezbędne do komunikacji
i obsługi klienta w aplikacji.

**2.7.** **Country**

Country to model reprezentujący kraj w aplikacji **SklepRTV**. Model ten
przechowuje informacje o krajach, które mogą być używane do określania
lokalizacji oddziałów, adresów klientów oraz pracowników.

**Właściwości:**

> • idCountry (int): Unikalny identyfikator kraju. Jest oznaczony
> atrybutem \[Key\], co wskazuje, że jest kluczem głównym tabeli.
>
> • country (string): Nazwa kraju.

Model Country jest używany do przechowywania nazw krajów i ich
identyfikatorów, co umożliwia łatwe zarządzanie i referencjonowanie
krajów w innych modelach.

**Przykład** **użycia** **w** **kontrolerze** **CountryController:**

Wyświetlanie listy krajów:

public IActionResult Index() {

> var countries = \_db.Countries.ToList(); return View(countries);

}

Tworzenie nowego kraju:

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(Country country) {

> if (ModelState.IsValid) {
>
> \_db.Countries.Add(country); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(country); }

Edytowanie istniejącego kraju:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(int id) {

> var country = \_db.Countries.FirstOrDefault(x =\> x.idCountry == id);
> if (country == null) return NotFound();

return View(country); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Country country) {

> if (ModelState.IsValid) {
>
> \_db.Countries.Update(country); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(country); }

Usuwanie kraju:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(int id) {

> var country = \_db.Countries.FirstOrDefault(x =\> x.idCountry == id);
> if (country == null) return NotFound();

return View(country); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\] public
IActionResult DeletePOST(int id) {

> var country = \_db.Countries.FirstOrDefault(x =\> x.idCountry == id);
> if (country == null) return NotFound();
> \_db.Countries.Remove(country);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model Country odgrywa kluczową rolę w zarządzaniu lokalizacjami i
adresami w aplikacji, umożliwiając łatwe przechowywanie i odnajdywanie
informacji o krajach.

**2.8.** **Customer**

Customer to model reprezentujący klienta w aplikacji **SklepRTV**. Model
ten przechowuje szczegóły kontaktowe oraz adresowe klientów sklepu, a
także identyfikatory umożliwiające ich jednoznaczną identyfikację w
systemie.

**Właściwości:**

> • id (Guid): Unikalny identyfikator klienta. Jest generowany
> automatycznie przy tworzeniu nowego klienta.
>
> • userId (Guid): Identyfikator użytkownika powiązanego z danym
> klientem. Jest ustawiany automatycznie.
>
> • contactDetails (ContactDetails): Szczegóły kontaktowe klienta, takie
> jak adres e-mail i numer telefonu.
>
> • addressDetails (AddressDetails): Szczegóły adresowe klienta, takie
> jak miasto, ulica, numer domu i mieszkania oraz prowincja.

Model Customer integruje modele ContactDetails i AddressDetails,
umożliwiając przechowywanie kompleksowych informacji o klientach w
jednym miejscu.

**Przykład** **użycia** **w** **kontrolerze** **CustomerController:**

Tworzenie nowego klienta:

\[HttpPost\]

public IActionResult Create(Customer customer)

{

> if (ModelState.IsValid) {
>
> \_db.Customers.Add(customer); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(customer); }

Edytowanie istniejącego klienta:

\[HttpPost\]

public IActionResult Edit(Customer customer) {

> if (ModelState.IsValid) {
>
> \_db.Customers.Update(customer); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(customer); }

Wyświetlanie szczegółów klienta:

public IActionResult Details(Guid id) {

> var customer = \_db.Customers.FirstOrDefault(c =\> c.id == id); if
> (customer == null) return NotFound();

return View(customer); }

Usuwanie klienta:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(Guid id) {

> var customer = \_db.Customers.FirstOrDefault(c =\> c.id == id); if
> (customer == null) return NotFound();

return View(customer); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeleteConfirmed(Guid id) {

> var customer = \_db.Customers.FirstOrDefault(c =\> c.id == id); if
> (customer == null) return NotFound(); \_db.Customers.Remove(customer);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model Customer jest kluczowy dla zarządzania danymi klientów, integrując
różne informacje kontaktowe i adresowe w jednej strukturze danych.
Dzięki temu aplikacja może łatwo zarządzać relacjami z klientami,
przetwarzać zamówienia oraz prowadzić efektywną obsługę klienta.

**2.9.** **JobPosition**

JobPosition to model reprezentujący stanowisko pracy w aplikacji
**SklepRTV**. Model ten przechowuje informacje o różnych stanowiskach
pracy dostępnych w sklepie, co pozwala na zarządzanie zasobami ludzkimi
i organizacją pracy w sklepie.

**Właściwości:**

> • id (int): Unikalny identyfikator stanowiska pracy.
>
> • name (string): Nazwa stanowiska pracy, opisująca rolę lub funkcję
> pełnioną przez pracownika.

Model JobPosition jest używany do przechowywania i zarządzania
informacjami o różnych stanowiskach pracy, co ułatwia organizację
struktury zatrudnienia w sklepie.

**Przykład** **użycia** **w** **kontrolerze** **JobPositionController:**

Wyświetlanie listy stanowisk pracy:

public IActionResult Index() {

> var jobPositions = \_db.JobPositions.ToList(); return
> View(jobPositions);

}

Wyświetlanie szczegółów stanowiska pracy:

public IActionResult Details(int id) {

> var jobPosition = \_db.JobPositions.FirstOrDefault(j =\> j.id == id);
> if (jobPosition == null) return NotFound();

return View(jobPosition); }

Tworzenie nowego stanowiska pracy:

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(JobPosition jobPosition) {

> if (ModelState.IsValid) {
>
> \_db.JobPositions.Add(jobPosition); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(jobPosition); }

Edytowanie istniejącego stanowiska pracy:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(int id) {

> var jobPosition = \_db.JobPositions.FirstOrDefault(j =\> j.id == id);
> if (jobPosition == null) return NotFound();

return View(jobPosition); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(JobPosition jobPosition) {

> if (ModelState.IsValid) {
>
> \_db.JobPositions.Update(jobPosition); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(jobPosition); }

Usuwanie stanowiska pracy:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(int id) {

> var jobPosition = \_db.JobPositions.FirstOrDefault(j =\> j.id == id);
> if (jobPosition == null) return NotFound();

return View(jobPosition); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeleteConfirmed(int id)

{

> var jobPosition = \_db.JobPositions.FirstOrDefault(j =\> j.id == id);
> if (jobPosition == null) return NotFound();
> \_db.JobPositions.Remove(jobPosition);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model JobPosition pozwala na skuteczne zarządzanie informacjami o
różnych stanowiskach pracy w sklepie, wspierając procesy rekrutacyjne
oraz organizację struktury zatrudnienia. Dzięki temu można łatwo
przypisywać pracowników do odpowiednich ról i monitorować ich pozycje w
firmie.

**2.10.** **Order**

Order to model reprezentujący zamówienie w aplikacji **SklepRTV**. Model
ten przechowuje informacje o zamówieniu, takie jak data zamówienia, dane
klienta, łączna kwota oraz lista pozycji zamówienia.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator zamówienia.
>
> • OrderDate (DateTime): Data złożenia zamówienia. Domyślnie ustawiona
> na bieżącą datę i godzinę.
>
> • CustomerName (string): Imię i nazwisko klienta, który złożył
> zamówienie. • CustomerEmail (string): Adres e-mail klienta.
>
> • CustomerAddress (AddressDetails): Szczegóły adresowe klienta. •
> TotalAmount (decimal): Łączna kwota zamówienia.
>
> • OrderItems (List\<OrderItem\>): Lista pozycji zamówienia,
> przechowująca szczegóły dotyczące każdego produktu w zamówieniu.

Model Order jest kluczowy dla zarządzania zamówieniami klientów,
umożliwiając przechowywanie szczegółowych informacji o zamówieniach.

**Przykład** **użycia** **w** **kontrolerze** **OrderController:**

Tworzenie nowego zamówienia:

\[HttpPost\]

public ActionResult PlaceOrder(string customerName, string
customerEmail, AddressDetails customerAddress)

{

> var cartJson = HttpContext.Session.GetString("Cart");
>
> Cart cart = cartJson != null ?
> JsonSerializer.Deserialize\<Cart\>(cartJson) : new Cart();
>
> if (cart.Items.Any()) {
>
> var order = new Order {
>
> Id = Guid.NewGuid(),
>
> OrderDate = DateTime.Now, CustomerName = customerName, CustomerEmail =
> customerEmail, CustomerAddress = customerAddress, TotalAmount =
> cart.CalculateTotal()
>
> };
>
> foreach (var item in cart.Items) {
>
> order.OrderItems.Add(new OrderItem {
>
> Id = Guid.NewGuid(), OrderId = order.Id, ProductId = item.Product.Id,
>
> ProductName = item.Product.name, Quantity = item.Quantity,
>
> Price = item.Product.price });
>
> }
>
> \_db.Orders.Add(order); \_db.SaveChanges();
>
> HttpContext.Session.Remove("Cart");
>
> return RedirectToAction("OrderConfirmation", new { orderId = order.Id
> }); }

return RedirectToAction("Index", "Cart"); }

Wyświetlanie szczegółów zamówienia:

public IActionResult OrderConfirmation(Guid orderId) {

> var order = \_db.Orders.FirstOrDefault(o =\> o.Id == orderId); if
> (order == null)
>
> {
>
> return RedirectToAction("OrderSubmitingEnd"); }

return View(order); }

**Przykład** **użycia** **w** **widoku:**

Wyświetlanie listy pozycji zamówienia:

\<table\> \<thead\>

> \<tr\> \<th\>Produkt\</th\> \<th\>Ilość\</th\> \<th\>Cena\</th\>
>
> \<th\>Łączna kwota\</th\> \</tr\>
>
> \</thead\> \<tbody\>
>
> @foreach (var item in Model.OrderItems) {
>
> \<tr\> \<td\>@item.ProductName\</td\> \<td\>@item.Quantity\</td\>
> \<td\>@item.Price\</td\>
>
> \<td\>@(item.Price \* item.Quantity)\</td\> \</tr\>
>
> } \</tbody\>

\</table\>

Model Order jest fundamentalnym elementem systemu zarządzania
zamówieniami w aplikacji SklepRTV, umożliwiając kompleksowe
przechowywanie i zarządzanie informacjami o zamówieniach klientów, od
szczegółów kontaktowych po listę zakupionych produktów.

**2.11.** **OrderItem**

OrderItem to model reprezentujący pojedynczą pozycję w zamówieniu w
aplikacji **SklepRTV**. Model ten przechowuje informacje o produkcie
zamówionym przez klienta, takie jak identyfikatory zamówienia i
produktu, nazwa produktu, ilość oraz cena.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator pozycji w zamówieniu.
>
> • OrderId (Guid): Identyfikator zamówienia, do którego należy ta
> pozycja. • ProductId (Guid): Identyfikator produktu.
>
> • ProductName (string): Nazwa produktu.
>
> •      Quantity (int): Ilość zamówionego produktu. •      Price
> (decimal): Cena jednostkowa produktu.

Model OrderItem jest używany jako część modelu Order, przechowując
szczegółowe informacje o każdym produkcie zawartym w zamówieniu.

**Przykład** **użycia** **w** **kontekście** **modelu** **Order:**

Dodawanie pozycji do zamówienia:

var order = new Order {

> Id = Guid.NewGuid(), OrderDate = DateTime.Now,
>
> CustomerName = customerName, CustomerEmail = customerEmail,
> CustomerAddress = customerAddress, TotalAmount = cart.CalculateTotal()

};

foreach (var item in cart.Items) {

> order.OrderItems.Add(new OrderItem {
>
> Id = Guid.NewGuid(), OrderId = order.Id, ProductId = item.Product.Id,
>
> ProductName = item.Product.name, Quantity = item.Quantity,
>
> Price = item.Product.price });

}

\_db.Orders.Add(order); \_db.SaveChanges();

**Przykład** **użycia** **w** **kontrolerze** **OrderController:**

Tworzenie nowego zamówienia z pozycjami:

\[HttpPost\]

public ActionResult PlaceOrder(string customerName, string
customerEmail, AddressDetails customerAddress)

{

> var cartJson = HttpContext.Session.GetString("Cart");
>
> Cart cart = cartJson != null ?
> JsonSerializer.Deserialize\<Cart\>(cartJson) : new Cart();
>
> if (cart.Items.Any()) {
>
> var order = new Order {
>
> Id = Guid.NewGuid(), OrderDate = DateTime.Now,
>
> CustomerName = customerName, CustomerEmail = customerEmail,
> CustomerAddress = customerAddress, TotalAmount = cart.CalculateTotal()
>
> };
>
> foreach (var item in cart.Items) {
>
> order.OrderItems.Add(new OrderItem {
>
> Id = Guid.NewGuid(),
>
> OrderId = order.Id, ProductId = item.Product.Id,
>
> ProductName = item.Product.name, Quantity = item.Quantity,
>
> Price = item.Product.price });
>
> }
>
> \_db.Orders.Add(order); \_db.SaveChanges();
>
> HttpContext.Session.Remove("Cart");
>
> return RedirectToAction("OrderConfirmation", new { orderId = order.Id
> }); }

return RedirectToAction("Index", "Cart"); }

**Przykład** **użycia** **w** **widoku:**

Wyświetlanie szczegółów pozycji zamówienia:

\<table\> \<thead\>

> \<tr\> \<th\>Produkt\</th\> \<th\>Ilość\</th\> \<th\>Cena\</th\>
>
> \<th\>Łączna kwota\</th\> \</tr\>
>
> \</thead\> \<tbody\>
>
> @foreach (var item in Model.OrderItems) {
>
> \<tr\> \<td\>@item.ProductName\</td\> \<td\>@item.Quantity\</td\>
> \<td\>@item.Price\</td\>
>
> \<td\>@(item.Price \* item.Quantity)\</td\> \</tr\>
>
> } \</tbody\>

\</table\>

Model OrderItem jest kluczowy dla zarządzania szczegółami zamówień w
aplikacji SklepRTV, umożliwiając przechowywanie i przetwarzanie
informacji o produktach zawartych w zamówieniach klientów. Dzięki temu
system może dokładnie śledzić, jakie produkty zostały zamówione, w
jakiej ilości i po jakiej cenie, co jest niezbędne do efektywnego
zarządzania sprzedażą.

**2.12.** **Product**

Product to model reprezentujący produkt dostępny w sklepie **SklepRTV**.
Model ten przechowuje szczegółowe informacje o produktach, takie jak
nazwa, cena, opis, dostępna ilość oraz powiązane magazyny i kategorie.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator produktu. Generowany automatycznie
> przy tworzeniu nowego produktu.
>
> •      name (string): Nazwa produktu. •      price (decimal): Cena
> produktu.
>
> • unitId (int): Identyfikator jednostki miary produktu.
>
> • quantity (int): Ilość produktu dostępna w jednostkach miary. •
> description (string): Opis produktu.
>
> • stock (int): Ilość produktu dostępna w magazynie.
>
> • warehouses (Warehouse\[\]): Powiązane magazyny, w których produkt
> jest przechowywany.
>
> • Categories (Category\[\]): Powiązane kategorie, do których należy
> produkt. • image (string?): Ścieżka do obrazu produktu.

Model Product jest kluczowy dla zarządzania ofertą produktów w sklepie,
umożliwiając przechowywanie i zarządzanie szczegółowymi informacjami o
każdym produkcie.

**Przykład** **użycia** **w** **kontrolerze** **ProductController:**

Wyświetlanie listy produktów:

public IActionResult Index() {

> var products = \_db.Products.ToList(); return View(products);

}

Tworzenie nowego produktu:

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public async Task\<IActionResult\> Create(Product product, IFormFile
image) {

> if (image == null) {
>
> Debug.WriteLine("Brak pliku"); return Content("Brak pliku");
>
> }
>
> if (image.Length == 0) {
>
> Debug.WriteLine("Błędny plik"); return Content("Błędny plik");
>
> }
>
> var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
> image.FileName); using (var stream = new FileStream(path,
> FileMode.Create))
>
> {
>
> await image.CopyToAsync(stream); }
>
> product.image = path;
>
> if (ModelState.IsValid) {
>
> \_db.Products.Add(product); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }
>
> else {
>
> var errors = ModelState.Values.SelectMany(x =\> x.Errors); foreach
> (var error in errors)
>
> {
>
> Debug.WriteLine(\$"Błąd modelu: {error.ErrorMessage}"); }
>
> ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku"); }

return View(product); }

Edytowanie istniejącego produktu:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(Guid id) {

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound();

return View(product); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Product product, IFormFile image) {

> if (ModelState.IsValid) {
>
> try
>
> {
>
> if (image != null) {
>
> var uploads = Path.Combine(\_environment.WebRootPath, "uploads"); if
> (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
>
> var filePath = Path.Combine(uploads, image.FileName);
>
> using (var fileStream = new FileStream(filePath, FileMode.Create)) {
>
> image.CopyToAsync(fileStream); }
>
> product.image = \$"/uploads/{image.FileName}"; }
>
> \_db.Products.Update(product); \_db.SaveChangesAsync();
>
> }
>
> catch (Exception ex) {
>
> ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku"); return
> View(product);
>
> }
>
> return RedirectToAction("AdminIndex"); }

return View(product); }

Usuwanie produktu:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(Guid id) {

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound();

return View(product); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeletePOST(Guid id) {

> var product = \_db.Products.FirstOrDefault(x =\> x.Id == id); if
> (product == null) return NotFound(); \_db.Products.Remove(product);
>
> \_db.SaveChanges();

return RedirectToAction("AdminIndex"); }

Model Product umożliwia kompleksowe zarządzanie ofertą produktów w
sklepie, w tym dodawanie nowych produktów, edytowanie istniejących oraz
ich usuwanie. Dzięki integracji z magazynami i kategoriami, system może
efektywnie zarządzać stanami magazynowymi oraz organizacją produktów.
Dodatkowo, obsługa obrazów produktów pozwala na wizualne przedstawienie
oferty sklepu.

**2.13.** **User**

User to model reprezentujący użytkownika w aplikacji **SklepRTV**. Model
ten przechowuje informacje o użytkownikach aplikacji, takie jak
identyfikator, typ użytkownika, nazwisko, adres e-mail, numer telefonu,
nazwa użytkownika oraz hasło.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator użytkownika. Generowany
> automatycznie przy tworzeniu nowego użytkownika.
>
> • Type (int): Typ użytkownika, który może być używany do rozróżniania
> różnych ról (np. admin, klient).
>
> • lastName (string): Nazwisko użytkownika. • email (string): Adres
> e-mail użytkownika.
>
> • phone (string): Numer telefonu użytkownika.
>
> • username (string): Nazwa użytkownika używana do logowania. •
> password (string): Hasło użytkownika.

Model User jest kluczowy dla zarządzania autoryzacją i autentykacją
użytkowników w systemie, umożliwiając przechowywanie i zarządzanie
danymi logowania oraz informacjami kontaktowymi.

**Przykład** **użycia** **w** **kontrolerze** **UserController:**

Wyświetlanie listy użytkowników:

public IActionResult Index() {

> var users = \_userManager.Users.ToList(); return View(users);

}

Wyświetlanie szczegółów użytkownika:

public async Task\<IActionResult\> Details(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();

return View(user); }

Tworzenie nowego użytkownika:

\[HttpPost\]

public async Task\<IActionResult\> Create(User user) {

> if (ModelState.IsValid) {
>
> var newUser = new IdentityUser { UserName = user.username, Email =
> user.email }; var result = await \_userManager.CreateAsync(newUser,
> user.password);
>
> if (result.Succeeded) {
>
> return RedirectToAction("Index"); }
>
> foreach (var error in result.Errors) {
>
> ModelState.AddModelError(string.Empty, error.Description); }
>
> }

return View(user); }

Edytowanie istniejącego użytkownika:

\[Authorize(Roles = "Admin")\]

public async Task\<IActionResult\> Edit(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();

return View(user); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public async Task\<IActionResult\> Edit(User user) {

> if (ModelState.IsValid) {
>
> var existingUser = await
> \_userManager.FindByIdAsync(user.Id.ToString()); if (existingUser !=
> null)
>
> {
>
> existingUser.UserName = user.username; existingUser.Email =
> user.email;
>
> var result = await \_userManager.UpdateAsync(existingUser); if
> (result.Succeeded)
>
> {
>
> return RedirectToAction("Index"); }
>
> foreach (var error in result.Errors) {
>
> ModelState.AddModelError(string.Empty, error.Description); }
>
> } }

return View(user); }

Usuwanie użytkownika:

\[Authorize(Roles = "Admin")\]

public async Task\<IActionResult\> Delete(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();

return View(user); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public async Task\<IActionResult\> DeleteConfirmed(Guid id) {

> var user = await \_userManager.FindByIdAsync(id.ToString()); if (user
> == null) return NotFound();
>
> var result = await \_userManager.DeleteAsync(user); if
> (result.Succeeded)
>
> {
>
> return RedirectToAction("Index"); }
>
> foreach (var error in result.Errors) {
>
> ModelState.AddModelError(string.Empty, error.Description); }

return View(user); }

Model User jest fundamentalnym elementem systemu zarządzania
użytkownikami w aplikacji **SklepRTV**, zapewniając struktury i metody
potrzebne do tworzenia, edytowania, przeglądania oraz usuwania kont
użytkowników. Dzięki temu system może efektywnie zarządzać danymi
użytkowników oraz zapewniać odpowiedni poziom bezpieczeństwa i
autoryzacji.

**2.14.** **UserType**

UserType to model reprezentujący typ użytkownika w aplikacji
**SklepRTV**. Model ten przechowuje informacje o różnych typach
użytkowników, takich jak admin, klient, menedżer itp. Umożliwia to
różnicowanie ról użytkowników w systemie i przydzielanie im odpowiednich
uprawnień.

**Właściwości:**

> • id (int): Unikalny identyfikator typu użytkownika.
>
> • Name (string): Nazwa typu użytkownika, opisująca rolę (np. "Admin",
> "Klient", "Menedżer").

Model UserType jest kluczowy dla zarządzania uprawnieniami użytkowników,
umożliwiając przypisywanie im określonych ról i odpowiednich poziomów
dostępu w aplikacji.

**Przykład** **użycia** **w** **kontrolerze** **UserTypeController:**

Wyświetlanie listy typów użytkowników:

public IActionResult Index() {

> var userTypes = \_db.UserTypes.ToList(); return View(userTypes);

}

Wyświetlanie szczegółów typu użytkownika:

public IActionResult Details(int id) {

> var userType = \_db.UserTypes.FirstOrDefault(u =\> u.id == id); if
> (userType == null) return NotFound();

return View(userType); }

Tworzenie nowego typu użytkownika:

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(UserType userType) {

> if (ModelState.IsValid) {
>
> \_db.UserTypes.Add(userType); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(userType); }

Edytowanie istniejącego typu użytkownika:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(int id) {

> var userType = \_db.UserTypes.FirstOrDefault(u =\> u.id == id); if
> (userType == null) return NotFound();

return View(userType); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(UserType userType) {

> if (ModelState.IsValid) {
>
> \_db.UserTypes.Update(userType); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(userType); }

Usuwanie typu użytkownika:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(int id) {

> var userType = \_db.UserTypes.FirstOrDefault(u =\> u.id == id); if
> (userType == null) return NotFound();

return View(userType); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeleteConfirmed(int id) {

> var userType = \_db.UserTypes.FirstOrDefault(u =\> u.id == id); if
> (userType == null) return NotFound(); \_db.UserTypes.Remove(userType);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model UserType umożliwia efektywne zarządzanie typami użytkowników i ich
uprawnieniami, co jest niezbędne do utrzymania porządku i bezpieczeństwa
w aplikacji. Dzięki temu można łatwo przypisywać różne role użytkownikom
i kontrolować ich dostęp do różnych części systemu.

**2.15.** **Warehouse**

Warehouse to model reprezentujący magazyn w aplikacji **SklepRTV**.
Model ten przechowuje informacje o magazynach, takie jak unikalny
identyfikator, nazwa oraz identyfikator oddziału, do którego magazyn
należy.

**Właściwości:**

> • Id (Guid): Unikalny identyfikator magazynu. Generowany automatycznie
> przy tworzeniu nowego magazynu.
>
> • name (string): Nazwa magazynu.
>
> • branchId (Guid): Identyfikator oddziału, do którego należy magazyn.
> Jest ustawiany automatycznie.

Model Warehouse jest używany do przechowywania informacji o
lokalizacjach magazynowych, co umożliwia zarządzanie stanami
magazynowymi oraz alokacją produktów w różnych oddziałach sklepu.

**Przykład** **użycia** **w** **kontrolerze** **WarehouseController:**

Wyświetlanie listy magazynów:

public IActionResult Index() {

> var warehouses = \_db.Warehouses.ToList(); return View(warehouses);

}

Wyświetlanie szczegółów magazynu:

public IActionResult Details(Guid id) {

> var warehouse = \_db.Warehouses.FirstOrDefault(w =\> w.Id == id); if
> (warehouse == null) return NotFound();

return View(warehouse); }

Tworzenie nowego magazynu:

\[Authorize(Roles = "Admin")\] public IActionResult Create() {

return View(); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Create(Warehouse warehouse) {

> if (ModelState.IsValid) {
>
> \_db.Warehouses.Add(warehouse); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(warehouse); }

Edytowanie istniejącego magazynu:

\[Authorize(Roles = "Admin")\] public IActionResult Edit(Guid id) {

> var warehouse = \_db.Warehouses.FirstOrDefault(w =\> w.Id == id); if
> (warehouse == null) return NotFound();

return View(warehouse); }

\[Authorize(Roles = "Admin")\] \[HttpPost\]

public IActionResult Edit(Warehouse warehouse) {

> if (ModelState.IsValid) {
>
> \_db.Warehouses.Update(warehouse); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(warehouse); }

Usuwanie magazynu:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(Guid id) {

> var warehouse = \_db.Warehouses.FirstOrDefault(w =\> w.Id == id); if
> (warehouse == null) return NotFound();

return View(warehouse); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeleteConfirmed(Guid id) {

> var warehouse = \_db.Warehouses.FirstOrDefault(w =\> w.Id == id); if
> (warehouse == null) return NotFound();
> \_db.Warehouses.Remove(warehouse);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model Warehouse umożliwia efektywne zarządzanie lokalizacjami
magazynowymi w systemie, wspierając procesy logistyczne i operacyjne
sklepu. Dzięki temu system może śledzić, gdzie przechowywane są produkty
i zarządzać stanami magazynowymi w różnych oddziałach.

**2.16.** **Worker**

Worker to model reprezentujący pracownika sklepu w aplikacji
**SklepRTV**. Model ten przechowuje informacje o pracownikach, takie jak
identyfikator, dane kontaktowe oraz adresowe, oraz identyfikator
powiązanego użytkownika.

**Właściwości:**

> • id (Guid): Unikalny identyfikator pracownika. Generowany
> automatycznie przy tworzeniu nowego pracownika.
>
> • userId (Guid): Identyfikator użytkownika powiązanego z pracownikiem.
> Jest ustawiany automatycznie.
>
> • contactDetails (ContactDetails): Szczegóły kontaktowe pracownika,
> takie jak adres e-mail i numer telefonu.
>
> • addressDetails (AddressDetails): Szczegóły adresowe pracownika,
> takie jak miasto, ulica, numer domu i mieszkania oraz prowincja.

Model Worker integruje modele ContactDetails i AddressDetails,
umożliwiając przechowywanie kompleksowych informacji o pracownikach w
jednym miejscu.

**Przykład** **użycia** **w** **kontrolerze** **WorkerController:**

Tworzenie nowego pracownika:

\[HttpPost\]

public IActionResult Create(Worker worker) {

> if (ModelState.IsValid) {
>
> \_db.Workers.Add(worker); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(worker); }

Edytowanie istniejącego pracownika:

\[HttpPost\]

public IActionResult Edit(Worker worker) {

> if (ModelState.IsValid) {
>
> \_db.Workers.Update(worker); \_db.SaveChanges();
>
> return RedirectToAction("Index"); }

return View(worker); }

Wyświetlanie szczegółów pracownika:

public IActionResult Details(Guid id) {

> var worker = \_db.Workers.FirstOrDefault(w =\> w.id == id); if (worker
> == null) return NotFound();

return View(worker); }

Usuwanie pracownika:

\[Authorize(Roles = "Admin")\] public IActionResult Delete(Guid id) {

> var worker = \_db.Workers.FirstOrDefault(w =\> w.id == id); if (worker
> == null) return NotFound();

return View(worker); }

\[Authorize(Roles = "Admin")\] \[HttpPost, ActionName("Delete")\]

public IActionResult DeleteConfirmed(Guid id) {

> var worker = \_db.Workers.FirstOrDefault(w =\> w.id == id); if (worker
> == null) return NotFound(); \_db.Workers.Remove(worker);
>
> \_db.SaveChanges();

return RedirectToAction("Index"); }

Model Worker jest kluczowy dla zarządzania danymi pracowników w
aplikacji, integrując różne informacje kontaktowe i adresowe w jednej
strukturze danych. Dzięki temu aplikacja może łatwo zarządzać relacjami
z pracownikami, przypisywać im odpowiednie role i monitorować ich dane
kontaktowe.

> **4.** **Migracje**

Migracje są zarządzane za pomocą narzędzia Entity Framework Core, które
umożliwia aktualizację schematu bazy danych w kontrolowany sposób.
Migracje pozwalają na wprowadzanie zmian w strukturze bazy danych w
sposób inkrementalny, co ułatwia zarządzanie wersjonowaniem bazy danych.

**Tworzenie** **migracji**

Aby utworzyć nową migrację, użyj następującej komendy:

dotnet ef migrations add \<NazwaMigracji\>

Tworzy to nowy plik migracji w katalogu Migrations, który zawiera
operacje potrzebne do wprowadzenia zmian w bazie danych.

**Migracja** **Init**

Migracja Init tworzy początkowe tabele dla aplikacji, takie jak
AspNetRoles, AspNetUsers, Branches, Countries, Customers, JobPositions,
Orders, Workers, Products, Categories, i Warehouses. Definiuje również
odpowiednie klucze podstawowe i relacje między tabelami.

**Migracja** **Init2**

Migracja Init2 wprowadza zmianę nazwy kolumny w tabeli Products,
zmieniając imagePath na image.

**Migracja** **Init3**

Migracja Init3 modyfikuje kolumnę image w tabeli Products, aby była
opcjonalna (nullable).

**Migracja** **OrdersChanges**

Migracja OrdersChanges wprowadza następujące zmiany:

> • Usuwa relację pomiędzy Products a Orders.
>
> • Dodaje szczegółowe informacje o adresie klienta oraz inne dane
> klienta do tabeli Orders.
>
> • Tworzy nową tabelę OrderItems, która przechowuje informacje o
> pozycjach w zamówieniu, takich jak identyfikator produktu, nazwa
> produktu, ilość oraz cena.

**Model** **Snapshot**

Model snapshot (ApplicationDbContextModelSnapshot) jest automatycznie
generowanym plikiem, który przechowuje aktualny stan modelu bazy danych.
Używany jest przez Entity Framework Core do porównywania bieżącego
modelu z modelem bazy danych podczas tworzenia nowych migracji. Snapshot
zapewnia, że każda migracja zawiera tylko zmiany wprowadzone od
ostatniej migracji, co pozwala na precyzyjne śledzenie i zarządzanie
zmianami w schemacie bazy danych.

**Zastosowanie** **migracji** **do** **bazy** **danych**

Aby zastosować migracje do bazy danych, użyj następującej komendy:

dotnet ef database update

Komenda ta zastosuje wszystkie niezaaplikowane migracje do bazy danych,
zapewniając, że struktura bazy danych jest zsynchronizowana z modelem
danych w aplikacji.

Migracje umożliwiają wersjonowanie schematu bazy danych oraz
wprowadzanie zmian w sposób kontrolowany, co jest kluczowe dla
utrzymania spójności danych oraz łatwości zarządzania bazą danych
podczas rozwoju aplikacji.

**Widoki** **w** **aplikacji** **SklepRTV**

Katalog "Views" w aplikacji SklepRTV zawiera pliki widoków Razor
(.cshtml), które są używane do renderowania interfejsu HTML dla
aplikacji. Każdy kontroler w aplikacji

zazwyczaj ma odpowiadający mu folder w katalogu "Views", zawierający
pliki widoków dla działań zdefiniowanych w kontrolerze.

**Admin**

> • **Index.cshtml**: Główny interfejs administracyjny. Widok ten
> umożliwia administratorom dostęp do różnych funkcji zarządzania,
> takich jak zarządzanie użytkownikami, produktami, zamówieniami oraz
> przeglądanie raportów i statystyk.

**Cart**

> • **Index.cshtml**: Wyświetla zawartość koszyka zakupów użytkownika.
> Użytkownicy mogą przeglądać produkty, które dodali do koszyka, oraz
> mogą usuwać produkty lub zmieniać ich ilość.

**Category**

> • **Create.cshtml**: Formularz umożliwiający tworzenie nowej kategorii
> produktów. Administratorzy mogą wprowadzić nazwę i inne szczegóły
> dotyczące kategorii.
>
> • **Delete.cshtml**: Widok potwierdzający usunięcie istniejącej
> kategorii produktów. Przed usunięciem kategorii wyświetlane są jej
> szczegóły w celu potwierdzenia decyzji.
>
> • **Details.cshtml**: Wyświetla szczegóły określonej kategorii
> produktów. Użytkownicy mogą zobaczyć informacje na temat kategorii
> oraz listę produktów do niej przypisanych.
>
> • **Edit.cshtml**: Formularz umożliwiający edytowanie istniejącej
> kategorii produktów. Administratorzy mogą zmieniać nazwę i inne
> szczegóły kategorii.
>
> • **Index.cshtml**: Wyświetla listę wszystkich kategorii produktów.
> Użytkownicy mogą przeglądać wszystkie dostępne kategorie w sklepie.

**Home**

> • **Index.cshtml**: Strona główna aplikacji. Wita użytkowników i
> zapewnia linki nawigacyjne do różnych sekcji sklepu.
>
> • **Privacy.cshtml**: Strona informacyjna o polityce prywatności
> sklepu. Użytkownicy mogą dowiedzieć się, jak ich dane osobowe są
> przetwarzane i chronione.

**Order**

> • **Checkout.cshtml**: Strona finalizacji zamówienia, gdzie
> użytkownicy finalizują swoje zakupy. Formularz zawiera informacje
> dotyczące płatności i dostawy.
>
> • **OrderConfirmation.cshtml**: Wyświetla szczegóły potwierdzenia
> zamówienia po jego pomyślnym złożeniu. Użytkownicy mogą zobaczyć
> podsumowanie zamówienia i informacje o dostawie.
>
> • **OrderSubmittingEnd.cshtml**: Strona informująca o zakończeniu
> procesu składania zamówienia. Potwierdza użytkownikom, że ich
> zamówienie zostało pomyślnie złożone i przetwarzane.

**Product**

> • **AdminIndex.cshtml**: Widok administracyjny do zarządzania
> produktami, dostępny tylko dla administratorów. Administratorzy mogą
> przeglądać, edytować, usuwać i dodawać nowe produkty.
>
> • **Create.cshtml**: Formularz umożliwiający tworzenie nowego
> produktu. Administratorzy mogą wprowadzić szczegóły produktu, takie
> jak nazwa, cena, opis i zdjęcie.
>
> • **Delete.cshtml**: Widok potwierdzający usunięcie istniejącego
> produktu. Przed usunięciem produktu wyświetlane są jego szczegóły w
> celu potwierdzenia decyzji.
>
> • **Details.cshtml**: Wyświetla szczegóły określonego produktu.
> Użytkownicy mogą zobaczyć pełne informacje o produkcie, takie jak
> nazwa, opis, cena, dostępność w magazynie i zdjęcie.
>
> • **Edit.cshtml**: Formularz umożliwiający edytowanie istniejącego
> produktu. Administratorzy mogą zmieniać wszystkie szczegóły produktu,
> w tym dodawać nowe zdjęcia.
>
> • **Index.cshtml**: Wyświetla listę wszystkich produktów dostępnych w
> sklepie. Użytkownicy mogą przeglądać wszystkie produkty i uzyskać
> dostęp do ich szczegółów.

**Shared**

> • **\_Layout.cshtml**: Główny układ strony zawierający wspólne
> elementy HTML i kod Razor, zapewniający spójność na różnych widokach.
> Zawiera nagłówek, stopkę i sekcje nawigacyjne, które są wspólne dla
> wszystkich stron w aplikacji.
>
> • **\_LoginPartial.cshtml**: Częściowy widok dla sekcji logowania,
> zazwyczaj włączony w układ. Umożliwia użytkownikom zalogowanie się lub
> wylogowanie z aplikacji.
>
> • **\_ProductsNavPartial.cshtml**: Częściowy widok dla sekcji
> nawigacyjnej produktów. Zawiera linki do różnych kategorii produktów,
> umożliwiając łatwe przeglądanie.
>
> • **\_ValidationScriptsPartial.cshtml**: Częściowy widok zawierający
> skrypty walidacyjne, zapewniający poprawność danych w formularzach.
>
> •      **Error.cshtml**: Strona błędów wyświetlająca komunikaty o
> błędach i ślady stosu. Użytkownicy mogą zobaczyć szczegóły błędów,
> które wystąpiły w aplikacji.

**User**

> • **Details.cshtml**: Wyświetla szczegóły określonego użytkownika.
> Administratorzy mogą przeglądać informacje o użytkowniku, takie jak
> imię, nazwisko, e-mail i role.
>
> • **Index.cshtml**: Wyświetla listę wszystkich użytkowników do celów
> administracyjnych. Administratorzy mogą przeglądać, edytować i
> zarządzać użytkownikami.
>
> • **\_ViewImports.cshtml**: Zawiera wspólne dyrektywy Razor,
> importowane do widoków. Umożliwia to współdzielenie dyrektyw między
> różnymi widokami.
>
> • **\_ViewStart.cshtml**: Określa układ używany domyślnie przez
> wszystkie widoki. Zapewnia spójność układu w całej aplikacji.

**Dodatkowe** **Pliki**

> • **appsettings.json**: Plik konfiguracyjny ustawień aplikacji.
> Zawiera informacje o połączeniach do bazy danych, ustawienia logowania
> i inne konfiguracje.
>
> • **Program.cs**: Główny punkt wejścia aplikacji, konfigurujący i
> uruchamiający serwer [<u>WWW</u>.](http://www/) Zawiera kod
> inicjalizujący aplikację i konfigurujący jej usługi.
>
> • **SklepRTV.Test**: Katalog zawierający testy jednostkowe aplikacji.
> Testy te są używane do weryfikacji poprawności działania
> poszczególnych części aplikacji.

Pliki widoków odpowiadają działaniom zdefiniowanym w ich odpowiednich
kontrolerach i zapewniają interfejs użytkownika do interakcji z różnymi
funkcjami aplikacji SklepRTV. Każdy widok jest odpowiedzialny za
renderowanie konkretnej części interfejsu użytkownika, umożliwiając
użytkownikom wykonywanie różnych czynności, takich jak przeglądanie
produktów, składanie zamówień i zarządzanie kontem.

> **5.** **ErrorViewModel**

ErrorViewModel to prosty model wykorzystywany do obsługi błędów w
aplikacji. Jest zdefiniowany w przestrzeni nazw SklepRTV.MVC.Models i
zawiera dwie właściwości:

> • RequestId: opcjonalny identyfikator żądania, który może być używany
> do śledzenia konkretnych błędów.
>
> • ShowRequestId: właściwość logiczna, która zwraca wartość true, jeśli
> RequestId nie jest pusty lub null. Jest używana do określenia, czy
> identyfikator żądania powinien być wyświetlany w widoku błędów.

Model ten jest często używany w aplikacjach ASP.NET Core do
przekazywania informacji o błędach do widoków, umożliwiając użytkownikom
uzyskanie dodatkowych szczegółów na temat napotkanych problemów.

**Przykład** **użycia** **w** **kontrolerze**

ErrorViewModel jest zazwyczaj używany w kontrolerze, który obsługuje
błędy aplikacji. Oto przykładowy fragment kodu z kontrolera
HomeController:

\[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,
NoStore = true)\] public IActionResult Error()

{

return View(new ErrorViewModel { RequestId = Activity.Current?.Id ??
HttpContext.TraceIdentifier });

}

W powyższym kodzie, metoda Error tworzy nową instancję ErrorViewModel i
przypisuje do właściwości RequestId identyfikator bieżącego żądania lub
identyfikator śledzenia z kontekstu HTTP. Model jest następnie
przekazywany do widoku Error, który może wyświetlić szczegóły błędu
użytkownikowi.

> **6.** **Tworzenie** **i** **zarządzanie** **bazą** **danych**

Baza danych w aplikacji **SklepRTV** jest zarządzana za pomocą Entity
Framework Core (EF Core), co umożliwia łatwe definiowanie i
manipulowanie schematem bazy danych.

**5.1** **Kontekst** **bazy** **danych**

Kontekst bazy danych (DatabaseContext oraz ApplicationDbContext) jest
centralnym miejscem, które definiuje tabele i relacje między nimi. Klasy
te dziedziczą po DbContext i zawierają właściwości DbSet reprezentujące
kolekcje encji.

**DatabaseContext**

Przykład kodu kontekstu bazy danych dla DatabaseContext:

using Microsoft.EntityFrameworkCore; using SklepRTV.Model;

using System;

namespace SklepRTV.Database {

> public class DatabaseContext : DbContext {
>
> public DatabaseContext(DbContextOptions\<DatabaseContext\> options) :
> base(options) {
>
> }
>
> public DbSet\<Branch\> Branches { get; set; } public DbSet\<Category\>
> Categories { get; set; } public DbSet\<Country\> Countries { get; set;
> } public DbSet\<Customer\> Customers { get; set; }
>
> public DbSet\<JobPosition\> JobPositions { get; set; } public
> DbSet\<Order\> Orders { get; set; }
>
> public DbSet\<Product\> Products { get; set; }
>
> public DbSet\<Warehouse\> Warehouses { get; set; } public
> DbSet\<Worker\> Workers { get; set; }
>
> protected override void OnModelCreating(ModelBuilder modelBuilder) {
>
> // Definiowanie relacji pomiędzy encjami
> modelBuilder.Entity\<Customer\>().OwnsOne(a =\> a.addressDetails);
> modelBuilder.Entity\<Worker\>().OwnsOne(a =\> a.addressDetails);
>
> modelBuilder.Entity\<Customer\>().OwnsOne(c =\> c.contactDetails);
> modelBuilder.Entity\<Worker\>().OwnsOne(c =\> c.contactDetails);
>
> } }

}

**ApplicationDbContext**

Przykład kodu kontekstu bazy danych dla ApplicationDbContext:

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders; using
SklepRTV.Model;

namespace SklepRTV.MVC.Data {

> public class ApplicationDbContext : IdentityDbContext\<IdentityUser\>
> {
>
> public ApplicationDbContext(DbContextOptions\<ApplicationDbContext\>
> options) : base(options)
>
> { }
>
> public DbSet\<Branch\> Branches { get; set; } public DbSet\<Category\>
> Categories { get; set; } public DbSet\<Country\> Countries { get; set;
> } public DbSet\<Customer\> Customers { get; set; }
>
> public DbSet\<JobPosition\> JobPositions { get; set; } public
> DbSet\<Order\> Orders { get; set; }
>
> public DbSet\<OrderItem\> OrderItems { get; set; } public
> DbSet\<Product\> Products { get; set; }
>
> public DbSet\<Warehouse\> Warehouses { get; set; } public
> DbSet\<Worker\> Workers { get; set; }
>
> protected override void OnModelCreating(ModelBuilder modelBuilder) {
>
> base.OnModelCreating(modelBuilder);
>
> modelBuilder.Entity\<Customer\>().OwnsOne(a =\> a.addressDetails);
> modelBuilder.Entity\<Worker\>().OwnsOne(a =\> a.addressDetails);
>
> modelBuilder.Entity\<Customer\>().OwnsOne(c =\> c.contactDetails);
> modelBuilder.Entity\<Worker\>().OwnsOne(c =\> c.contactDetails);
>
> modelBuilder.Entity\<Order\>().OwnsOne(o =\> o.CustomerAddress);
>
> modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration()); }
>
> }
>
> public class ApplicationUserConfiguration :
> IEntityTypeConfiguration\<IdentityUser\> {
>
> public void Configure(EntityTypeBuilder\<IdentityUser\> builder) {
>
> // Konfiguracja encji IdentityUser }

} }

**5.2** **Modelowanie** **relacji**

Relacje pomiędzy tabelami są definiowane w metodzie OnModelCreating. W
powyższych przykładach, Customer oraz Worker posiadają własne szczegóły
adresowe i kontaktowe, które są zdefiniowane jako własności (Owned
Entities) w Entity Framework Core. Dzięki temu szczegóły te są
przechowywane w tej samej tabeli co główne encje (Customer i Worker),
ale są traktowane jako osobne obiekty w kodzie.

**5.3** **Plik** **połączenia** **z** **bazą** **danych**

Ciąg połączenia do bazy danych jest przechowywany w pliku
appsettings.json. Jest to centralne miejsce, w którym konfigurujemy
dostęp do bazy danych. Oto przykład konfiguracji połączenia:

{

> "Logging": { "LogLevel": {
>
> "Default": "Information", "Microsoft.AspNetCore": "Warning"
>
> } },
>
> "AllowedHosts": "\*", "ConnectionStrings": {

"SklepRTV":
"Server=(localdb)\\mssqllocaldb;Database=SklepRTV;Trusted_Connection=True;",

"IdentityDbContextConnection":
"Server=(localdb)\\mssqllocaldb;Database=SklepRTV.MVC;Trusted_Connection=True;Multi
pleActiveResultSets=true"

} }

Konfiguracja ta umożliwia aplikacji nawiązanie połączenia z bazą danych
SQL Server, używając lokalnego serwera baz danych (localdb), który jest
łatwy do skonfigurowania i użycia podczas developmentu.
