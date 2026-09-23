namespace Core.Entities.Concrete;

using Core.Entities;
using System;
using System.Collections.Generic;

public class User : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserOperationClaim> UserOperationClaims { get; set; } = new List<UserOperationClaim>();
}