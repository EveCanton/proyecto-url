using System.Text;

namespace proyecto_url.Helpers
{
    public class CreateShortUrl
    {
        private readonly string AllowedCharacters = "123456789abcdefghijklmnopqrstuvwxyzACDEFGHIJKLMNÑOPQRSTUVWXYZ";
        public string OriginateShortUrl()
        {
            Random random = new Random();
            StringBuilder ShortUrl = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                int indice = random.Next(AllowedCharacters.Length);
                ShortUrl.Append(AllowedCharacters[indice]);
            }

            return ShortUrl.ToString();
        }
    }
}
