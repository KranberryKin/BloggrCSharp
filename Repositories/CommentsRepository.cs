using System;
using System.Data;
using BloggrCSharp.Models;
using Dapper;

namespace BloggrCSharp.Repositories
{
  public class CommentsRepository
  {
    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal Comment GetComment(int commentId)
    {
      string sql = "SELECT * FROM comments WHERE id = @commentId;";
      return _db.QueryFirstOrDefault<Comment>(sql, new {commentId});
    }

    internal Comment CreateComment(Comment commentData)
    {
      string sql = @"
      INSERT INTO comments
        (body)
      VALUES
        (@Body);
      ";
      var id = _db.ExecuteScalar<int>(sql, new {commentData});
      commentData.Id = id;
      return commentData;
    }

    internal Comment UpdateComment(int commentId)
    {
      string sql = @"
      UPDATE comments c
      SET
        body = @Body
      WHERE c.id = @commentId;
      ";
      return _db.QueryFirstOrDefault<Comment>(sql, new {commentId});
    }

    internal Comment DeleteComment(int commentId)
    {
      Comment foundComment = GetComment(commentId);
      string sql = "DELETE FROM comments WHERE id = @commentId;";
      var affectedRows = _db.ExecuteScalar<int>(sql, new {commentId});
      if (affectedRows == 0)
      {
        throw new Exception("Failed to Delete");
      }
      return foundComment;
    }
  }
}
