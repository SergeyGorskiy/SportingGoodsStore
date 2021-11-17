using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SportingGoodsStore.EF;
using SportingGoodsStore.Models;

namespace SportingGoodsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Products);
        }
    }
}
