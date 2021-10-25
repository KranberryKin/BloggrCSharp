using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggrCSharp.Models;
using BloggrCSharp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggrCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("blogs")]
        [Authorize]
        public async Task<ActionResult<List<Blog>>> GetBlogsByAccountId()
        {
            try
            {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

                 var blogs = _accountService.GetBlogsByAccountId(userInfo.Id);
                 return Ok(blogs);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("comments")]
        [Authorize]
        public async Task<ActionResult<List<Comment>>> GetCommentsByAccountId()
        {
            try
            {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

                 var comments = _accountService.GetCommentsByAccountId(userInfo.Id);
                 return Ok(comments);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account accountData)
        {
            try
            {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             accountData.Id = userInfo.Id;
                 var account = _accountService.UpdateAccount(accountData);
                 return Ok(account);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}