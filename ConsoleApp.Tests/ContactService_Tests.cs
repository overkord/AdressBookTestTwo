using Xunit;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class ContactService_Tests
    {
        [Fact]
        public void AddContact_Should_AddToContactsList()
        {
            // Arrange
            var contactService = new ContactService();
            var contact = new Contact
            {
                FirstName = "abbe",
                LastName = "Dj",
                Email = "snurre@ffffddde.com"
            };

            // Act
            contactService.AddContact(contact);

            // Assert
            var contacts = contactService.GetAllContacts();
            Assert.Contains(contact, contacts);
        }
    }
}
