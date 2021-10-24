using System;
using System.Collections.Generic;
using BloggrCSharp.Models;
using BloggrCSharp.Repositories;

namespace BloggrCSharp.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Profile GetProfile(string profileId)
    {
      return _pr.GetProfile(profileId);
    }

    internal List<Blog> GetBlogsByProfileId(string profileId)
    {
      return _pr.GetBlogsByProfileId(profileId);
    }

    internal List<Comment> GetCommentsByProfileId(string profileId)
    {
      return _pr.GetCommentsByProfileId(profileId);
    }
  }
}