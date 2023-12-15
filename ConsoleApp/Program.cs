using ConsoleApp.Models;

class Program
{
    static void Main(string[] args)
    {
        var contactService = new ContactService();
        var menuService = new MenuService(contactService);
        menuService.Load();
        
    }


}
