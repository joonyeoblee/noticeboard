public enum ETargetType
{
	Post = 0,
	Comment = 1,

	Count
}


public class Like
{
	public readonly string Email;
	public readonly string Nickname;
	public readonly string TargetID;
	public readonly ETargetType TargetType;
	public readonly bool IsLiked;
}
