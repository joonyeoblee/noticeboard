using System;
public class LikeTargetTypeSpecification : ISpecification<int>
{

    public bool IsSatisfiedBy(int value)
    {
        if (!Enum.IsDefined(typeof(ETargetType), value) || value == (int)ETargetType.Count)
        {
            ErrorMessage = "유효하지 않은 TargetType입니다.";
            return false;
        }
        return true;
    }
    public string ErrorMessage { get; private set; }
    
}