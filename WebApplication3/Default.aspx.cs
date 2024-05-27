using System;
using System.Linq;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication3.Models;
using System.Data.Entity;
using System.Web;
using System.Diagnostics;

namespace WebApplication3
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                if (currentUser != null && userManager.IsInRole(currentUser.Id, "Employee"))
                {
                    FarmerUsersPanel.Visible = true;
                    LoadFarmerUsers();
                }
            }
        }

        private void LoadFarmerUsers()
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = Context.GetOwinContext().Get<ApplicationRoleManager>();
            var farmerRole = roleManager.FindByName("Farmer");

            if (farmerRole != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    var farmerUsers = context.Users
                        .Where(u => u.Roles.Any(r => r.RoleId == farmerRole.Id))
                        .Select(u => new
                        {
                            u.UserName,
                            u.Email
                        })
                        .ToList();

                    // Output each farmer user's details to the debug console
                    foreach (var user in farmerUsers)
                    {
                        Debug.WriteLine("Username: " + user.UserName + ", Email: " + user.Email);
                    }

                    FarmerUsersGridView.DataSource = farmerUsers;
                    FarmerUsersGridView.DataBind();
                }
            }
        }
    }
}
