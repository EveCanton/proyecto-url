using Microsoft.AspNetCore.Mvc;
using proyecto_url.Data.Interfaces;
using proyecto_url.Entities;
using proyecto_url.Models;
using proyecto_url.Data.Implementations;

namespace proyecto_url.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
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
        public ActionResult<Url> CreateUrl([FromBody] CreateAndUpdateUrlDTO dto)
        {
            var newUrl = _urlService.Create(dto);
            return Ok(newUrl); // Devuelve el DTO creado o podrías devolver la URL creada
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
            var originalUrl = _urlService.GetOriginalUrl(shortUrl);
            if (originalUrl != null)
            {
                // Incrementa el contador de visitas
                _urlService.IncrementVisitCount(originalUrl.Id);

                // Redirige a la URL original
                return Redirect(originalUrl.LongUrl);
            }

            return NotFound(); // Manejo si no se encuentra la URL corta
        }
    }
}