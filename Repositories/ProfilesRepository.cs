using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggrCSharp.Models;
using Dapper;

namespace BloggrCSharp.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _bd;

    public ProfilesRepository(IDbConnection bd)
    {
      _bd = bd;
    }

    internal Profile GetProfile(string profileId)
    {
      string sql = "SELECT * FROM accounts a WHERE a.id = @profileId;";
      return _bd.QueryFirstOrDefault<Profile>(sql, new {profileId});
    }

    internal List<Blog> GetBlogsByProfileId(string profileId)
    {
      string sql = "SELECT * FROM blogs b WHERE b.creatorId = @profileId;";
      return _bd.Query<Blog>(sql, new {profileId}).ToList();
    }

    internal List<Comment> GetCommentsByProfileId(string profileId)
    {
      string sql = "SELECT * FROM comments c WHERE c.creatorId = @profileId;";
      return _bd.Query<Comment>(sql, new {profileId}).ToList();
    }
  }
}
