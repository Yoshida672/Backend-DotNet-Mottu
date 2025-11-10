using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend_Dotnet_Mottu.Domain.Entities;

public class User
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

   [BsonElement("nome")]
   public string Name { get; set; }

   [BsonElement("email")]
   public string Email { get; set; }
   
   [BsonElement("isActive")]
   public bool IsActive { get; set; } = true;
   
   [BsonElement("createdAt")]
   public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
   
   [BsonElement("passwordHash")] 
   private string PasswordHash { get; set; } 

   [BsonElement("passwordSalt")] 
   private string PasswordSalt { get; set; }

   [BsonElement("admin")] private bool Admin { get; set; } = false;
   
   public User(string name, string email, string password, bool admin)
   {
      if (string.IsNullOrWhiteSpace(name))
         throw new ArgumentException("Nome é obrigatório.", nameof(name));

      if (string.IsNullOrWhiteSpace(email))
         throw new ArgumentException("Email é obrigatório.", nameof(email));

      if (string.IsNullOrWhiteSpace(password))
         throw new ArgumentException("Password é obrigatório.", nameof(password));
      
      Name = name;
      Email = email;
      Admin = admin;
      CreatePasswordHash(password);
   }
   
   public void Deactivate()
   {
      IsActive = false;
   }

   public bool IsAdmin => Admin = true;
   
   private void CreatePasswordHash(string password) 
   {
      using var hmac = new HMACSHA512();
      PasswordSalt = Convert.ToBase64String(hmac.Key);
      using (var hash = new HMACSHA512(hmac.Key))
      {
         PasswordHash = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
      }
   }
   
   public bool VerifyPassword(string password)
   {
      if (string.IsNullOrWhiteSpace(password))
         return false;

      using var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt));
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      var computedHashString = Convert.ToBase64String(computedHash);

      return computedHashString == PasswordHash;
   }
   
   public void UpdatePassword(string newPassword)
   {
      if (string.IsNullOrWhiteSpace(newPassword))
         throw new ArgumentException("Nova senha é obrigatória.", nameof(newPassword));

      CreatePasswordHash(newPassword);
   }

}