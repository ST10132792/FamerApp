using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication3.Models;

namespace WebApplication3
{
    public partial class FarmerPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }
        /// <summary>
        /// Method to add a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = userManager.FindById(User.Identity.GetUserId());

            if (currentUser != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    var product = new Products
                    {
                        FarmerId = currentUser.Id,
                        Name = txtName.Text,
                        Category = txtCategory.Text,
                        ProductionDate = DateTime.Parse(txtProductionDate.Text)
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                    LoadProducts();
                }
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            LoadProducts();
        }
        /// <summary>
        /// method to update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvProducts.Rows[e.RowIndex];
            int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value); // Parse productId as int
            string name = ((TextBox)row.Cells[0].Controls[0]).Text; 
            string category = ((TextBox)row.Cells[1].Controls[0]).Text; 
            string productionDateString = ((TextBox)row.Cells[2].Controls[0]).Text; 

            using (var context = new ApplicationDbContext())
            {
                var product = context.Products.Find(productId);
                if (product != null)
                {
                    product.Name = name;
                    product.Category = category;
                    product.ProductionDate = DateTime.Parse(productionDateString);
                    context.SaveChanges();
                }
            }

            gvProducts.EditIndex = -1;
            LoadProducts();
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            LoadProducts();
        }
        /// <summary>
        /// method to delete a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value); // Parse productId as int

            using (var context = new ApplicationDbContext())
            {
                var product = context.Products.Find(productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }

            LoadProducts();
        }

        /// <summary>
        /// Fills the datagrid with the farmer's products
        /// </summary>
        private void LoadProducts()
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = userManager.FindById(User.Identity.GetUserId());

            if (currentUser != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    var products = context.Products
                        .Where(p => p.FarmerId == currentUser.Id)
                        .ToList();

                    gvProducts.DataSource = products;
                    gvProducts.DataBind();
                }
            }
        }
    }
}
