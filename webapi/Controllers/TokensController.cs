using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Middlewares;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        private readonly UserRepository userRepository;

        public TokensController()
        {
            userRepository = new UserRepository(Startup.connString);
        }

        [Route("access")]
        [HttpPost]
        public dynamic GetToken([FromBody]User user)
        {
            dynamic result;

            var getUser = userRepository.GetByEmailAndPassword(user.Email, user.Password);

            if (getUser != null)
            {
                if (getUser.Is_Activated == true)
                {
                    result = new
                    {
                        code = ReturnCodes.DataGetSucceeded,
                        data = new
                        {
                            user = new
                            {
                                id = getUser.UserId,
                                email = getUser.Email,
                                activation = getUser.Is_Activated,
                            },
                            token = Utilities.GenerateTokens(user.Email),
                        }
                    };
                }
                else
                {
                    result = new
                    {
                        code = ReturnCodes.DataGetFailedWithErrorRelationships,
                        data = DBNull.Value,
                    };

                }
            }
            else
            {

                result = new
                {
                    code = ReturnCodes.DataGetFailed,
                    data = DBNull.Value,
                };
            }

            return result;
        }

        [Route("refresh")]
        [Authorize]
        [HttpPut]
        public dynamic RefreshToken()
        {
            var authorization = HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            var token = authorization.Substring(authorization.IndexOf(' ') + 1);
            var jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            dynamic result = new
            {
                code = ReturnCodes.DataGetSucceeded,
                data = new
                {
                    user = new
                    {
                        email = jwt.Subject,
                    },
                    token = Utilities.GenerateTokens(jwt.Subject),
                }
            };

            return result;
        }

        [Route("validate")]
        [Authorize]
        [HttpPost]
        public dynamic ValidateToken()
        {
            /*
            var authorization = HttpContext.Request.Headers["Authorization"].SingleOrDefault();
            var token = authorization.Substring(authorization.IndexOf(' ') + 1);
            var jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            dynamic result = jwt;
            */
            dynamic result = new
            {
                code = ReturnCodes.DataGetSucceeded,
            };
            return result;
        }


    }
}
