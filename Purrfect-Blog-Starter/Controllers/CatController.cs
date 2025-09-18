using Newtonsoft.Json;
using Purrfect_Blog_Starter.Dtos;
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
        private static readonly HttpClient _http = new HttpClient();
        private const string FactsUrl = "https://catfact.ninja/facts?limit=20";

        [AllowAnonymous]
        public ActionResult Index() => View();

        [Authorize]
        public async Task<ActionResult> Facts()
        {
            var vm = new CatFactsViewModel();
            var facts = await FetchFactsAsync();

            vm = ViewModelMapper.ToCatFactsViewModel(facts);

            return View(vm);
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
    }
}