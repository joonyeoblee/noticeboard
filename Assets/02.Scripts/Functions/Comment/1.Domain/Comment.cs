using System;
using Firebase.Firestore;

public class Comment
{
    public string CommentID;
    public string Email;
    public string Nickname;
    public string Content;
    public Timestamp CommentTime;

    public Comment(string email, string nickname, string content)
        : this("", email, nickname, content, Timestamp.GetCurrentTimestamp())
    {
    }

    public Comment(string commentID, string email, string nickname, string content, Timestamp commentTime)
    {
        CommentID = commentID;
        Email = email;
        Nickname = nickname;
        Content = content;
        CommentTime = commentTime;
    }

    public CommentDTO ToDto()
    {
        return new CommentDTO(CommentID, Email, Nickname, Content, CommentTime);
    }
}