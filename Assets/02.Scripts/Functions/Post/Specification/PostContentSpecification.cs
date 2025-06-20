public class PostContentSpecification : ISpecification<string>
{
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ErrorMessage = "게시글 내용은 비어있을 수 없습니다.";
            return false;
        }
        if (value.Length > 1000)
        {
            ErrorMessage = "게시글 내용은 1000자 이내여야 합니다.";
            return false;
        }
        return true;
    }
    public string ErrorMessage { get; private set; }
    
}