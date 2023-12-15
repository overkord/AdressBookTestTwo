public class MenuService
{
    private readonly ContactService _contactService; // En sak för att göra saker med kontakter

    /// <summary>
    /// Startar allting för att kunna göra saker med kontakterna.
    /// </summary>
    public MenuService(ContactService contactService)
    {
        _contactService = contactService; // Sätter upp det vi behöver för att hantera kontakter
    }

    /// <summary>
    /// Visar en lista med saker vi kan göra med kontakterna och låter användaren välja vad som ska göras.
    /// </summary>
    public void Load()
    {
        while (true) // Fortsätter att visa alternativen tills vi vill sluta
        {
            Console.WriteLine("Välkommen till kontaktlistan!");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Lägg till ny kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Ta bort kontakt");
            Console.WriteLine("4. Visa detaljerad information om en kontakt");
            Console.WriteLine("5. Avsluta");

            int choice;
            // Läser in vad användaren skriver
            if (!int.TryParse(Console.ReadLine(), out choice)) // Kollar om det är ett nummer vi skrev
            {
                Console.WriteLine("Hoppsan, det där var inte ett nummer. Försök igen!");
                continue; // Om det inte var ett nummer, säg till och börja om
            }


            // Kollar vilket alternativ vi valde och kör funktionen för det alternativet från ContactService
            switch (choice)
            {
                case 1:
                    _contactService.AddNewContact(); // Lägger till en ny kontakt
                    break;
                case 2:
                    _contactService.ShowAllContacts(); // Visar alla kontakter
                    break;
                case 3:
                    _contactService.RemoveContact(); // Tar bort en kontakt
                    break;
                case 4:
                    _contactService.DisplayContactInformation(); // Visar detaljer om en kontakt
                    break;
                case 5:
                    Environment.Exit(0); // Stänger ner allting om vi vill sluta
                    break;
                default:
                    Console.WriteLine("Oj, det där var inte ett val. Prova igen!"); 
                    break;
            }
        }
    }
}
