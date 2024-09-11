namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            string choice = "";

            while (choice != "6")  
            {
                Console.Clear(); 
                Console.WriteLine("Inventory Management System");
                Console.WriteLine("1. Add a Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Edit a Product");
                Console.WriteLine("4. Delete a Product");
                Console.WriteLine("5. Search for a Product");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine(); 

                switch (choice)
                {
                    case "1":
                        inventory.AddProduct();  
                        break;
                    case "2":
                        inventory.ViewProducts();  
                        break;
                    case "3":
                        inventory.EditProduct(); 
                        break;
                    case "4":
                        inventory.DeleteProduct(); 
                        break;
                    case "5":
                        inventory.SearchProduct();  
                        break;
                    case "6":
                        Console.WriteLine("Exiting the system...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();  
            }

            Console.WriteLine("Program has ended. Press any key to exit.");
            Console.ReadKey(); 
        }
    }
}