using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text;
using TaskManagement.Models;
using System.Net;

//using System.Net.Http;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<SelectListItem> _statusList;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _statusList = PopulateStatusList();
            _httpClientFactory = httpClientFactory;
            _configuration = configuration; 
        }

        public IActionResult Index()
        {
            TaskViewModel viewModel = new TaskViewModel();
            viewModel.StatusList = _statusList;
            return View(viewModel);
        }

        public async Task<IActionResult> SubmitTask(TaskViewModel tvm)
        {
            tvm.StatusList = _statusList;
            tvm.TaskId = tvm.TaskId != null ? tvm.TaskId : 0;
            try
            {
                if (ModelState.IsValid)
                {

                  
                    string json = JsonSerializer.Serialize(tvm);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var httpClient = _httpClientFactory.CreateClient();
                    httpClient.BaseAddress = new Uri(_configuration["AppSettings:BaseUrl"]);
                    HttpResponseMessage response;
                    if (tvm.TaskId == 0)
                    {
                        response = await httpClient.PostAsync("api/Task", httpContent);
                    }
                    else
                    {
                        response = await httpClient.PutAsync("api/Task/" + tvm.TaskId, httpContent);
                    }
                    DisplayMessage(response, tvm);
                }
            }
            catch (Exception ex)
            {

            }
            return View("Index",tvm);
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

        private void DisplayMessage(HttpResponseMessage response, TaskViewModel tvm)
        {
            if (response.IsSuccessStatusCode)
            {
                ViewBag.SucessMessage = tvm.TaskId == 0 ? "Task created successfully" : "Task updated successfully";
                ModelState.Clear();
                ClearViewModel(tvm);
                
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {

                ViewBag.FailureMessage = "Task name already exist.";
            }
            else
            {

                ViewBag.FailureMessage = tvm.TaskId == 0 ? "An error occurred while creating task." : "An error occurred while updating task"; 
            }
            
        }

        private void ClearViewModel(TaskViewModel tvm)
        {
            tvm.Name = string.Empty;
            tvm.Priority = 0;
            tvm.Status = string.Empty;
            tvm.TaskId = 0;
        }

        private List<SelectListItem> PopulateStatusList()
        {
            var StatusListItem = new List<SelectListItem>() {
            new SelectListItem{Value="not started",Text="not started" },
            new SelectListItem{Value="in progress",Text="in progress" },
            new SelectListItem{Value="completed",Text="completed" },
            };
            return StatusListItem;
        }
    }
}