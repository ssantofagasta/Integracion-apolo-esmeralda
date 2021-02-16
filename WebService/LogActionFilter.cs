using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace WebService
{
    public class LogActionFilter: IActionFilter
    {
        private readonly IDiagnosticContext _diagnosticContext;

        public LogActionFilter(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _diagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
            _diagnosticContext.Set("ValidationState", context.ModelState.IsValid);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
