using Firebase.Firestore;
[FirestoreData]
public class Comment
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
