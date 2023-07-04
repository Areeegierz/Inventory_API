using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Inventory_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory_API.Models.db;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;
using System.Net.Http.Headers;
using System.Numerics;
using Inventory_API.Models.AD;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly InventoryContext _db;

        public AuthController(InventoryContext db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // TODO: Validate user credentials
            if (loginModel.Username != null || loginModel.Password != null)
            {

                ADLogin u = new ADLogin();
                u.username = loginModel.Username;
                u.password = loginModel.Password;
                u.userType = "CPAC";




                string authInfo = "fc_vdo_content_intf" + ":" + "E3nzMjMrpWusLApW";
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                ClaimsIdentity claim = null;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://bluenet.cipcloud.net/api/v2/authen/auth", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var root = JsonConvert.DeserializeObject<JsonViewModel>(apiResponse);


                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey@123"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, loginModel.Username)
                        };

                        var tokenOptions = new JwtSecurityToken(
                            issuer: "http://localhost:3000",
                            audience: "http://localhost:3000",
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: signinCredentials
                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        // check API Status 200
                        if (root.status == 200)
                        {

                            // check user in db
                            var thisUser = await _db.Users.Where(i => i.Username == loginModel.Username).Select(i=> new { Id = i.Id, Name = i.Fname + " " +i.Lname ,Status = i.Status }).FirstOrDefaultAsync();
                            if (thisUser != null)
                            {
                                return Ok(new { User = thisUser, Token = tokenString });
                            }

                            /// Not have user in database
                            /// Save user go to database

                            var userModel = new User();
                            userModel.Username = loginModel.Username;
                            userModel.Password = loginModel.Password;
                            userModel.Fname = root.data.firstName;
                            userModel.Lname = root.data.lastName;
                            userModel.Email = loginModel.Username + "@scg.com";
                            userModel.Status = "Member";
                            _db.Users.Add(userModel);
                            _db.SaveChanges();
                            var newUser = await _db.Users.Where(i => i.Username == loginModel.Username).Select(i => new { Id = i.Id, Name = i.Fname + " " + i.Lname, Status = i.Status }).FirstOrDefaultAsync();

                            return Ok(new { User = newUser ,Token = tokenString });
                        }
                        return Unauthorized();
                    }
                }
            }
            return Unauthorized();
        }
    }
}
