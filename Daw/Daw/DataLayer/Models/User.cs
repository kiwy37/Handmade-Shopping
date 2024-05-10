namespace Daw.DataLayer.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public User()
        {
        }
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Password = password;
        }   

    }
}
