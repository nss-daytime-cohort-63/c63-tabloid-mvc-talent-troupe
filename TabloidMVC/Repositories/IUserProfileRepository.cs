using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        UserProfile GetByUserId(int id);
        List<UserProfile> GetUserProfiles();
        void DeactivateProfile(UserProfile profile);
        void ActivateProfile(UserProfile profile);
    }
}