using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Auth.API.Core.Entities
{
    public class User
    {
        public User(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
        }

        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }

        public string Username { get; private set; }

        public string Email { get; private set; } 

        
        public string Password { get; private set; }
    }
}
