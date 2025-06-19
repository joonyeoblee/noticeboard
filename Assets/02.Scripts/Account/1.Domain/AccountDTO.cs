public class AccountDTO
{
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }


    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // Account 도메인 객체로부터 DTO 생성
    public static AccountDTO FromDomain(Account account)
    {
        return new AccountDTO(account.Email, account.Nickname, account.Password);
    }

    // Account 도메인 객체로 변환 (필요시)
    public Account ToDomain()
    {
        return new Account(Email, Nickname, Password);
    }
}
