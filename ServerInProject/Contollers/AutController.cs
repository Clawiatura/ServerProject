using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ServerInProject.Contollers
{
    public class AutController 
    {
        private readonly ProrjdContext _db; 


        
        public AutController(ProrjdContext db)
        {
            _db = db;
        }

        public async Task<string> Login(UserCredentials credentials) 
        {

            
            var user = await _db.Клиентs.FirstOrDefaultAsync(u => u.Email == credentials.Username && u.PasswordHash == credentials.Password); 


            if (user == null)
            {
                throw new UnauthorizedAccessException("Неверный логин или пароль."); 
            }


            
            var tokenString = GenerateJwtToken(user.IdКлиента.ToString(), user.Имя); 

           
            return tokenString; 




        }


        
        private string GenerateJwtToken(string userId, string userName)
        {


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("52Vepedi"); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);



        }

    }


    
    public class UserCredentials
    {

        public string Username { get; set; }
        public string Password { get; set; }


    }



}

