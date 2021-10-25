using System;
using System.Data;
using System.Linq;
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
      string sql = @"
      SELECT 
      c.*,
      a.*
      FROM comments c 
      JOIN accounts a
      on a.id = c.creatorId
      WHERE c.id = @commentId;";
      return _db.Query<Comment, Account, Comment>(sql,(c, a) =>
      {
        c.Creator = a;
        return c;
      } , new {commentId}).FirstOrDefault();
    }

    internal Comment CreateComment(Comment commentData)
    {
      string sql = @"
      INSERT INTO comments
        (body, creatorId, blog)
      VALUES
        (@Body, @CreatorId, @Blog);
        SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, commentData);
      var gottenComment = GetComment(id);
      return gottenComment;
    }

    internal Comment UpdateComment(Comment commentData)
    {
      string sql = @"
      UPDATE comments c
      SET
        body = @Body
      WHERE c.id = @Id;
      ";
      var affectedRows =  _db.Execute(sql, commentData);
       if (affectedRows > 1)
      {
        throw new System.Exception("NEIN");
      }
      if (affectedRows == 0)
      {
        throw new System.Exception("The update failed");
      }
      return commentData;
    }

    internal void DeleteComment(int commentId)
    {
      string sql = "DELETE FROM comments WHERE id = @commentId;";
      var affectedRows = _db.ExecuteScalar<int>(sql, new {commentId});
      if (affectedRows == 0)
      {
        throw new Exception("Failed to Delete");
      }
    }
  }
}
