using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SimplAdsAuth.Data;
using SimpleAdsAuth.Web.Models;
using System.Diagnostics;

namespace SimpleAdsAuth.Web.Controllers
{
    public class AccoutnController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAds;Integrated Security=true;Trust Server Certificate=true;";

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            var repo = new AdRepository(_connectionString);

            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            repo.AddUser(user, hash);

            return Redirect("/account/login");


        }



        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return Redirect("/home/index");
        }

    }
}

