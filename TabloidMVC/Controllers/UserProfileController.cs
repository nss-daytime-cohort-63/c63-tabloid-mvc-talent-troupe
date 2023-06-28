using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Security.Claims;

namespace TabloidMVC.Controllers
{
	public class UserProfileController : Controller
	{
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetUserProfiles();

            int userId = GetCurrentUserProfileId();

            UserProfile userProfile = _userProfileRepository.GetByUserId(userId);

            if (userProfile.UserType.Name != "Admin")
            {
                return NotFound();
            }
            return View(userProfiles);
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public IActionResult Details(int id)
        {
            var user = _userProfileRepository.GetByUserId(id);
            return View(user);
        }
    }
}

