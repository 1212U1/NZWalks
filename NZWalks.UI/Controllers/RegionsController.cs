using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionsDTO> regionsDto = new List<RegionsDTO>();
            //get all regions
            try
            {
                HttpClient httpClient = httpClientFactory.CreateClient();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5083/api/regions/all");
                httpResponseMessage.EnsureSuccessStatusCode();
                regionsDto.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDTO>>());
            }
            catch (Exception ex) { }
            return View(regionsDto);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            try
            {
                HttpClient httpClient = httpClientFactory.CreateClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:5083/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                RegionsDTO regionsDTO = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDTO>();
                if (regionsDTO is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
                return View();
            }
            catch (Exception ex) { return BadRequest(); }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            RegionsDTO regionsDTO = await httpClient.GetFromJsonAsync<RegionsDTO>($"http://localhost:5083/api/regions/{id.ToString()}");
            if (regionsDTO is not null)
            {
                return View(regionsDTO);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegionsDTO request)
        {
            try
            {
                String id = request.Id.ToString();
                HttpClient httpClient = httpClientFactory.CreateClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"http://localhost:5083/api/regions?regionID={id}"),
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                RegionsDTO regionsDTO = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDTO>();
                if (regionsDTO is not null)
                {
                    return RedirectToAction("Edit", "Regions");
                }
                return View();
            }
            catch (Exception ex) { return BadRequest(); }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RegionsDTO request)
        {
            String id = request.Id.ToString();
            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync($"http://localhost:5083/api/regions/{id.ToString()}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Regions");
            }
            return BadRequest();
        }
    }
}
