using BookBarn.Models;

using Microsoft.AspNetCore.Mvc;

namespace BookBarn.Controllers
{
    public class SuggestionsController : Controller
    {
        // GET: /Suggestions/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookSuggestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Success logic will be expanded later in the course (CRUD + database persistence).
            // For now, we simply redirect to a success page (PRG).
            return RedirectToAction("ThankYou");
        }
        // GET: /Suggestions/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
