using Microsoft.AspNetCore.Mvc;
using proyecto_url.Data.Interfaces;
using proyecto_url.Entities;
using proyecto_url.Models;
using proyecto_url.Data.Implementations;

using proyecto_url.Helpers;

namespace proyecto_url.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlRepository)
        {
            _urlService = urlRepository;
        }

        [HttpGet("{urlId}")]
        public ActionResult<Url> GetUrlById(int urlId)
        {
            var url = _urlService.GetUrlById(urlId);
            if (url == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la URL
            }

            return Ok(url);
        }

        [HttpGet]
        public ActionResult<List<Url>> GetAllUrls()
        {
            var urls = _urlService.GetAllUrls();
            return Ok(urls);
        }
        [HttpPost]
        [ProducesResponseType(400)]
        public IActionResult CreateShortUrl([FromBody] CreateAndUpdateUrlDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            CreateShortUrl ShortUrl = new CreateShortUrl();
            string shortUrl = ShortUrl.OriginateShortUrl();

            // creación url
            Url urlEntity = new Url
            {
                LongUrl = dto.LongUrl,
                ShortUrl = shortUrl,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId
            };

           _urlService.AddUrl(urlEntity); //añadimos a la base de datos el url
           _urlService.SaveChanges();
            
            return Created($"api/url/{shortUrl}", urlEntity);// respuesta exitosa
        }


        [HttpPut("{urlId}")]
        public IActionResult UpdateUrl(int urlId, [FromBody] CreateAndUpdateUrlDTO dto)
        {
            var existingUrl = _urlService.GetUrlById(urlId);
            if (existingUrl == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la URL
            }

            _urlService.Update(dto, urlId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }

        [HttpDelete("{urlId}")]
        public IActionResult DeleteUrl(int urlId)
        {
            var existingUrl = _urlService.GetUrlById(urlId);
            if (existingUrl == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la URL
            }

            _urlService.DeleteUrl(urlId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }

        [HttpGet("redirect/{shortUrl}")]
        public IActionResult RedirectShortenedUrl(string shortUrl)
        {
            Url originalUrl = _urlService.GetOriginalUrl(shortUrl);

            if (originalUrl != null)
            {
                // Incrementa el contador de visitas
                _urlService.IncrementVisitCount(originalUrl.Id);
                var uri = new Uri(originalUrl.LongUrl);
                return Redirect(uri.AbsoluteUri);
                // Redirige a la URL original
            }

            return NotFound(); // Manejo si no se encuentra la URL corta
        }
    }
}