﻿using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Stakeholders.Core.Domain;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public string Email {  get; private set; }
    public bool IsBlocked {  get; set; }

    public User(string username, string password, UserRole role, bool isActive, string email, bool isBlocked)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Validate();
        Email = email;
        IsBlocked = isBlocked;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}