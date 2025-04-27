using Microsoft.AspNetCore.Mvc;
using SimplAdsAuth.Data;
using SimpleAdsAuth.Web.Models;
using System.Diagnostics;

namespace SimpleAdsAuth.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAds;Integrated Security=true;Trust Server Certificate=true;";
        
        public IActionResult Index()
        {
            var vm = new HomeViewModel();
            var repo = new AdRepository(_connectionString);
            vm.Ads = repo.GetAllAds();

            return View(vm);
        }

        
    }
}
