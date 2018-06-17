using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ActionFilters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder errInfo = new StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errInfo.AppendFormat("{0}\\n", err.ErrorMessage);
                    }
                }
                context.Result = new JsonResult(new { Result = false, Msg = errInfo.ToString() });
            }
        }
    }
}
