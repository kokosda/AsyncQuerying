using AsyncQuerying.Web.Models;
using AsyncQuerying.Web.Models.Builders.Abstract;
using AsyncQuerying.Infrastructure.Extensions;
using System.Web.Mvc;
using AsyncQuerying.Web.Models.Users;

namespace AsyncQuerying.Web.Controllers
{
    public class UsersListController : Controller
    {
        public UsersListController(IUsersListModelBuilder usersListModelBuilder)
        {
            this.usersListModelBuilder = usersListModelBuilder;
        }

        [HttpGet]
        public ViewResult Index()
        {
            UserFilterModel filterModel = null;
            
            if (Request.Cookies["DelayedQueryState.Token"] != null)
            {
                filterModel = new UserFilterModel
                                    {
                                        DelayedQueryState = new DelayedQueryStateModel
                                                                {
                                                                    Token = System.Net.WebUtility.UrlDecode(Request.Cookies["DelayedQueryState.Token"].Value)
                                                                }
                                    };
            }

            var model = usersListModelBuilder.Build(filterModel);

            var result = View(model);

            return result;
        }

        [HttpPost]
        public ViewResult Index(UserFilterModel filterModel)
        {
            var model = usersListModelBuilder.Build(filterModel, true);

            var result = View("WaitForRequest", model);

            return result;
        }

        [HttpGet]
        public JsonResult List(UserFilterModel filterModel)
        {
            var model = usersListModelBuilder.Build(filterModel, true);
            var result = Json(model, JsonRequestBehavior.AllowGet);

            return result;
        }

        #region private

        private readonly IUsersListModelBuilder usersListModelBuilder;

        #endregion
    }
}