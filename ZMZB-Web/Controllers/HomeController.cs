using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Data.ORM;
using PCloud.Entity.Entity;
using ZMZB_Entity.Entity;
using ZMZB_Framework.Authorize;
using ZMZB_Framework.Menu;
using ZMZB_Web.Models.Home;

namespace ZMZB_Web.Controllers
{
    //[MyModule(ModuleEnum.Home, ModuleGroupEnum.BaseInfo)]
    public class HomeController : BaseController
    {
        IDbProvider<AuthorizeEntity> db_Authorize = new SqlServerProvider<AuthorizeEntity>();
        IDbProvider<AccountEntity> db_user = new SqlServerProvider<AccountEntity>();
        IDbProvider<RoleEntity> db_role = new SqlServerProvider<RoleEntity>();

        [MyAuthorize(AuthorizeType.List, "列表")]
        public ActionResult Index()
        {
            /*var a = db_role.Insert(new RoleEntity { RoleName = "角色1" });
            var c = db_role.Insert(new RoleEntity { RoleName = "角色2" }); //新增
          
            int id;
            db_role.Insert_Return_Id(new RoleEntity { RoleName = "角色3" }, out id);*/ //新增并返回id

            var a = db_role.Update(new RoleEntity { Id = 1, RoleDesc = "这是一个测试角色描述" }, t => t.Id == 1); //更新
            var d = db_role.GetList(null, 2);//top
            var aa = db_role.Delete(t => t.Id == 3);//删除

            string sql = "select * from t_role";//自定义语句
            ISqlDocker sqls = new SqlServerConstructor<RoleEntity>().CreateCustomerSql(sql);
            SqlServerContainer aaa = new SqlServerContainer();
            var list = aaa.ExecuteReader<RoleEntity>(sqls);


            // var thisuser = db_user.Get(t => t.Id == 1);
            MenuManage.LoadModules(); //加载菜单
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Authoritys = db_Authorize.GetList(t => t.RoleId == 1); //new List<AuthorizeEntity>();//从数据获取权限列表;
            model.MenuItmes = MenuManage.GetMenuItems(true, model.Authoritys); //根据权限列表生成菜单



            return View(model);
        }
        [MyAuthorize(AuthorizeType.List, "列表")]
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult Logins(string username, string pwd)
        {
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {

                return Json("<script>alert('账号密码不能为空');</script>");

            }
            else
            {
                var user = db_user.GetList(t => t.LoginId == username && t.Password == pwd);
                if (user == null)
                {
                    return Json("<script>alert('输入有误');</script>");

                }
                else
                {
                    Session["user"] = user;
                    return Json("<script>location.href=''</script>");
                    //登录成功跳转
                }
            }

            var data = new { code = 200, content = "1" };
            return Json(data);
        }



        [MyAuthorize(AuthorizeType.Look, "查看详情")]
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetAuthorizeList()
        {
            List<AuthorizeTreeViewModel> authList = new List<AuthorizeTreeViewModel>();
            authList.Add(new AuthorizeTreeViewModel() { Id = -1, Name = "权限添加", ParentId = 0 });
            {
                foreach (var a in AuthorizeModuleManage.GetAuthorizeModuleManage().Modules)
                {
                    authList.AddRange(from t in a.Authorizes
                                      select new AuthorizeTreeViewModel()
                                      {
                                          Id = t.Id,
                                          Name = t.Name,
                                          ParentId = a.Id
                                      });
                    authList.Add(new AuthorizeTreeViewModel() { Id = a.Id, Name = a.Describe, ParentId = -1 });
                }
                return Json(authList);
            }
        }
    }
}