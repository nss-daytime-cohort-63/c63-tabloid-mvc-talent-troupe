using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using TabloidMVC.Utils;

public class CommentRepository : BaseRepository, ICommentRepository
{
    public CommentRepository(IConfiguration config) : base(config) { }

    public void Add(Comment comment)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            INSERT INTO Comment (Subject, Content, PostId, CreateDateTime, UserProfileId)
            OUTPUT INSERTED.ID
            VALUES (@Subject, @Content, @PostId, @CreateDateTime, @UserProfileId)";
                cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                cmd.Parameters.AddWithValue("@Content", comment.Content);
                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);

                comment.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public List<Comment> GetCommentsByPostId(int postId)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            SELECT Id, PostId, Subject, Content, CreateDateTime, UserProfileId
              FROM Comment
             WHERE PostId = @PostId";
                cmd.Parameters.AddWithValue("@PostId", postId);

                var reader = cmd.ExecuteReader();

                var comments = new List<Comment>();

                while (reader.Read())
                {
                    comments.Add(new Comment()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                        Subject = reader.GetString(reader.GetOrdinal("Subject")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                    });
                }

                reader.Close();

                return comments;
            }
        }
    }
}

