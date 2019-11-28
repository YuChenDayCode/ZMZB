using Framework.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ZMZB_Framework.Authorize
{
    /// <summary>
    /// 权限特性类
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute,IAuthorizeAttribute
    {
        public AuthorizeType Type { get; set; }
        public string Name { get; set; }
        private bool _status;
        private int _moduleId;
        private int _typeId;

        public MyAuthorizeAttribute() { }

        public MyAuthorizeAttribute(AuthorizeType type)
        {
            this.Type = type;
            this.Name = type.GetDescribeInfo();
        }

        public MyAuthorizeAttribute(AuthorizeType type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var Identity = (ClaimsIdentity)httpContext.User.Identity;
            //return Identity.IsAuthenticated && Identity.AuthenticationType == MyIdentityManage.GetIdentityManage().IdentityName;


           /* var Identity = (ClaimsIdentity)httpContext.User.Identity;
            if (this._status = (Identity.IsAuthenticated && Identity.AuthenticationType == MyIdentityManage.GetIdentityManage().IdentityName))
            {
                var a = MyIdentityManage.GetIdentityManage().GetMyIdentity(Identity);
                this._status = a.Authorize.Where(m => m.Id.ToString() == $"{this._moduleId}.{this._typeId}").Count() > 0;
            }*/
            return this._status;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("/Login");
            }
            else
            {
                filterContext.HttpContext.Response.Status = "401 Unauthorized";
            }

            base.HandleUnauthorizedRequest(filterContext);
        }

        /// <summary>
        /// 用来动态读取访问权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
         /*   var actionName = filterContext.ActionDescriptor.ActionName;
            var a = AuthorizeModuleManage.GetAuthorizeModuleManage()[filterContext.ActionDescriptor.ControllerDescriptor.ControllerName];
            this._moduleId = a?.Id > 0 ? (int)a.Id : 0;



            var t = filterContext.ActionDescriptor.GetCustomAttributes(typeof(MyAuthorizeAttribute), false).FirstOrDefault();
            var ps = t?.GetType().GetProperties();
            if (ps != null)
            {
                foreach (PropertyInfo p in ps)
                {
                    if (p.PropertyType == typeof(AuthorizeType))
                    {
                        this._typeId = Convert.ToInt32(p.GetValue(t));
                    }
                }
            }

            base.OnAuthorization(filterContext);*/
        }


    }
}
