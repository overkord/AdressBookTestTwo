namespace ConsoleApp.Models;

public interface IContact
{
    string Address { get; set; }
    string City { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string PhoneNumber { get; set; }
    string PostalCode { get; set; }
}

public class Contact : IContact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

}
