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
      string sql = "SELECT * FROM blogs;";
      return _db.Query<Blog>(sql).ToList();
    }

    internal Blog GetBlogById(int blogId)
    {
      string sql = "SELECT * FROM blogs WHERE id = @blogId;";
      return _db.QueryFirstOrDefault<Blog>(sql, new {blogId});
    }

    internal List<Comment> GetCommentsByBlogId(int blogId)
    {
      string sql = "SELECT * FROM comments c WHERE c.blogId = @blogId;";
      return _db.Query<Comment>(sql, new {blogId}).ToList();
    }

    internal Blog DeleteBlog(int blogId)
    {
      Blog foundBlog = GetBlogById(blogId);
      string sql = "DELETE FROM blogs WHERE id = @blogId LIMIT 1;";
      var affectedRows = _db.ExecuteScalar<int>(sql, new {blogId});
      if (affectedRows == 0)
      {
        throw new Exception("Failed to Delete");
      }
      return foundBlog;
    }

    internal Blog UpdateBlog(int blogId)
    {
      string sql = @"
      UPDATE blogs b
      SET 
        title = @Title,
        body = @Body,
        imgUrl = @ImgUrl,
        published = @Published
      WHERE b.id = @blogId;";
      return _db.QueryFirstOrDefault<Blog>(sql, new {blogId});
    }

    internal Blog CreateBlog(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs
        (id, title, body, imgUrl, published, creatorId)
      VALUES
        (@Id, @Title, @Body, @ImgUrl, @Published, @CreatorId);
      ";
      var id = _db.ExecuteScalar<int>(sql, new {blogData});
      blogData.Id = id;
      return blogData;
    }
  }
}
