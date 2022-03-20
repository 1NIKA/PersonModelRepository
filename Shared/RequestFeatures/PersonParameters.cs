namespace Shared.RequestFeatures;

public class PersonParameters : RequestParameters
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonalNumber { get; set; }
}