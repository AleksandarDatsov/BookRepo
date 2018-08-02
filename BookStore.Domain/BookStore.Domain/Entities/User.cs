namespace BookStore.Domain.Entities
{
    public class User
    {
        public User(string username, string password, int age, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Age = age;
            this.Email = email;
        }

        public User()
        { }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public int Age { get; private set; }

        public string Email { get; private set; }

        public bool ConfirmedEmail { get;  set; }
    }
}