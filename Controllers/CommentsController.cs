using System.Threading.Tasks;
using BloggrCSharp.Models;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using BloggrCSharp.Services;

namespace BloggrCSharp.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _cs;

    public CommentsController(CommentsService cs)
    {
      _cs = cs;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          commentData.CreatorId = userInfo.Id;
           var comment = _cs.CreateComment(commentData);
           return Ok(comment);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpPut("{commentId}")]
    public ActionResult<Comment> UpdateComment(int commentId, [FromBody] Comment commentData)
    {
      try
      {
           var comment = _cs.UpdateComment(commentId, commentData);
           return Ok(comment);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{commentId}")]
    public async Task<ActionResult<Comment>> DeleteComment(int commentId)
    {
      try
      {
           Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           _cs.DeleteComment(commentId, userInfo.Id);
           return Ok("DeYEETed");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}