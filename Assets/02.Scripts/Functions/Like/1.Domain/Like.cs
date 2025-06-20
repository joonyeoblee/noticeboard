using System;
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
		
		AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
		if (!emailSpecification.IsSatisfiedBy(email)) throw new Exception(emailSpecification.ErrorMessage);

		AccountNicknameSpecification nicknameSpecification = new AccountNicknameSpecification();
		if (!nicknameSpecification.IsSatisfiedBy(nickname)) throw new Exception(nicknameSpecification.ErrorMessage);

		LikeTargetIdSpecification targetIdSpecification = new LikeTargetIdSpecification();
		if (!targetIdSpecification.IsSatisfiedBy(targetID)) throw new Exception(targetIdSpecification.ErrorMessage);

		LikeIdSpecification likeIdSpecification = new LikeIdSpecification();
		if (!likeIdSpecification.IsSatisfiedBy(likeID)) throw new Exception(likeIdSpecification.ErrorMessage);

		LikeTargetTypeSpecification targetTypeSpecification = new LikeTargetTypeSpecification();
		if (!targetTypeSpecification.IsSatisfiedBy(targetType)) throw new Exception(targetTypeSpecification.ErrorMessage);

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
