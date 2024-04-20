﻿using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Service.DTOs.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public long RoledId { get; set; }
    public UserRole UserRole { get; set; }
}
