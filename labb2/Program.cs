using System;
using System.Collections.Generic;
using System.IO;

namespace labb2
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static Customer loggedInCustomer = null;
        static List<Product> ShoppingCart = new List<Product>();

        static void Main(string[] args)
        {
            // Predefined customers here
            Customer customer1 = new Customer("andreas", "skoog");
            Customer customer2 = new Customer("Robin", "kakashi");
            Customer customer3 = new Customer("bob", "bobson");

            customers.AddRange(new[] { customer1, customer2, customer3 });

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Welcome to Andreas Store");
                Console.WriteLine("1. Register New Customer");
                Console.WriteLine("2. Log into Existing Account");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    // New account
                    case "1":
                        RegisterCustomer();
                        break;

                    // Log in to an existing account
                    case "2":
                        LogIn();
                        break;

                    // Exit
                    case "3":
                        isRunning = false;
                        break;

                    // Invalid input
                    default:
                        Console.WriteLine("Invalid choice. Please type 1, 2, or 3 to continue.");
                        break;
                }
            }
        }

        static void RegisterCustomer()
        {
            // Create a new customer with a username and password

            Console.WriteLine("Type in your username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Type in your password: ");
            string password = Console.ReadLine();

            Customer newCustomer = new Customer(username, password);
            customers.Add(newCustomer);

            // Save the user data to a file
            using (StreamWriter file = new StreamWriter("users.txt", true))
            {
                file.WriteLine($"{username}, {password}");
            }

            Console.WriteLine("User registered successfully.");
        }

        static void LogIn()
        {
            // Log in to an existing account either created with the RegisterCustomer
            //or the 3 fördefinierade

            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            loggedInCustomer = customers.Find(customer => customer.Username == username && customer.VerifyPassword(password));

            if (loggedInCustomer != null)
            {
                Console.WriteLine("Login successful.");
                ShopMenu();
            }
            else
            {
                Console.WriteLine("Somethings wrong, either username or password. Please try again.");
            }
        }

        static void ShopMenu()
        {
            // Menu in the store
            bool shopping = true;
            bool hasWelcomed = false; // Initialize the flag


            while (shopping)
            {
                if (!hasWelcomed)
                {
                    Console.WriteLine($"Welcome to the store, {loggedInCustomer.Username}!");
                    hasWelcomed = true; 
                    // I did this to only displaying the welcome message once.
                }

                Console.WriteLine("1. Shop");
                Console.WriteLine("2. Shopping Cart");
                Console.WriteLine("3. Proceed to Checkout");
                Console.WriteLine("4. Log out");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    // Shop for products
                    case "1":
                        Shop();
                        break;

                    // View shopping cart
                    case "2":
                        ShowShoppingCart();
                        break;

                    // Proceed to checkout
                    case "3":
                        Checkout();
                        break;

                    // Log out
                    case "4":
                        shopping = false;
                        loggedInCustomer = null;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, 3, or 4.");
                        break;
                }
            }
        }

        static void Shop()
        {
            // Pies in the store

            Console.WriteLine("Choose a product in the store and add it to your cart:");
            Console.WriteLine("1. Pie - costs 100");
            Console.WriteLine("2. A Small Pie - costs 150");
            Console.WriteLine("3. A Medium Pie - costs 200");
            Console.WriteLine("4. A Big Pie - costs 250");
            Console.WriteLine("5. Big A** Pie - costs 500");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShoppingCart.Add(new Product("Pie", 100));
                    Console.WriteLine("Pie was added to your cart.");
                    break;

                case "2":
                    ShoppingCart.Add(new Product("A Small Pie", 150));
                    Console.WriteLine("A Small pie has been added to your cart.");
                    break;

                case "3":
                    ShoppingCart.Add(new Product("A Medium Pie", 200));
                    Console.WriteLine("A Medium pie has been added to your cart.");
                    break;

                case "4":
                    ShoppingCart.Add(new Product("A Big Pie", 250));
                    Console.WriteLine("A Big pie has been added to your cart.");
                    break;

                case "5":
                    ShoppingCart.Add(new Product("A Big A** Pie", 500));
                    Console.WriteLine("A Big A** Pie has been added to your cart.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, 3, 4, or 5.");
                    break;
            }
        }

        static void ShowShoppingCart()
        {
            Console.WriteLine("Your cart contains the following items:");
            double total = 0;

            foreach (var product in ShoppingCart)
            {
                Console.WriteLine($"{product.Name} - Cost: {product.Price}");
                total += product.Price;
            }

            Console.WriteLine("Total price in cart: " + total);
        }

        static void Checkout()
        {
            double total = 0;

            foreach (var product in ShoppingCart)
            {
                total += product.Price;
            }

            Console.WriteLine("Total cost to pay: " + total);
            ShoppingCart.Clear();
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine("You must really like pie, huh?");
        }
    }
}