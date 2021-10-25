using System.Collections.Generic;
using BloggrCSharp.Models;
using BloggrCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggrCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfile(string profileId)
    {
      try
      {
           var profile = _ps.GetProfile(profileId);
           return Ok(profile);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/blogs")]
    public ActionResult<List<Blog>> GetBlogsByProfileId(string profileId)
    {
      try
      {
           var blogs = _ps.GetBlogsByProfileId(profileId);
           return Ok(blogs);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/comments")]
    public ActionResult<List<Comment>> GetCommentsByProfileId(string profileId)
    {
      try
      {
           var comments = _ps.GetCommentsByProfileId(profileId);
           return Ok(comments);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}