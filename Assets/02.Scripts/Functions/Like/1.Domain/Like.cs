using Firebase.Firestore;
public enum ETargetType
{
	Post = 0,
	Comment = 1,

	Count
}

[FirestoreData]
public class Like
{
	[FirestoreProperty]
	public string Email { get; set; }
	[FirestoreProperty]
	public string Nickname { get; set; }
	[FirestoreProperty]
	public string TargetID { get; set; }
	[FirestoreProperty]
	public int TargetType { get; set; }
	[FirestoreProperty]
	public bool IsLiked { get; set; }
}
