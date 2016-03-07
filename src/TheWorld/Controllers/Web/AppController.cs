using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController :Controller

    {
        private IMailService _mailService;
        private IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository)
        {
            _repository = repository;
            _mailService = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Trips()
        {
            
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not Send email");
                }
                if (_mailService.SendMail(email, email, $"Contact Page from {model.Name}({model.Email})", model.Message)) {
                    ViewBag.message = "email sent; Thanks !";
                    ModelState.Clear();
                    }
            }
            return View();
        }
    }
}