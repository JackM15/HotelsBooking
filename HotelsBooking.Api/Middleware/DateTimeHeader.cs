using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace HotelsBooking.Api.Middleware
{
    public class DateTimeHeader
    {
        private readonly RequestDelegate _next;

        public DateTimeHeader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Executed on request
            httpContext.Request.Headers.Add("my-middleware-header", DateTime.Now.ToString());
            await Task.FromResult(_next(httpContext));

            //Executed on Response
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DateTimeHeaderExtensions
    {
        public static IApplicationBuilder UseDateTimeHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DateTimeHeader>();
        }
    }
}
