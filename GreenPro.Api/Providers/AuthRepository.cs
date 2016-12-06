using GreenPro.Api.Models;
using GreenPro.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GreenPro.Api.Providers
{
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(RegisterBindingModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                ProfilePic = userModel.ProfilePic

            };

            var result = await _userManager.CreateAsync(user, userModel.Password);


            return result;
        }
        //private void UpdateUserName(string userid, RegisterBindingModel info)
        //{
        //    AspNetUser user;
        //    //1. Get student from DB
        //    using (var ctx = new GreenProDbEntities())
        //    {
        //        user = ctx.AspNetUsers.Where(s => s.Id == userid).FirstOrDefault();
        //    }
        //    user.FirstName = info.FirstName;
        //    user.ProfilePic = info.ProfilePic;
        //    using (var dbCtx = new GreenProDbEntities())
        //    {
        //        //3. Mark entity as modified
        //        dbCtx.Entry(user).State = System.Data.Entity.EntityState.Modified;
        //        //4. call SaveChanges
        //        dbCtx.SaveChanges();
        //    }


        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }
        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }
        public async Task<ApplicationUser> FindByNameAsync(string userEmail)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userEmail);

            return user;
        }
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}