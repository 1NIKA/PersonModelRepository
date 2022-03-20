using System.ComponentModel.DataAnnotations;

namespace Shared;

public class MinAgeAttribute : ValidationAttribute
{
    private int MinAge { get; }
    
    public MinAgeAttribute(int minAge)
    {
        MinAge = minAge;
        ErrorMessage = "{0} must be someone at least {1} years of age";
    }
    
    public override bool IsValid(object? value)
    {
        if (value != null && DateTime.TryParse(value.ToString(), out var date))
            return date.AddYears(MinAge) < DateTime.Now;

        return false;
    }
    
    public override string FormatErrorMessage(string name) =>
        string.Format(ErrorMessageString, name, MinAge);
}