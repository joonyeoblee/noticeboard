using System;
using Firebase.Firestore;
public class Account
{
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }


    public Account(string email, string nickname)
    {
        // 규칙을 객체로 캡슐화해서 분리한다.
        // 그래서 도메인과 UI는 규칙을 만족한가?
        // 캡슐화한 규칙 : 명세(Specification)
        // 이메일 검증
        AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // 닉네임 검증
        AccountNicknameSpecification nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }
        
        Email = email;
        Nickname = nickname;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname);
    }
}
