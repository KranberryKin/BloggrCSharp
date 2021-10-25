using System;
using System.Collections.Generic;
using BloggrCSharp.Models;
using BloggrCSharp.Repositories;

namespace BloggrCSharp.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _br;

    public BlogsService(BlogsRepository br)
    {
      _br = br;
    }

    internal List<Blog> GetAll()
    {
      return _br.GetAll();
    }

    internal Blog GetBlogById(int blogId)
    {
      return _br.GetBlogById(blogId);
    }

    internal List<Comment> GetCommentsByBlogId(int blogId)
    {
      return _br.GetCommentsByBlogId(blogId);
    }

    internal Blog CreateBlog(Blog blogData)
    {
      return _br.CreateBlog(blogData);
    }

    internal Blog UpdateBlog(int blogId, Blog blogData)
    {
      Blog foundBlog = GetBlogById(blogId);
      if (foundBlog == null)
      {
        throw new Exception("Can't find Blog");
      }
      blogData.Id = blogId;
      return _br.UpdateBlog(blogData);
    }

    internal void DeleteBlog(int blogId, string creatorId)
    {
       Blog foundBlog = GetBlogById(blogId);
      if (foundBlog.CreatorId != creatorId)
      {
        throw new Exception("This isn't your Blog!");
      }
      _br.DeleteBlog(blogId);
    }
  }
}