public class PostViewCountSpecification : ISpecification<int>
{

    public bool IsSatisfiedBy(int value)
    {
        if (value < 0)
        {
            ErrorMessage = "조회수는 0 이상이어야 합니다.";
            return false;
        }
        return true;
    }
    public string ErrorMessage { get; private set; }
}