﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.CustomAttributes
{
    public class CustomAuthorize:AuthorizeAttribute
    {
        public string LoginPage { get; set; } = "/Account/Login";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(LoginPage);
            }
            base.OnAuthorization(filterContext);
        }
    }
}