public class CommentPostIdSpecification : ISpecification<string>
{

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "PostID는 비어있을 수 없습니다.";
            return false;
        }
        return true;
    }
    public string ErrorMessage { get; private set; }
}