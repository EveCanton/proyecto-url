using proyecto_url.Entities;
using proyecto_url.Models;
using System;
namespace proyecto_url.Data.Interfaces
{
    public interface IUrlService
    {
            Url Create(CreateAndUpdateUrlDTO dto);
            //método para crear una nueva URL

            void AddUrl(Url url);
            void SaveChanges();
            Url GetUrlById(int urlId);
            //obtener una url específica según su ID
            Url GetOriginalUrl (string shortUrl);
            List<Url> GetAllUrls();
            //lista de todas las urls del sistema
            void Update(CreateAndUpdateUrlDTO dto, int UrlId);
            //actualizar la información de una url existente
            void DeleteUrl(int urlId);
            //eliminar una url existente por medio del id
            void IncrementVisitCount(int urlId);
            //incrementar los contadores de visita

        }

}
//los que comienzan con Url es porque van a devolver un url, los que empiezan con void quiere decir
//que hace una acción pero no devuelve nada, void es vacío.
