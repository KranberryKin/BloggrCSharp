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

    internal Comment GetCommentById(int commentId)
    {
      return _cr.GetComment(commentId);
    }

    internal Comment UpdateComment(int commentId, Comment commentData)
    {
      Comment foundComment = GetCommentById(commentId);
      if (foundComment == null)
      {
        throw new Exception("Can't find Comment");
      }
      commentData.Id = commentId;
      return _cr.UpdateComment(commentData);
    }

    internal void DeleteComment(int commentId, string userId)
    {
      var foundComment = GetCommentById(commentId);
      if (foundComment.CreatorId != userId)
      {
        throw new Exception("Not Your Comment!");
      }
       _cr.DeleteComment(commentId);
    }
  }
}