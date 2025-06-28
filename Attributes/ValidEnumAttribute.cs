using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Attributes;

public class ValidEnumAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public ValidEnumAttribute(Type enumType)
    {
        _enumType = enumType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || Enum.IsDefined(_enumType, value))
            return ValidationResult.Success;

        var names = Enum.GetNames(_enumType);
        var values = Enum.GetValues(_enumType);

        var formattedValues = names
            .Select(name =>
            {
                var enumValue = Convert.ToInt32(Enum.Parse(_enumType, name));
                return $"{name} = {enumValue}";
            });

        var validValues = string.Join(", ", formattedValues);
        var errorMessage = $"Invalid value. Valid values are: {validValues}.";

        return new ValidationResult(errorMessage);
    }
}