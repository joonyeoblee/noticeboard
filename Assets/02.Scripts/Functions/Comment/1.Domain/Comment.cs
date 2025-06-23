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
        : this(email, nickname, content, Timestamp.GetCurrentTimestamp())
    {
    }

    public Comment(string email, string nickname, string content, Timestamp commentTime)
    {
        AccountEmailSpecification accountEmailSpecification = new AccountEmailSpecification();
        if (!accountEmailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(accountEmailSpecification.ErrorMessage);
        }

        AccountNicknameSpecification accountNicknameSpecification = new AccountNicknameSpecification();
        if (!accountNicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(accountNicknameSpecification.ErrorMessage);
        }

        CommentContentSpecification commentContentSpecification = new CommentContentSpecification();
        if (!commentContentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(commentContentSpecification.ErrorMessage);
        }
       
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