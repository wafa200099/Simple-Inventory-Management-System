using System.Text.RegularExpressions;

namespace InventoryManagementSystem
{
    public class Inventory
    {
        private List<Product> products = new ();

        private string GetValidatedInput(string prompt, string regexPattern = null, bool isRequired = true,
            int maxLength = 50)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (isRequired && string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input is required. Please try again.");
                    continue;
                }

                if (input.Length > maxLength)
                {
                    Console.WriteLine($"Input is too long. Maximum length is {maxLength} characters.");
                    continue;
                }

                if (regexPattern != null && !Regex.IsMatch(input, regexPattern))
                {
                    Console.WriteLine("Invalid input format. Please try again.");
                    input = null;
                    continue;
                }
            } while (input == null && isRequired);

            return input;
        }

        private decimal GetValidatedDecimal(string prompt)
        {
            decimal value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out value) || value < 0)
                {
                    Console.WriteLine("Invalid number. Please enter a valid decimal.");
                }
                else
                {
                    break;
                }
            } while (true);

            return value;
        }

        private int GetValidatedInt(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out value) || value < 0)
                {
                    Console.WriteLine("Invalid number. Please enter a valid integer.");
                }
                else
                {
                    break;
                }
            } while (true);

            return value;
        }

        public void AddProduct()
        {
            try
            {
                string name = GetValidatedInput("Enter product name: ", "^[a-zA-Z0-9 ]+$", true, 50);
                decimal price = GetValidatedDecimal("Enter product price: ");
                int quantity = GetValidatedInt("Enter product quantity: ");

                Product product = new Product(name, price, quantity);
                products.Add(product);
                Console.WriteLine($"{product.Name} has been added to the inventory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the product: {ex.Message}");
            }
        }


        public void EditProduct()
        {
            try
            {
                string name = GetValidatedInput("Enter the name of the product to edit: ");
                Product product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    string newName = GetValidatedInput("Enter new name (or press Enter to keep the same): ",
                        "^[a-zA-Z0-9 ]+$", false);
                    if (!string.IsNullOrEmpty(newName))
                        product.Name = newName;

                    string priceInput = GetValidatedInput("Enter new price (or press Enter to keep the same): ", null,
                        false);
                    if (!string.IsNullOrEmpty(priceInput))
                    {
                        if (decimal.TryParse(priceInput, out decimal newPrice))
                        {
                            product.Price = newPrice;
                            Console.WriteLine($"New price: {newPrice}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid price entered, keeping the current price.");
                        }
                    }

                    string quantityInput = GetValidatedInput("Enter new quantity (or press Enter to keep the same): ",
                        null, false);
                    if (!string.IsNullOrEmpty(quantityInput))
                    {
                        if (int.TryParse(quantityInput, out int newQuantity))
                        {
                            product.Quantity = newQuantity;
                            Console.WriteLine($"New quantity: {newQuantity}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity entered, keeping the current quantity.");
                        }
                    }

                    Console.WriteLine($"{product.Name} has been updated.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing the product: {ex.Message}");
            }
        }


        public void ViewProducts()
        {
            try
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("The inventory is empty.");
                }
                else
                {
                    Console.WriteLine("\nProducts in Inventory:");
                    foreach (var product in products)
                    {
                        Console.WriteLine(product.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing products: {ex.Message}");
            }
        }

        public void DeleteProduct()
        {
            try
            {
                string name = GetValidatedInput("Enter the name of the product to delete: ");
                Product product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    Console.Write($"Are you sure you want to delete {product.Name}? (yes/no): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation.ToLower() == "yes")
                    {
                        products.Remove(product);
                        Console.WriteLine($"{product.Name} has been deleted from the inventory.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
            }
        }

        public void SearchProduct()
        {
            try
            {
                string name = GetValidatedInput("Enter the name of the product to search: ");
                Product product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    Console.WriteLine(product);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for the product: {ex.Message}");
            }
        }
    }
}