﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportingGoodsStore.EF;
using SportingGoodsStore.Models.ViewModels;

namespace SportingGoodsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;

        public int PageSize = 4;
        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = _repository.Products.OrderBy(p => p.ProductId)
                    .Skip((productPage - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            });

        }
    }
}
