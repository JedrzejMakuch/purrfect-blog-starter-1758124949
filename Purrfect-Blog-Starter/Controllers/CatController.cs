using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.Services;
using Purrfect_Blog_Starter.Utilities;
using Purrfect_Blog_Starter.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Purrfect_Blog_Starter.Controllers
{
    public class CatController : Controller
    {
        private readonly IFactsService _factsService;
        private static readonly HttpClient _http = new HttpClient();
        private const string FactsUrl = "https://catfact.ninja/facts?limit=20";
        private const string FactUrl = "https://catfact.ninja/fact?limit=50";

        public CatController(IFactsService factsService)
        {
            _factsService = factsService;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var vm = new CatFactViewModel();
            var fact = await FetchFactAsync();
            vm = ViewModelMapper.ToFactViewModel(fact);

            return View(vm);
        }

        [Authorize]
        public async Task<ActionResult> Facts()
        {
            var vm = new CatFactsViewModel();
            var userId = User.Identity.GetUserId();
            var facts = await FetchFactsAsync();
            var savedFacts = _factsService.GetSavedFactDtos(userId);

            vm = ViewModelMapper.ToCatFactsViewModel(facts, savedFacts);

            return View(vm);
        }

        [Authorize]
        public ActionResult Favourites()
        {
            var userId = User.Identity.GetUserId();
            var facts = _factsService.GetSavedFactDtos(userId);
            var vm = ViewModelMapper.ToSavedFactsViewModel(facts);

            return View(vm);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Favourites");
            }

            _factsService.Delete(userId, id);

            return RedirectToAction("Favourites");
        }

        [Authorize]
        public ActionResult Save(string description)
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Facts");
            }

            _factsService.Save(userId, description);

            return RedirectToAction("Facts");
        }

        private async Task<CatFactsDto> FetchFactsAsync()
        {
            var resp = await _http.GetAsync(FactsUrl);

            if (!resp.IsSuccessStatusCode)
            {
                return new CatFactsDto { Data = new List<CatFactDto>() };
            }

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CatFactsDto>(json);
        }
        private async Task<CatFactDto> FetchFactAsync()
        {
            var resp = await _http.GetAsync(FactUrl);
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CatFactDto>(json);
        }
    }
}