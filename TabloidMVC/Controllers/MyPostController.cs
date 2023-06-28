using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
namespace TabloidMVC.Controllers
{
    public class MyPostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MyPostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: MyPostController
        public ActionResult Index()
        {
            int currentUser = GetCurrentUserProfileId();
            List<Post> posts = _postRepository.GetAllPostsByCurrentUser(currentUser);

            return View(posts);
        }

        // GET: MyPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
