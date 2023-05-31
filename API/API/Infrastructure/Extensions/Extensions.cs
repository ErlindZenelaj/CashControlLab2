using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Extensions
{
    public static class Extensions
    {
        public static string NormalizeStringToCompare(this string s)
        {
            return s.Trim().ToLower();
        }

        public static void LogDetailedInformation(this ILogger logger,
            string message, IHttpContextAccessor contextAccessor)
        {
            if (logger is null || contextAccessor is null)
            {
                return;
            }

            try
            {
                var userId = contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // var thread = Thread.CurrentThread.ManagedThreadId;
                var thread = Environment.CurrentManagedThreadId;

                logger.LogInformation("{message}. Thread: {thread}. User: {user}", message, thread, userId ?? "()");
            }
            catch
            {
                logger.LogInformation("{message}", message);
            }
        }

        public static void LogDetailedError(this ILogger logger, Exception exception,
            string message, IHttpContextAccessor contextAccessor)
        {
            if (logger is null || contextAccessor is null)
            {
                return;
            }

            try
            {
                var userId = contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // var thread = Thread.CurrentThread.ManagedThreadId;
                var thread = Environment.CurrentManagedThreadId;

                logger.LogError(exception, "{message}. Thread: {thread}. User: {user}", message, thread, userId ?? "()");
            }
            catch
            {
                logger.LogInformation("{message}", message);
            }
        }

        public static dynamic FromJson(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject(source,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Arrays
                });
        }

        public static T FromJson<T>(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(source, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }

        public static T FromJson<T>(this string source, T anonymous)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default;
            }
            return source.FromJson<T>();
        }

        public static string ToJson<T>(this T source)
        {
            return source?.ToJson(new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }

        public static string ToJson<T>(this T source, JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(source ?? default, options);
        }

        
    }
}