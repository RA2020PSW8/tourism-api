using Explorer.BuildingBlocks.Core.Domain;
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
    public ICollection<TourIssueComment>? IssueComments { get; private set; }
    public ICollection<TourIssue>? Issues { get; private set; }

    public User()
    {
        IssueComments = new List<TourIssueComment>();
        Issues = new List<TourIssue>();
    }

    public User(string username, string password, UserRole role, bool isActive, string email, bool isBlocked, ICollection<TourIssueComment>? issueComments = null, ICollection<TourIssue> issues = null)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Validate();
        Email = email;
        IsBlocked = isBlocked;
        IssueComments = issueComments;
        Issues = issues;
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