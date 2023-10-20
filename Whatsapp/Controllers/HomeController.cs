using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
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


            string accessToken = "EAAMG5iaUtHkBO8vm2YvWEQUEMu1KgxsLO1VSNd58XCsD2wliVdifnLlNMuVSGbfEyCsbDBzC81MVFTMjidc1z57DscX0fQ5gxcKJaHGSooF4V6TRCD3Qf3BGf0A5ERH3YeMneH4qg4sMZBCaPNCSuD4wDvZBhG9DxIxc6R5esMatuvtCpLMOuKEdElRtr7O7qNBOBClyUtfHamLjZC7TONmxRLz4Uc34yG5zdnFWOEZD"; // Replace with your actual access token

            string recipient = "919029817629"; // Replace with recipient's phone number
            string message = "Hello, this is a test message.";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/v18.0/139728289222763");

                HttpResponseMessage response = await client.PostAsync(
                    $"/messages?access_token={accessToken}",
                    new StringContent(
                        $"{{\"recipient\": {{\"phone_number\": \"{recipient}\"}}, \"message\": {{\"attachment\": {{\"type\": \"image\", \"payload\": {{\"url\": \"https://fastly.picsum.photos/id/866/200/300.jpg?hmac=rcadCENKh4rD6MAp6V_ma-AyWv641M4iiOpe1RyFHeI\"}}}}}}}}",
                        System.Text.Encoding.UTF8,
                        "application/json"
                    )
                );

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Message sent successfully: {responseBody}");
                }
                else
                {
                    Console.WriteLine($"Error sending message: {response.StatusCode}");
                }
            }




            //using (HttpClient client = new HttpClient())
            //{

            //    string url = "https://graph.facebook.com/v17.0/139728289222763/messages";

            //    string accessToken = "EAAMG5iaUtHkBO8vm2YvWEQUEMu1KgxsLO1VSNd58XCsD2wliVdifnLlNMuVSGbfEyCsbDBzC81MVFTMjidc1z57DscX0fQ5gxcKJaHGSooF4V6TRCD3Qf3BGf0A5ERH3YeMneH4qg4sMZBCaPNCSuD4wDvZBhG9DxIxc6R5esMatuvtCpLMOuKEdElRtr7O7qNBOBClyUtfHamLjZC7TONmxRLz4Uc34yG5zdnFWOEZD"; // Replace with your actual access token

            //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            //    string messaging_product = "whatsapp";
            //    string toMobile = "919029817629";
            //    string type = "template";
            //    string templatename = "hello_world";
            //    string languageCode = "en_us";
            //    //string languageCode = "en";

            //    string jsonContent = $"{{\"messaging_product\": \"{messaging_product}\", \"to\": \"{toMobile}\", \"type\": \"{type}\",\"template\": {{ \"name\": \"{templatename}\", \"language\": {{ \"code\": \"{languageCode}\" }} }} }}";
            //    //string jsonContent = "{\"messaging_product\": \"whatsapp\", \"to\": \"919892654415\", \"type\": \"template\", \"template\": { \"name\": \"greeting\", \"language\": { \"code\": \"en\" } } }";

            //    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //    HttpResponseMessage response = await client.PostAsync(url, content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseContent = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine("Message sent successfully.");
            //        Console.WriteLine("Response: " + responseContent);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error sending message. Status code: " + response.StatusCode);
            //    }
            //}


            //string phone = "919029817629";  // Replace with your WhatsApp number
            //string message = "Hello from WhatsApp!";  // Replace with your message
            //string attachmentURL = "https://www.africau.edu/images/default/sample.pdf";  // Replace with your attachment URL
            //string accessToken = "EAAMG5iaUtHkBO8vm2YvWEQUEMu1KgxsLO1VSNd58XCsD2wliVdifnLlNMuVSGbfEyCsbDBzC81MVFTMjidc1z57DscX0fQ5gxcKJaHGSooF4V6TRCD3Qf3BGf0A5ERH3YeMneH4qg4sMZBCaPNCSuD4wDvZBhG9DxIxc6R5esMatuvtCpLMOuKEdElRtr7O7qNBOBClyUtfHamLjZC7TONmxRLz4Uc34yG5zdnFWOEZD"; // Replace with your actual access token

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var messageData = new
            //{
            //    phone_number = phone,
            //    message = new
            //    {
            //        attachment = new
            //        {
            //            type = "file",
            //            payload = new
            //            {
            //                url = attachmentURL,
            //                is_reusable = true
            //            }
            //        }
            //    }
            //};

            //var jsonContent = JsonConvert.SerializeObject(messageData);
            //var httpContent = new StringContent(jsonContent);
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //string url = $"https://graph.facebook.com/v17.0/139728289222763/messages?access_token={accessToken}";

            //HttpResponseMessage response = await client.PostAsync(url, httpContent);

            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("Message sent successfully!");
            //}
            //else
            //{
            //    Console.WriteLine($"Error: {response.StatusCode}");
            //}

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