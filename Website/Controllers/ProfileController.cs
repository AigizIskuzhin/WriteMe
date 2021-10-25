using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService ProfileService;

        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value); 
        #endregion

        public ProfileController(IProfileService profileService)
        {
            ProfileService = profileService;
        }

        #region Profile
        /// <summary>
        /// Получение страницы профиля, с возмоджным указанием id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/profile")]
        public async Task<IActionResult> Profile(int id)
        {
            id = id == 0 ? GetConnectedUserID : id;
            var user = await ProfileService.GetUserAsync(id);
            if (user == null) return RedirectToAction("Profile");
            return View(new ProfileViewModel
            {
                User = user,
                IsOwner = id == GetConnectedUserID,
                UserPosts = ProfileService.GetUserPosts(id)
            });
        } 
        #endregion

        // TODO: implement it
        #region Upload profile avatar
        [HttpPost]
        public ActionResult UploadAvatar(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {

            }
            return View("Profile");
        } 
        #endregion
        
        // TODO: implement it
        #region Remove profile avatar
        [HttpPost]
        public ActionResult RemoveAvatar(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {

            }
            return View("Profile");
        } 
        #endregion

        #region Upload profile post
        /// <summary>
        /// Добавление поста на страницу
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPost(PostViewModel post) => PartialView("PostView", ProfileService.UploadPost(new(){
            Title=post.Title,
            Description=post.Description,
            OwnerId = GetConnectedUserID,
            CreationDateTime=DateTime.Now
            })); 
        #endregion

        #region Delete profile post
        /// <summary>
        /// Удалить указанный пост с профиля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeletePost(int id) => ProfileService.RemovePost(id, GetConnectedUserID);

        #endregion

        #region Search user posts
        /// <summary>
        /// Поиск постов пользователя с фильтром
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public ActionResult SearchUserPosts(int id, string filterText)
        {
            id = id == 0 ? GetConnectedUserID : id;
            return View("PostsView", string.IsNullOrWhiteSpace(filterText) ? ProfileService.GetUserPosts(id) : ProfileService.GetUserPostsWithFilter(id, filterText));
        } 
        #endregion
    }
}
