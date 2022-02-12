using System;
using System.Collections.Generic;
using faceRecognitionApi.Models;
using faceRecognitionApi.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using faceRecognitionApi.Factory;
using System.Net;

namespace faceRecognitionApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UsersService _userService;
        

        public UsersController(UsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult>  Get()
        {
            
            return new ObjectResult(await _userService.Get());

        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<IActionResult> Get(string id)
        {
            var user =  _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(await user);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Users user)
        {
             await _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id:length(24)}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(string id, Users userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.Remove(id);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
       public ActionResult Login([FromBody]Users model)
        {
            if (ModelState.IsValid)
            {
                var result =  _userService.GetLoginUser(model.UserEmail, model.Password);
                if (result != null)
                {
                                   
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized, "Bad Credentials");
            }
            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }



    }
}