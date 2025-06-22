using System.Collections.Generic;
using Firebase.Firestore;
[FirestoreData]
public class CommentDTO
{
    [FirestoreDocumentId]
    public string CommentID { get; set; }
    [FirestoreProperty]
    public string Email { get; private set; }
    [FirestoreProperty]
    public string Nickname { get; private set; }
    [FirestoreProperty]
    public string Content { get; private set; }
    [FirestoreProperty]
    public Timestamp CommentTime { get; private set; }

    public List<LikeDTO> Likes { get; private set; } = new List<LikeDTO>();
    public int LikeCount => Likes.Count;

    public CommentDTO() { }

    public CommentDTO(string commentID, string email, string nickname, string content, Timestamp commentTime)
    {
        CommentID = commentID;
        Email = email;
        Nickname = nickname;
        Content = content;
        CommentTime = commentTime;
    }

    public CommentDTO(CommentDTO commentDto, List<LikeDTO> likes)
    {
        CommentID = commentDto.CommentID;
        Email = commentDto.Email;
        Nickname = commentDto.Nickname;
        Content = commentDto.Content;
        CommentTime = commentDto.CommentTime;
        Likes = likes;
    }

    public Comment ToDomain()
    {
        return new Comment(Email, Nickname, Content, CommentTime);
    }
}