using ConsoleApp.Models;
using Newtonsoft.Json;

public class ContactService
{
    private List<Contact> contacts = new List<Contact>();
    private readonly string filePath = (@"C:\Projects\contacts.json"); // Filväg för JSON-filen

    public ContactService()
    {
        LoadContacts(); // Läs in kontakter från filen vid skapandet av tjänsten
    }

    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
        SaveContacts();
    }

    public void RemoveContact(string email)
    {
        var contactToRemove = contacts.FirstOrDefault(c => c.Email == email);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            SaveContacts();
        }
    }

    public List<Contact> GetAllContacts()
    {
        return contacts;
    }

    public Contact GetContactByEmail(string email)
    {
        return contacts.FirstOrDefault(c => c.Email == email)!;
    }

    private void LoadContacts()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json)!;
        }
    }

    private void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(contacts);
        File.WriteAllText(filePath, json);
    }
}
