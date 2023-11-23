using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Filters
{
	public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = new BadRequestObjectResult(new { result = context.Exception.Message});
		}
	}
}
