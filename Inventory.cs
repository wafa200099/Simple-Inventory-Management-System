using System.Text.RegularExpressions;

namespace InventoryManagementSystem
{
    public class Inventory
    {
        private List<Product> products = new (); 

        private T GetValidatedInput<T>(string prompt, string regexPattern = null, bool isRequired = true, int maxLength = 50) where T : IComparable
        {
            string input;
            T value = default;

            // Loop until valid input is obtained
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                // Check if input is required and non-empty
                if (isRequired && string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input is required. Please try again.");
                    continue;
                }

                // Check if input exceeds the maximum length
                if (!string.IsNullOrEmpty(input) && input.Length > maxLength)
                {
                    Console.WriteLine($"Input is too long. Maximum length is {maxLength} characters.");
                    continue;
                }

                // If regex pattern is provided, validate the input
                if (!string.IsNullOrEmpty(regexPattern) && !Regex.IsMatch(input, regexPattern))
                {
                    Console.WriteLine($"Input does not match the required format. Expected pattern: {regexPattern}");
                    input = null;
                    continue;
                }

                // If T is a string, no further validation is needed
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)input; // Return input as string
                }

                // If T is a numeric type, attempt to parse the input
                if (!string.IsNullOrEmpty(input))
                {
                    switch (value)
                    {
                        case int _:
                            if (int.TryParse(input, out int intValue) && intValue >= 0)
                                value = (T)(object)intValue;
                            else
                                Console.WriteLine("Invalid integer value. Please enter a valid number.");
                            break;

                        case decimal _:
                            if (decimal.TryParse(input, out decimal decimalValue) && decimalValue >= 0)
                                value = (T)(object)decimalValue;
                            else
                                Console.WriteLine("Invalid decimal value. Please enter a valid number.");
                            break;

                        case double _:
                            if (double.TryParse(input, out double doubleValue) && doubleValue >= 0)
                                value = (T)(object)doubleValue;
                            else
                                Console.WriteLine("Invalid double value. Please enter a valid number.");
                            break;
                        
                        default:
                            Console.WriteLine($"Unsupported type: {typeof(T).Name}");
                            input = null;
                            break;
                    }
                }
            } while (input == null || (typeof(T) != typeof(string) && EqualityComparer<T>.Default.Equals(value, default(T))));

            return value;
        }

        public void AddProduct()
        {
            try
            {
                string name = GetValidatedInput<string>("Enter product name: ", "^[a-zA-Z0-9 ]+$", true, 50);
                decimal price = GetValidatedInput<decimal>("Enter product price: ");
                int quantity = GetValidatedInput<int>("Enter product quantity: ");

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
                string name = GetValidatedInput<string>("Enter the name of the product to edit: ");
                Product product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    string newName = GetValidatedInput<string>("Enter new name (or press Enter to keep the same): ",
                        "^[a-zA-Z0-9 ]+$", false);
                    if (!string.IsNullOrEmpty(newName))
                        product.Name = newName;

                    string priceInput = GetValidatedInput<string>("Enter new price (or press Enter to keep the same): ", null, false);
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

                    string quantityInput = GetValidatedInput<string>("Enter new quantity (or press Enter to keep the same): ", null, false);
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
                string name = GetValidatedInput<string>("Enter the name of the product to delete: ");
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
                string name = GetValidatedInput<string>("Enter the name of the product to search: ");
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
        }    }
}
