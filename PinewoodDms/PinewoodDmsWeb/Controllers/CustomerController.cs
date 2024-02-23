using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PinewoodDmsWeb.Models;

namespace PinewoodDmsWeb.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7073/api");
        private readonly HttpClient _httpClient;

        public CustomerController()
        {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = baseAddress;  
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel>? customers = new List<CustomerViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel customer)
        {
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Customer", customer).Result;
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CustomerViewModel? customer = new CustomerViewModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/"+ id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customer)
        {
            HttpResponseMessage response = _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + "/Customer", customer).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CustomerViewModel? customer = new CustomerViewModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Customer/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
