using System.ComponentModel.DataAnnotations;
using proyecto_url.Models;

namespace proyecto_url.Models
{
    public class CreateAndUpdateUserDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
