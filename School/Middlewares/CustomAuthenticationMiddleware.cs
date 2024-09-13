namespace School.Middlewares
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           
            if (context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
