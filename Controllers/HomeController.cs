using System;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.Utility;
using PieShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;

        public HomeController(IPieRepository pieRepository, IStringLocalizer<HomeController> stringLocalizer, ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _pieRepository = pieRepository;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [ResponseCache(CacheProfileName = "Default")]
        public ViewResult Index()
        {
            //Serilog
            _logger.LogDebug("Loading home page");
            
           

            //caching change for IMemoryCache
            List<Pie> piesOfTheWeekCached = null;

            if (!_memoryCache.TryGetValue(CacheEntryConstants.PiesOfTheWeek, out piesOfTheWeekCached))
            {
                piesOfTheWeekCached = _pieRepository.PiesOfTheWeek.ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                cacheEntryOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

                _memoryCache.Set(CacheEntryConstants.PiesOfTheWeek, piesOfTheWeekCached, cacheEntryOptions);
            }

           
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = piesOfTheWeekCached
            };

            return View(homeViewModel);
        }

        private void FillCacheAgain(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogInformation(LogEventIds.LoadHomepage, "Cache was cleared: reason " + reason.ToString());
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //Logging
            _logger.LogInformation(LogEventIds.ChangeLanguage, "Language changed to {0}", culture);

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}