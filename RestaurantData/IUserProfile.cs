using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IUserProfile
    {
        List<UserProfile> GetUserProfiles(int? restaurantId);
        UserProfile GetUserProfile(string userId);
        UserProfile AddUserProfile(UserProfile profile);
        bool DeleteUserProfile(UserProfile profile);
        UserProfile EditUserProfile(UserProfile profile);
        bool IsUserProfileExist(UserProfile profile);
    }
}
