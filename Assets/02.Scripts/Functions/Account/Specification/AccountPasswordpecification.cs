using System.Text.RegularExpressions;
public class AccountPasswordpecification : ISpecification<string>
{

    // 닉네임: 한글 또는 영어로 구성, 2~7자
    private static readonly Regex NicknameRegex = new Regex(@"^[가-힣a-zA-Z]{2,7}$", RegexOptions.Compiled);

    // 금지된 닉네임 (비속어 등)
    private static readonly string[] ForbiddenNicknames = {"바보", "멍청이", "운영자", "김홍일"};
    public bool IsSatisfiedBy(string value)
    {
        if (value.Length < 6 || value.Length > 12)
        {
            ErrorMessage = "비밀번호는 6자 이상 12자 이하이어야 합니다.";
            return false;
        }

        return true;
    }
    public string ErrorMessage { get; private set; }
}
