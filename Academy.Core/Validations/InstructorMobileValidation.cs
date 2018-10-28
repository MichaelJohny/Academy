using System.ComponentModel.DataAnnotations;
using Academy.Core.Instructors;

namespace Academy.Core.Validations
{
    public class InstructorMobileValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instructor = (Instructor)validationContext.ObjectInstance;

            if (instructor.Mobile1 != null && (instructor.Mobile1.Length > 11 || instructor.Mobile1.Length < 11))
                return new ValidationResult("Wrong Number");

            return ValidationResult.Success;
        }
    }

    public class InstructorMobile2Validation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instructor = (Instructor)validationContext.ObjectInstance;
            if (instructor.Mobile2 != null && (instructor.Mobile2.Length > 11 || instructor.Mobile2.Length < 11))
                return new ValidationResult("Wrong Number");
            return ValidationResult.Success;
        }
    }
}
