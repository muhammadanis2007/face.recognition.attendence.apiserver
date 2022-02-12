using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using faceRecognitionApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using faceRecognitionApi.Factory;
using Microsoft.Extensions.Options;

namespace faceRecognitionApi.Services
{
    public class UsersService
    {

        private readonly IMongoCollection<Users> _users;
        private readonly AppSettings _appSettings;

        public UsersService(IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            var client = new MongoClient(config.GetConnectionString("faceRecognitionDb"));
            var database = client.GetDatabase("facerecognitionapp");
            _users = database.GetCollection<Users>("users");
            
        }

        public async Task<List<Users>> Get()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<Users> Get(string id)
        {
            return await _users.Find<Users>(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> Create(Users users)
        {
            await _users.InsertOneAsync(users);
            return  users;
        }

        public  async Task<Users> Update(string id, Users userIn)
        {
           await  _users.ReplaceOneAsync(user => user.Id == id, userIn);
            return userIn;
        }

        public async Task<Users> Remove(Users userIn)
        {
          await  _users.DeleteOneAsync(user => user.Id == userIn.Id);
           return userIn;
        }

        public async Task<bool> Remove(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
            return true;
        }


        public Users GetLoginUser(string UserEmail, string password)
        {


            var user = _users.Find(x => x.UserEmail == UserEmail && x.Password == password).SingleOrDefault();


            // return null if user not found
            if (_users == null)
                return null;


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRole)
                }),
                Expires = DateTime.UtcNow.AddDays(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;


            /*var builder = Builders<Users>.Filter;
            var filter = builder.Eq("Login", UserEmail) & builder.Eq("Password", password);

            return await _users
                        .Find(filter)
                        .FirstOrDefaultAsync();*/




        }


        public async Task<Users> GetUserByEmail(string UserEmail)
        {

            var builder = Builders<Users>.Filter;
            var filter = builder.Eq("Login", UserEmail);

            return await _users
                        .Find(filter)
                        .FirstOrDefaultAsync();

        }



       



    }
}
