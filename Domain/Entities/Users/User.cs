﻿using Domain.Entities.Courses;
using Domain.Entities.Devices;
using Domain.Entities.Sessions;
using Domain.Entities.Tests;
using System.Linq.Expressions;

namespace Domain.Entities.Users;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public List<CourseItem> CourseItems { get; set; } = new();
    public List<CourseXUser> Courses { get; set; } = new();
    public List<Session> Sessions { get; set; } = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public User(int id, string firstName, string lastName, string username, string password, UserRole role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Password = password;
        Role = role;
    }
}