using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggrCSharp.Models;
using Dapper;

namespace BloggrCSharp.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetByEmail(string userEmail)
        {
            string sql = "SELECT * FROM accounts WHERE email = @userEmail";
            return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }

        internal Account Edit(Account update)
        {
            string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

    internal List<Blog> GetBlogsByAccountId(string userId)
    {
        string sql = "SELECT * FROM blogs b WHERE b.creatorId = @userId;";
        return _db.Query<Blog>(sql, new {userId}).ToList();
    }

    internal List<Comment> GetCommentsByAccountId(string userId)
    {
        string sql = "SELECT * FROM comments c WHERE c.creatorId = @userId;";
        return _db.Query<Comment>(sql, new {userId}).ToList();
    }
  }
}
