using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Tasks.API.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<IActionResult> RequestExecutionResultAsync(this Controller controller, 
            IMediator mediator, 
            IRequest<bool> request)
        {
            bool result = await mediator.Send(request);

            if (result)
                return controller.Ok();

            return controller.BadRequest();
        }
    }
}
