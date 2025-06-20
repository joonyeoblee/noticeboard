using Firebase.Firestore;
[FirestoreData]
public class CommentDTO
{
    [FirestoreProperty]
    public string Email { get; private set; }
    [FirestoreProperty]
    public string Nickname { get; private set; }
    [FirestoreProperty]
    public string PostID { get; private set; }
    [FirestoreProperty]
    public string CommentID { get; private set; }
    [FirestoreProperty]
    public string Content { get; private set; }
    [FirestoreProperty]
    public Timestamp CommentTime { get; private set; }
}
