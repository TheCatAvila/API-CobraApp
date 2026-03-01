using System.Text;
using System.Text.Json;
using API_CobraApp.Application.Common.Interfaces;

namespace API_CobraApp.Infrastructure.Services
{
    public class BrevoEmailSender : IEmailSender
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public BrevoEmailSender(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task SendPasswordResetCode(string email, string code)
        {
            var client = _httpClientFactory.CreateClient();

            // Brevo endpoint para enviar emails transaccionales
            var url = "https://api.brevo.com/v3/smtp/email";

            // Cuerpo de la petición
            var body = new
            {
                sender = new
                {
                    email = _config["Brevo:SenderEmail"],
                    name = _config["Brevo:SenderName"]
                },
                to = new[] {
                new { email, name = email }
            },
                subject = "Código de recuperación de contraseña",
                textContent = $"Tu código es: {code}"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("api-key", _config["Brevo:ApiKey"]);
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            // Enviar solicitud
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // LANZAR exception si falla
        }
    }
}
