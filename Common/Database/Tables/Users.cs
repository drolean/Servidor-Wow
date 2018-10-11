using System;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Users
    {
        public ObjectId Id { get; set; }

        public Users(string name, string email, string username, string password)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Users()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] SessionKey { get; set; }
        public DateTime? BannetAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}