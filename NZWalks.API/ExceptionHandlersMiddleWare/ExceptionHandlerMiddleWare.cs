using System.Net;

namespace NZWalks.API.ExceptionHandlersMiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;
        public readonly RequestDelegate requestDelegate;

        public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> logger,RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception e) 
            {
                Guid errorID = Guid.NewGuid();
                this.logger.LogError(e,$"{errorID} : {e.Message}");

                httpContext.Response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    Id = errorID,
                    message = "Something went wrong. We are looking at it"
                };
                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
