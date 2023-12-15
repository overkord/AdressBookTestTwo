using ConsoleApp.Models; //använder models mappen för att använda contact.cs
using Newtonsoft.Json; // hantering av json

/// <summary>
/// skapar en lista för att lagra kontakter och sökväg till json filen
/// </summary>
public class ContactService
{
    private List<Contact> contacts = new List<Contact>();
    private readonly string filePath = (@"C:\Projects\contacts.json"); 
    
    /// <summary>
    /// Skapar kunstruktor för ContactService och laddar kontakter vid skapandet av tjänsten 
    /// </summary>
    public ContactService()
    {
        LoadContacts();  // laddar kontakter
    }
    
    /// <summary>
    /// lägger till kontakt och sparar den till filen
    /// </summary>
    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
        SaveContacts();
    }

    /// <summary>
    /// tar bort en kontakt med specifik email adress ifrån listan och sparar ändringar
    /// </summary>
    private void RemoveContact(string email)
    {
        var contactToRemove = contacts.FirstOrDefault(c => c.Email == email);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            SaveContacts();
        }
    }

    /// <summary>
    /// hämtar alla kontakter i listan
    /// </summary>
    public List<Contact> GetAllContacts()
    {
        return contacts;
    }

    /// <summary>
    /// hämtar en specifik kontakt baserat på email adress
    /// </summary>
    public Contact GetContactByEmail(string email)
    {
        return contacts.FirstOrDefault(c => c.Email == email)!;
    }

    /// <summary>
    /// hämtar kontakter ifrån json filen om den existerar
    /// </summary>
    private void LoadContacts()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json)!;
        }
    }

    /// <summary>
    /// sparar alla kontakter till json filen
    /// </summary>
    public void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(contacts);
        File.WriteAllText(filePath, json);
    }


    /// <summary>
    /// kollar om användaren skrivit in någonting i fältet, om värdet är tomt returneras två stycken writelines, annars går den vidare
    /// </summary>
    static string ReadInput()
    {
        var input = "";
        while (true)
        {
            input = Console.ReadLine()!;
            if (input.Length == 0)
            {
                Console.WriteLine("Du har matat in ett tomt värde");
                Console.WriteLine("Mata in nytt giltigt värde: ");
            }
            else
            {
                break;
            }
        }
        return input;
    }

    /// <summary>
    /// lägger till ny kontakt med användarens inmatning. här används readinput för att kolla så att ett värde är inskrivet
    /// </summary>
    public void AddNewContact()
    {
        do
        {
            Console.WriteLine("Lägg till ny kontakt:");
            Console.Write("Förnamn: ");
            var firstName = ReadInput();

            Console.Write("Efternamn: ");
            string lastName = ReadInput();

            Console.Write("Telefonnummer: ");
            string phoneNumber = ReadInput();

            Console.Write("E-postadress: ");
            string email = ReadInput();

            Console.Write("Stad: ");
            string city = ReadInput();

            Console.Write("Adress: ");
            string address = ReadInput();

            Console.Write("Postnummer: ");
            string postalCode = ReadInput();


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

            AddContact(newContact);
            Console.WriteLine("Kontakt tillagd!");

            Console.WriteLine("Vill du ange en till kontakt? (Ja/Nej)");
            string addAnother = Console.ReadLine()!;
            if (addAnother.Trim().Equals("Nej", StringComparison.OrdinalIgnoreCase))
                break;

        } while (true);
    }


    /// <summary>
    /// visar alla kontakter om det finns några i listan
    /// </summary>
    public void ShowAllContacts()
    {
        var allContacts = GetAllContacts();
        if (allContacts.Count == 0)
        {
            Console.WriteLine("Det finns inga kontakter att visa.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Alla kontakter:");
        foreach (var contact in allContacts)
        {
            Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName} \n E-post: {contact.Email} \n-----------------------");

        }

        Console.WriteLine("\nTryck på valfri knapp för att återgå till menyn.");
        Console.ReadKey();
    }
     /// <summary>
     /// tar bort kontakter med ett index värde, (kanske ska ändra till inmatning av email på denna?)
     /// </summary>
    public void RemoveContact()
    {
        do
        {
            var allContacts = GetAllContacts();

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
            RemoveContact(contactToRemove.Email);
            Console.WriteLine($"Kontakten med e-postadressen '{contactToRemove.Email}' har tagits bort.");

            Console.WriteLine("Vill du ta bort fler kontakter? (Ja/Nej)");
            string removeMore = Console.ReadLine()!;
            if (removeMore.Trim().Equals("Nej", StringComparison.OrdinalIgnoreCase))
                break;

        } while (true);
    }

    /// <summary>
    /// visar alla kontakter i listan med val att visa mer detaljerad information om en specifik användare ifrån en lista
    /// </summary>
    public void DisplayContactInformation()
    {
        do
        {
            var allContacts = GetAllContacts();

            if (allContacts.Count == 0)
            {
                Console.WriteLine("Det finns inga kontakter att visa.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Vilken kontakt vill du veta mer om? Välj genom att ange siffran:");
            for (int i = 0; i < allContacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allContacts[i].LastName}");
            }

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > allContacts.Count)
            {
                Console.WriteLine("Ogiltigt val. Välj igen.");
                Console.ReadKey();
                continue;
            }

            var contactToShow = allContacts[choice - 1];
            DisplayIndividualContactInformation(contactToShow);

            Console.WriteLine("Vill du veta mer om någon annan kontakt? (Ja/Nej)");
            string viewMore = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(viewMore) && viewMore.Trim().Equals("Nej", StringComparison.OrdinalIgnoreCase))
                break;

        } while (true);
    }
    
    public void DisplayIndividualContactInformation(Contact contact)
    {

        Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName} \n E-post: {contact.Email}\n Telefonnummer: {contact.PhoneNumber}\n Adress: {contact.Address}, PostalCode: {contact.PostalCode}, City: {contact.City}, \n-----------------------");

    }
}
