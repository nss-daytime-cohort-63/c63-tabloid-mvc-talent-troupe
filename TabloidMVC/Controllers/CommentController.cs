using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int postId)
        {
            CommentCreateViewModel viewModel = new CommentCreateViewModel();
            viewModel.PostId = postId;
            return View(viewModel);
        }

        // POST: CommentController/Create
        [HttpPost]
        public ActionResult Create(CommentCreateViewModel viewModel)
        {
            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a new Comment object
            Comment newComment = new Comment();
            newComment.Subject = viewModel.Subject;
            newComment.Content = viewModel.Content;
            newComment.PostId = viewModel.PostId;
            newComment.CreateDateTime = DateTime.Now;
            newComment.UserProfileId = int.Parse(userId);

            // Add the new comment to the database
            _commentRepository.Add(newComment);

            // Redirect back to the post details page
            return RedirectToAction("Details", "Post", new { id = viewModel.PostId });
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
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

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
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
    }
}
