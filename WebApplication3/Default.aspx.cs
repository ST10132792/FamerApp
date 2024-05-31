using System;
using System.Linq;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication3.Models;
using System.Data.Entity;
using System.Web;
using System.Web.UI.WebControls;

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
                    LoadCategories();
                }
                else if (currentUser != null && userManager.IsInRole(currentUser.Id, "Farmer"))
                {
                    Response.Redirect("~/FarmerPage.aspx");
                }
            }
        }
        /// <summary>
        /// Fills the datagrid with farmers
        /// </summary>
        private void LoadFarmerUsers()
        {
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
                            u.Id,
                            u.UserName,
                            u.Email
                        })
                        .ToList();

                    FarmerUsersGridView.DataSource = farmerUsers;
                    FarmerUsersGridView.DataBind();
                }
            }
        }
        /// <summary>
        /// Fills the category picker with data
        /// </summary>
        private void LoadCategories()
        {
            using (var context = new ApplicationDbContext())
            {
                var categories = context.Products
                    .Select(p => p.Category)
                    .Distinct()
                    .ToList();

                ddlCategory.DataSource = categories;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", ""));
            }
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = new ApplicationUser() { UserName = Username.Text, Email = Email.Text };
            IdentityResult result = userManager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Farmer");
                LoadFarmerUsers();
            }
            else
            {
                // Display error message
            }
        }

        protected void FarmerUsersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            FarmerUsersGridView.EditIndex = e.NewEditIndex;
            LoadFarmerUsers();
        }
        /// <summary>
        /// Method to update a farmers details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FarmerUsersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = FarmerUsersGridView.Rows[e.RowIndex];
            string userId = FarmerUsersGridView.DataKeys[e.RowIndex].Value.ToString();
            string userName = ((TextBox)row.Cells[0].Controls[0]).Text;
            string userEmail = ((TextBox)row.Cells[1].Controls[0]).Text;

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Find(userId);
                if (user != null)
                {
                    user.UserName = userName;
                    user.Email = userEmail;
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }

            FarmerUsersGridView.EditIndex = -1;
            LoadFarmerUsers();
        }

        protected void FarmerUsersGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            FarmerUsersGridView.EditIndex = -1;
            LoadFarmerUsers();
        }
        /// <summary>
        /// Method to delete a farmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FarmerUsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userId = FarmerUsersGridView.DataKeys[e.RowIndex].Value.ToString();

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Find(userId);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }

            LoadFarmerUsers();
        }
        /// <summary>
        /// Method to filter products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFilterProducts_Click(object sender, EventArgs e)
        {
            string category = ddlCategory.SelectedValue;
            DateTime? startDate = string.IsNullOrEmpty(txtStartDate.Text) ? (DateTime?)null : DateTime.Parse(txtStartDate.Text);
            DateTime? endDate = string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

            using (var context = new ApplicationDbContext())
            {
                var productsQuery = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    productsQuery = productsQuery.Where(p => p.Category == category);
                }

                if (startDate.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
                }

                var filteredProducts = productsQuery.Select(p => new
                {
                    p.Name,
                    p.Category,
                    p.ProductionDate
                }).ToList();

                gvProducts.DataSource = filteredProducts;
                gvProducts.DataBind();
            }
        }
    }
}  