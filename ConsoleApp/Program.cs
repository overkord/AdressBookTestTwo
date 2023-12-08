using ConsoleApp.Models; 

class Program
{
    static void Main(string[] args)
    {
        var contactService = new ContactService();

        while (true)
        {
            Console.WriteLine("Välkommen till kontaktlistan!");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Lägg till ny kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Ta bort kontakt");
            Console.WriteLine("4. Avsluta");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Felaktig inmatning. Välj ett giltigt alternativ.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddNewContact(contactService);
                    break;
                case 2:
                    ShowAllContacts(contactService);
                    break;
                case 3:
                    RemoveContact(contactService);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Välj igen.");
                    break;
            }
        }
    }

    static void AddNewContact(ContactService contactService)
    {
        do
        {
            Console.WriteLine("Lägg till ny kontakt:");
            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Telefonnummer: ");
            string phoneNumber = Console.ReadLine()!;

            Console.Write("E-postadress: ");
            string email = Console.ReadLine()!;

            Console.Write("Stad: ");
            string city = Console.ReadLine()!;

            Console.Write("Adress: ");
            string address = Console.ReadLine()!;

            Console.Write("Postnummer: ");
            string postalCode = Console.ReadLine()!;


            var newContact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                PostalCode = postalCode,
                City = city
            };

            contactService.AddContact(newContact);
            Console.WriteLine("Kontakt tillagd!");

            Console.WriteLine("Vill du ange en till kontakt? (Ja/Nej)");
            string addAnother = Console.ReadLine()!;
            if (addAnother.Trim().Equals("Nej", StringComparison.OrdinalIgnoreCase))
                break;

        } while (true);
    }

    static void ShowAllContacts(ContactService contactService)
    {
        var allContacts = contactService.GetAllContacts();
        if (allContacts.Count == 0)
        {
            Console.WriteLine("Det finns inga kontakter att visa.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Alla kontakter:");
        foreach (var contact in allContacts)
        {
            Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}, E-post: {contact.Email}, Telefonnummer: {contact.PhoneNumber}, Adress: {contact.Address}, PostalCode: {contact.PostalCode}, City: {contact.City}");
           
        }

        Console.WriteLine("\nTryck på valfri knapp för att återgå till menyn.");
        Console.ReadKey();
    }

    static void RemoveContact(ContactService contactService)
    {
        do
        {
            var allContacts = contactService.GetAllContacts();

            if (allContacts.Count == 0)
            {
                Console.WriteLine("Det finns inga kontakter att ta bort.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Vilken kontakt vill du ta bort? Välj genom att ange siffran:");
            for (int i = 0; i < allContacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allContacts[i].Email}");
            }

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > allContacts.Count)
            {
                Console.WriteLine("Ogiltigt val. Välj igen.");
                Console.ReadKey();
                continue;
            }

            var contactToRemove = allContacts[choice - 1];
            contactService.RemoveContact(contactToRemove.Email);
            Console.WriteLine($"Kontakten med e-postadressen '{contactToRemove.Email}' har tagits bort.");

            Console.WriteLine("Vill du ta bort fler kontakter? (Ja/Nej)");
            string removeMore = Console.ReadLine()!;
            if (removeMore.Trim().Equals("Nej", StringComparison.OrdinalIgnoreCase))
                break;

        } while (true);
    }
}
