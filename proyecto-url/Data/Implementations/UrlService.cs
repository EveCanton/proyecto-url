
using global::proyecto_url.Entities;
using proyecto_url;
using System;
using System.Collections.Generic;
using proyecto_url.Data.Interfaces;
using proyecto_url.Models;

namespace proyecto_url.Data.Implementations
{

    public class UrlService : IUrlService
    {
        private readonly UrlShorterContext _context;

        public UrlService(UrlShorterContext context)
        {
            _context = context;
        }

        public Url Create(CreateAndUpdateUrlDTO dto)
        {
            // Acortar la URL y almacenarla en la base de datos
            Url newUrl = new Url()
            {
                LongUrl = dto.LongUrl,
                ShortUrl = dto.ShortUrl,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId

            };
            _context.Urls.Add(newUrl);
            _context.SaveChanges();

            return newUrl;
        }

        public Url GetOriginalUrl(string shortUrl)
        {
            return _context.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);
        }

        public Url GetUrlById(int urlId)
        {
            return _context.Urls.Find(urlId);
        }

        public List<Url> GetAllUrls()
        {
            return _context.Urls.ToList();
        }

        public void Update(CreateAndUpdateUrlDTO dto, int UrlId)
        {

            Url userToUpdate = _context.Urls.First(u => u.Id == UrlId);
            userToUpdate.LongUrl = dto.LongUrl;
            userToUpdate.ShortUrl = dto.ShortUrl;
            _context.SaveChanges();
        }


        public void DeleteUrl(int urlId)
        {
            var url = _context.Urls.Find(urlId);
            if (url != null)
            {
                _context.Urls.Remove(url);
                _context.SaveChanges();
            }
        }

        public void AddUrl(Url url)
        {
            _context.Urls.Add(url);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void IncrementVisitCount(int urlId)
        {
            var url = _context.Urls.Find(urlId);
            if (url != null)
            {
                url.VisitsCount++;
                _context.SaveChanges();
            }


        }
    }
}