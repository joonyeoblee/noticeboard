using Firebase.Firestore;
[FirestoreData]
public class CommentDTO
{
    [FirestoreDocumentId]
    public string CommentID { get; private set; }
    [FirestoreProperty]
    public string Email { get; private set; }
    [FirestoreProperty]
    public string Nickname { get; private set; }
    [FirestoreProperty]
    public string Content { get; private set; }
    [FirestoreProperty]
    public Timestamp CommentTime { get; private set; }

    public CommentDTO(string email, string nickname, string content, Timestamp commentTime)
    {
        Email = email;
        Nickname = nickname;
        Content = content;
        CommentTime = commentTime;
    }
    public CommentDTO() { }

    public Comment ToDomain()
    {
        return new Comment(Email, Nickname, Content, CommentTime);
    }
}
