using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Whatsapp.Models;

namespace Whatsapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();

        }

        public async Task<IActionResult> WhatsApp()
        {

            using (HttpClient client = new HttpClient())
            {

                string url = "https://graph.facebook.com/v17.0/139728289222763/messages";

                string accessToken = "EAAMG5iaUtHkBO5pgvwbsOBw8dvEQJKGjE4qpZAXVxQQaP1U9DXcWnni5lSjgqIbmoGjwrer7STWi7XS2qD3cxB1CbK2EJk7puBcQBXS2jn9vXi1qWfuKpVFIjPbmu4jGoADv3Vr6rlzeDiLr8k36T6ZCoLpQjHy88hhnqnvwplz0p2u32wOUBCCLdlOn4bqyu71jZBdCxeGh9qZCa1BpZCbWI2arN2lFV1W8H2ZBkvZAy0ZD"; // Replace with your actual access token

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                string messaging_product = "whatsapp";
                string toMobile = "919029817629";
                string type = "template";
                string templatename = "hello_world";
                string languageCode = "en_us";
                //string languageCode = "en";

                string jsonContent = $"{{\"messaging_product\": \"{messaging_product}\", \"to\": \"{toMobile}\", \"type\": \"{type}\",\"template\": {{ \"name\": \"{templatename}\", \"language\": {{ \"code\": \"{languageCode}\" }} }} }}";
                //string jsonContent = "{\"messaging_product\": \"whatsapp\", \"to\": \"919892654415\", \"type\": \"template\", \"template\": { \"name\": \"greeting\", \"language\": { \"code\": \"en\" } } }";

                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Message sent successfully.");
                    Console.WriteLine("Response: " + responseContent);
                }
                else
                {
                    Console.WriteLine("Error sending message. Status code: " + response.StatusCode);
                }
            }
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}