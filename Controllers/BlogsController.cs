using System.Collections.Generic;
using System.Threading.Tasks;
using BloggrCSharp.Models;
using BloggrCSharp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;

namespace BloggrCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
      private readonly BlogsService _bs;

    public BlogsController(BlogsService bs)
    {
      _bs = bs;
    }

    [HttpGet]
      public ActionResult<List<Blog>> GetAll()
      {
        try
        {
             var blogs = _bs.GetAll();
             return Ok(blogs);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
      [HttpGet("{blogId}")]
      public ActionResult<Blog> GetBlogById(int blogId)
      {
        try
        {
             var blog = _bs.GetBlogById(blogId);
             return Ok(blog);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
      [HttpGet("{blogId}/comments")]
      public ActionResult<List<Comment>> GetCommentsByBlogId(int blogId)
      {
        try
        {
             var comments = _bs.GetCommentsByBlogId(blogId);
             return Ok(comments);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
      [HttpPost]
      public async Task<ActionResult<Blog>> CreateBlog([FromBody] Blog blogData)
      {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             blogData.CreatorId = userInfo.Id;
             var blog = _bs.CreateBlog(blogData);
             return Ok(blog);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
      [HttpPut("{blogId}")]
      public ActionResult<Blog> UpdateBlog(int blogId)
      {
        try
        {
             var blog = _bs.UpdateBlog(blogId);
             return Ok(blog);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
      [HttpDelete("{blogId}")]
      public ActionResult<Blog> DeleteBlog(int blogId)
      {
        try
        {
             _bs.DeleteBlog(blogId);
             return Ok("Success");
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
      }
    }
}