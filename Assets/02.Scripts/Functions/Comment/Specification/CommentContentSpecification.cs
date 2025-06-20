public class CommentContentSpecification : ISpecification<string>
{
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ErrorMessage = "댓글 내용은 비어있을 수 없습니다.";
            return false;
        }
        if (value.Length > 500)
        {
            ErrorMessage = "댓글 내용은 500자 이내여야 합니다.";
            return false;
        }
        return true;
    }
    public string ErrorMessage { get; private set; }
}