using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.Logger;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;

namespace StoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        
        [Authorize]
        [HttpGet]
        public IActionResult GetUserList(int pageIndex, int pageSize)
        {
            var claims = User.Claims;
            if (pageSize <=0 || pageIndex <= 0)
            {
                pageIndex = 1;
                pageSize = 100;
            }
            var userList = _userService.GetAllUsers(pageIndex, pageSize);
            if (userList == null)
            {
                
                return NotFound();
            }
            return Ok(userList);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var userRespond = await _userService.GetUserById(userId);
            if (userRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, userRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, userRespond);
            }
        }

        [HttpPost]
        public async Task<IActionResult>CreateUser(UserRequest userRequest)
        {
            var userRespond = await _userService.CreateUser(userRequest);
            if(userRespond.StatusCode==200)
            {
                return StatusCode(StatusCodes.Status200OK, userRespond);
            }
            else if(userRespond.StatusCode==400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, userRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, userRespond);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserRequest userRequest)
        {
            UserRespond userRespond = new UserRespond();
            if (userId != userRequest.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, userRespond);
            }
            else
            {
                userRespond = await _userService.UpdateUser(userRequest);
                if (userRespond.StatusCode == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, userRespond);
                }
                else if (userRespond.StatusCode == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, userRespond);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, userRespond);
                }
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var userRespond = await _userService.DeleteUser(userId);
            if (userRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, userRespond);
            }
            else if (userRespond.StatusCode == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, userRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, userRespond);
            }
        }
    }
}
