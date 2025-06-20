using System;
public class Like
{
	public readonly string Email;
	public readonly string Nickname;

	public Like(string email, string nickname)
	{
		
		AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
		if (!emailSpecification.IsSatisfiedBy(email)) throw new Exception(emailSpecification.ErrorMessage);

		AccountNicknameSpecification nicknameSpecification = new AccountNicknameSpecification();
		if (!nicknameSpecification.IsSatisfiedBy(nickname)) throw new Exception(nicknameSpecification.ErrorMessage);

		Email = email;
		Nickname = nickname;
	}
	public LikeDTO ToDto()
	{
		return new LikeDTO(Email, Nickname);
	}
}
