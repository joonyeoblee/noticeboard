using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class CommentDTO
{
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string PostID { get; set; }
    [FirestoreProperty]
    public string CommentId { get; set; }
    [FirestoreProperty]
    public string Content { get; set; }
    [FirestoreProperty]
    public Timestamp CommentTime { get; set; }
}
