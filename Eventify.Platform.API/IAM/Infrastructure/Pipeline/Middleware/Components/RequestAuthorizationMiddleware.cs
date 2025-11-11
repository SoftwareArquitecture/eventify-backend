El error `System.NullReferenceException` que estás viendo es causado por la línea que intenta acceder al `.Metadata` del *Endpoint* sin verificar si el *Endpoint* es nulo. Esto es común cuando se accede a rutas que no son endpoints MVC (como la ruta raíz `/` o archivos estáticos de Swagger).

Aquí tienes el código completo de `RequestAuthorizationMiddleware.cs` con la corrección aplicada, siguiendo la lógica que permite que las rutas no definidas (como `/swagger` o `/`) se salten el proceso de autenticación.

El cambio clave es en las **líneas 30-31** para primero capturar el endpoint y luego verificar que no sea nulo antes de intentar acceder a sus propiedades.

```csharp
using System.Security.Authentication;
using Eventify.Platform.API.IAM.Application.Internal.OutboundServices;
using Eventify.Platform.API.IAM.Domain.Model.Queries;
using Eventify.Platform.API.IAM.Domain.Services;
using Eventify.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Http.Extensions; // Necesario para GetEndpoint()

namespace Eventify.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next, ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        _logger.LogInformation("Entering InvokeAsync");
        
        var endpoint = context.Request.HttpContext.GetEndpoint();
        
        // skip authorization if endpoint is null (e.g., static files, root path) 
        // OR if it is explicitly decorated with [AllowAnonymous] attribute.
        var allowAnonymous = endpoint != null && endpoint.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        
        if (endpoint == null || allowAnonymous)
        {
            _logger.LogInformation("Skipping authorization");
            // [AllowAnonymous] attribute is set, so skip authorization
            await next(context);
            return;
        }

        _logger.LogInformation("Entering authorization");
        // get token from request header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


        // if token is null then throw exception
        if (token is null) throw new AuthenticationException("Null or invalid token");

        // validate token
        var userId = await tokenService.ValidateToken(token);

        // if token is invalid then throw exception
        if (userId is null) throw new AuthenticationException("Invalid token");

        // get user by id
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        // set user in HttpContext.Items["User"]

        var user = await userQueryService.Handle(getUserByIdQuery);
        _logger.LogInformation("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        _logger.LogInformation("Continuing with Middleware Pipeline");
        // call next middleware
        await next(context);
    }
}
```
