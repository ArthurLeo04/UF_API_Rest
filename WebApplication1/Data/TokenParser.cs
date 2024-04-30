using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.Data
{
    public class TokenParser
    {
        //Static function to parse the token from string to JwtSecurityToken
        public static JwtSecurityToken ParseToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadToken(token) as JwtSecurityToken;
        }

        //Static function to get the claim value from the token
        public static string GetClaimValue(JwtSecurityToken token, string claimType)
        {
            return token.Claims.First(claim => claim.Type == claimType).Value;
        }

        //Static function to get the nameid claim value from the token
        public static Guid GetUserId(JwtSecurityToken token)
        {
            return Guid.Parse(GetClaimValue(token, "nameid"));
        }

        //Static function to get the role claim value from the token
        public static string GetUserRole(JwtSecurityToken token)
        {
            return GetClaimValue(token, "role");
        }

        // Static function to get if the user is a client
        public static bool IsClient(JwtSecurityToken token)
        {
            return GetUserRole(token) == "client";
        }

        // Static function to get if the user is a server
        public static bool IsServer(JwtSecurityToken token)
        {
            return GetUserRole(token) == "server";
        }

        //Static function to get the username claim value from the token
        public static string GetUsername(JwtSecurityToken token)
        {
            return GetClaimValue(token, "unique_name");
        }
    }
}
