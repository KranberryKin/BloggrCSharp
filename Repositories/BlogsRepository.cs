using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggrCSharp.Models;
using Dapper;

namespace BloggrCSharp.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Blog> GetAll()
    {
      string sql = "SELECT * FROM blogs b WHERE b.published = 1;";
      return _db.Query<Blog>(sql).ToList();
    }

    internal Blog GetBlogById(int blogId)
    {
      string sql = @"
      SELECT 
      b.*,
      a.* 
      FROM blogs b
      JOIN accounts a on a.id = b.creatorId
       WHERE b.id = @blogId;";
      return _db.Query<Blog, Account, Blog>(sql, (b, a) => 
      {
        b.Creator = a;
        return b;
      }
      , new{blogId}).FirstOrDefault();
    }

    internal List<Comment> GetCommentsByBlogId(int blogId)
    {
      string sql = "SELECT * FROM comments c WHERE c.blog = @blogId;";
      return _db.Query<Comment>(sql, new {blogId}).ToList();
    }

    internal void DeleteBlog(int blogId)
    {
      Blog foundBlog = GetBlogById(blogId);
      string sql = "DELETE FROM blogs WHERE id = @blogId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {blogId});
      if (affectedRows == 0)
      {
        throw new Exception("Failed to Delete");
      }
    }

    internal Blog UpdateBlog(Blog blogData)
    {
      string sql = @"
      UPDATE blogs 
      SET 
        title = @Title,
        body = @Body,
        imgUrl = @ImgUrl,
        published = @Published
      WHERE id = @Id;";
      var affectedRows = _db.Execute(sql, blogData);
      if (affectedRows > 1)
      {
        throw new System.Exception("NEIN");
      }
      if (affectedRows == 0)
      {
        throw new System.Exception("The update failed");
      }
      return blogData;
    }

    internal Blog CreateBlog(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs( title, body, imgUrl, published, creatorId)
      VALUES( @Title, @Body, @ImgUrl, @Published, @CreatorId);
        SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, blogData);
      blogData.Id = id;
      var gottenData = GetBlogById(id);
      return gottenData;
    }
  }
}
