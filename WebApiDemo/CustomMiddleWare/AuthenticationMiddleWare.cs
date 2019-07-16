using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApiDemo.CustomMiddleWare
{
    public class AuthenticationMiddleWare
    {
        //2 kural var 1.si invoke methodu olması lazım
        private readonly RequestDelegate _next;

        public AuthenticationMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader==null)
            {
                await _next(context);
                return;
                
            }
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                var credentialString = "";
                try
                {
                     credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                }
                catch
                {
                    context.Response.StatusCode = 500;
                }
                var credentials = credentialString.Split(':');
                // veritabanı
                if (credentials[0]=="admin" && credentials[1]=="admin")
                {
                    var claims = new[]
                    {
                        new Claim("name", credentials[0]),
                        new Claim(ClaimTypes.Role,"Admin"), 
             
                    };
                    var identity=new ClaimsIdentity(claims,"Basic");
                    context.User=new ClaimsPrincipal(identity);
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
            }
            await _next(context);
        }
    }
}
