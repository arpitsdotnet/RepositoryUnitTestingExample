namespace Core.Models;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }

    public static User Create(Guid id, string name, string email, string mobile)
    {
        return new User { Id = id, Name = name, Email = email, Mobile = mobile };
    }
}