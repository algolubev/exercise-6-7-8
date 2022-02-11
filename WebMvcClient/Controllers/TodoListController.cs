using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using TodoListClient.Services;

namespace TodoListClient.Controllers
{
    public class TodoListController : Controller
    {
        private IWeatherForecastService _todoListService;

        public TodoListController(IWeatherForecastService todoListService)
        {
            _todoListService = todoListService;
        }

        // GET: TodoList
        [AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope")]
        public async Task<ActionResult> Index()
        {
            return View(await _todoListService.GetAsync());
        }
    }
}