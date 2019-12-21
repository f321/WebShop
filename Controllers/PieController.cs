using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.Utility;
using PieShop.ViewModels;
using Microsoft.Extensions.Logging;

namespace PieShop.Controllers
{

    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieReviewRepository _pieReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger<PieController> _logger;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, ILogger<PieController> logger,
            IPieReviewRepository pieReviewRepository, HtmlEncoder htmlEncoder)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;
            _logger = logger;
        }

        public ViewResult List(string category)
        {

            IEnumerable<Pie> pies;
            string currentCategory = string.Empty;

            if (category.Equals("View all pies"))
            {
                pies = _pieRepository.Pies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }
            ViewBag.CurrentCategory = currentCategory;

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        [Route("[controller]/Details/{id}")]
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                _logger.LogDebug(LogEventIds.GetPieIdNotFound, new Exception("Pie not found"), "Pie with id {0} not found", id);
                throw new PieNotFoundException();
            }

            return View(new PieDetailViewModel() { Pie = pie });
        }

        [Route("[controller]/Details/{id}")]
        [HttpPost]
        public IActionResult Details(int id, string review)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                _logger.LogWarning(LogEventIds.GetPieIdNotFound, new Exception("Pie not found"), "Pie with id {0} not found", id);
                return NotFound();
            }

            string encodedReview = _htmlEncoder.Encode(review);

            _pieReviewRepository.AddPieReview(new PieReview() { Pie = pie, Review = encodedReview });

            return View(new PieDetailViewModel() { Pie = pie });
        }

    }
}
