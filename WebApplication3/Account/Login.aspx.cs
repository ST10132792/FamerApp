﻿using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebApplication3.Models;

namespace WebApplication3.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

                // Find user by email
                var user = manager.FindByEmail(Email.Text);
                if (user != null)
                {
                    var result = signinManager.PasswordSignIn(user.UserName, Password.Text, RememberMe.Checked, shouldLockout: false);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            if (manager.IsInRole(user.Id, "Farmer"))
                            {
                                Response.Redirect("~/FarmerPage.aspx");
                            }
                            else
                            {
                                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            }
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"],
                                                            RememberMe.Checked),
                                              true);
                            break;
                        case SignInStatus.Failure:
                        default:
                            FailureText.Text = "Invalid login attempt";
                            ErrorMessage.Visible = true;
                            break;
                    }
                }
                else
                {
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}
