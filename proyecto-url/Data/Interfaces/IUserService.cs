using proyecto_url.Entities;
using System;
using proyecto_url.Models;

namespace proyecto_url.Data.Interfaces;

public interface IUserService
{
    User GetUserById(int userId);
    User GetUserByUsername(string username);
    List<User> GetAllUsers();
    User Create(CreateAndUpdateUserDTO dto);
    void Update(CreateAndUpdateUserDTO dto, int UserId);
    User? ValidateUser(AuthenticationDTO dto);
    void DeleteUser(int userId);
}