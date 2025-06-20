using UnityEngine;

public class PostSpecification : ISpecification<string>
{

    public bool IsSatisfiedBy(string value)
    {

        return false;
    }
    public string ErrorMessage { get; private set; }
}
