using Firebase.Firestore;

public enum ETargetType
{
	Post = 0,
	Comment = 1,

	Count
}

public class Like
{
	public string Email;
	public string Nickname;
	public string TargetID;
	public int TargetType;
	public bool IsLiked;
}
