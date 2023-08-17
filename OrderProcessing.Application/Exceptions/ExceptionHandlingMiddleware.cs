using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTracing;
using OpenTracing.Tag;
using OrderProcessing.Application.Responses;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace OrderProcessing.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITracer _tracer;

        public ExceptionHandlingMiddleware(RequestDelegate next, ITracer tracer)
        {
            _next = next;
            _tracer = tracer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _tracer.BuildSpan("HTTP Request").StartActive(true))
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    // Create span for exception
                    var exceptionSpan = _tracer.BuildSpan("Exception")
                        .WithTag(Tags.Error, true)
                        .WithTag("exception.type", ex.GetType().FullName)
                        .WithTag("exception.message", ex.Message)
                        .StartActive(true);

                    // Log exception details
                    exceptionSpan.Span.Log(new[]
                      {
                        new KeyValuePair<string, object>("event", "exception"),
                        new KeyValuePair<string, object>("exception.type", ex.GetType().FullName),
                        new KeyValuePair<string, object>("exception.message", ex.Message)
                    });

                    // Rethrow the exception for further handling
                    throw;
                }
                finally
                {
                    // Close the request span
                    scope.Span.Finish();
                }
            }
        }
    }
}
