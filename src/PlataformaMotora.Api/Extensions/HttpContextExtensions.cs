using System.Security.Claims;

namespace PlataformaMotora.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? ObterUsuarioId(this HttpContext context)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var usuarioId))
                return usuarioId;

            return null;
        }
    }
}
