using proyecto_url;
using proyecto_url.Entities;
using System;
using proyecto_url.Data.Interfaces;
using proyecto_url.Models;
using Microsoft.EntityFrameworkCore;

namespace proyecto_url.Data.Implementations
{
    public class UserService : IUserService
    {
        private UrlShorterContext _context;
        //Se declara una variable privada "context", readonly garantiza que una vez
        //que se le asigne un valor a context, no va a cambiar

        public UserService(UrlShorterContext context)
        {
            _context = context;
        }
        //le paso el contexto que yo hice, osea el UrlShorterContext para poder interactuar con la bd a lo largo de la clase
        public User? GetUserById(int userId)
        //busca y devuelve un usuario por su ID
        {
            return _context.Users.Find(userId);
            //Find es un método de Entity Framework que se utiliza para buscar un registro en la base de datos según cu PrimerKey
        }

        public User? ValidateUser(AuthenticationDTO authenticationDTO)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authenticationDTO.Email && p.Password == authenticationDTO.Password);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        // Utiliza el método ToList del contexto para obtener todos los registros de usuarios.
        public void Create(CreateAndUpdateUserDTO dto)
        {
            User newUser = new User()
            {
                Name = dto.Name,
                Password = dto.Password,
                UserName = dto.UserName
            };
      
            _context.Users.Add(newUser);
            _context.SaveChanges();
        
    }
        //Este método crea un nuevo usuario. Agrega el usuario al conjunto de usuarios en el contexto
        //y luego llama a SaveChanges para persistir los cambios en la base de datos.
        public void Update(CreateAndUpdateUserDTO dto, int UserId)
        {
        User userToUpdate = _context.Users.First(u => u.Id == UserId);
        userToUpdate.Name = dto.Name;
        userToUpdate.Password = dto.Password;
        _context.SaveChanges();
        }
        //Este método actualiza la información de un usuario existente. Utiliza el método Update del contexto
        //y luego SaveChanges para persistir los cambios en la base de datos.
        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
    //Este método elimina un usuario por su ID. Busca el usuario por su ID, lo elimina si existe y luego llama
    //a SaveChanges para aplicar la eliminación en la base de datos.
}