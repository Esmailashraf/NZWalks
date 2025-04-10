using System.Net;

namespace NZwalks.api.Middlewares
{
    public class ExceptionErrorHandler
    {
        private readonly ILogger<ExceptionErrorHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionErrorHandler(ILogger<ExceptionErrorHandler> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                next(httpContext);

            }catch(Exception ex)
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex,$"{errorId} : {ex.Message}" );
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    errorId=errorId,
                    errorMessage="something is wrong"
                };
                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
