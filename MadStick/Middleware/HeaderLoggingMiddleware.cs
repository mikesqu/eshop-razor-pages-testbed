namespace MadStickWebAppTester.Middleware
{
    internal class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IConfiguration _configuration;

        public HeaderLoggingMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
        {
            _requestDelegate = requestDelegate;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ILogger<HeaderLoggingMiddleware> logger)
        {
            String pathStartsWith = _configuration.GetSection("HeaderLogger")["path"];

            if (context.Request.Path.StartsWithSegments(pathStartsWith) == false)
            {
                await _requestDelegate(context);
            }
            else
            {
                // incoming request \/
                
                logger.LogInformation("Request headers: ---");
                foreach (var header in context.Request.Headers)
                {
                    logger.LogInformation("header.Key: {headerKey}; header.Value: {headerValue}", header.Key, header.Value);
                }
                logger.LogInformation("---------------------");
                
                await _requestDelegate(context);

                // outgoing request \/
                logger.LogInformation("Response headers: ---");
                foreach (var header in context.Response.Headers)
                {
                    logger.LogInformation("header.Key: {headerKey}; header.Value: {headerValue}", header.Key, header.Value);
                }
                logger.LogInformation("---------------------");
            }
        }
    }
}