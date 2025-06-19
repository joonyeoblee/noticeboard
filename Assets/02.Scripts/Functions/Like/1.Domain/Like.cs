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
	public readonly string LikeID;
	public readonly int TargetType;

	public Like(string email, string nickname, string targetID, string likeID, int targetType)
	{
		Email = email;
		Nickname = nickname;
		TargetID = targetID;
		LikeID = likeID;
		TargetType = targetType;
	}
	public LikeDTO ToDto()
	{
		return new LikeDTO(Email, Nickname, TargetID, LikeID, TargetType);
	}
}
