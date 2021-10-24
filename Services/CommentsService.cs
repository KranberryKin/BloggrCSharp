using System;
using BloggrCSharp.Models;
using BloggrCSharp.Repositories;

namespace BloggrCSharp.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _cr;

    public CommentsService(CommentsRepository cr)
    {
      _cr = cr;
    }

    internal Comment CreateComment(Comment commentData)
    {
      return _cr.CreateComment(commentData);
    }

    internal Comment UpdateComment(int commentId)
    {
      return _cr.UpdateComment(commentId);
    }

    internal Comment DeleteComment(int commentId)
    {
      return _cr.DeleteComment(commentId);
    }
  }
}