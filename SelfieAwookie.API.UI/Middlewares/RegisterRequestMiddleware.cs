namespace SelfieAwookie.API.UI.Middlewares
{
    public class RegisterRequestMiddleware
    {
        #region Fields
        private RequestDelegate _nextRequest = null;
        private ILogger<RegisterRequestMiddleware> _logger = null;
        #endregion
        #region Constructors
        public RegisterRequestMiddleware(RequestDelegate nextRequest, ILogger<RegisterRequestMiddleware> logger)
        {
            _nextRequest = nextRequest;
            _logger = logger;
        }
        #endregion

        #region Public methods
        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug(context.Request.Path.Value);
            await _nextRequest(context);
        }
        #endregion
    }
}
