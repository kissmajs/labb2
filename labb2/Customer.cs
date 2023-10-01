namespace labb2
{
    internal class Customer
    {
        public string Username { get; }
        private string PasswordHash { get; }

        public Customer(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password); 
            // Store the hashed password
        }

        public bool VerifyPassword(string password)
        {
            // Verify if the provided password matches the stored hashed password
            return HashPassword(password) == PasswordHash;
        }

        private string HashPassword(string password)
        {
            return password; 
        }


        //jag implementerade ToString men jag använder mig alldrig av den.
        //Hoppas det går bra. Annars är det bara att säga till så klämmer jag in det i program.cs
        public string ToString(List<Product> ShoppingCart)
        {
            return $"Name: {Username}\nPassword: ********\nShopping Cart:\n{string.Join("\n", ShoppingCart)}";
        }
    }
}
