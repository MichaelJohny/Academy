using System.ComponentModel.DataAnnotations;
using Academy.Core.Students;

namespace Academy.Core.Validations
{
    public class MobileValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (Student)validationContext.ObjectInstance;

            if (student.Mobile1 != null && (student.Mobile1.Length > 11 || student.Mobile1.Length < 11))
                return new ValidationResult("Wrong Number");


            return ValidationResult.Success;
        }

    }

    public class Mobile2Validation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (Student)validationContext.ObjectInstance;
            if (student.Mobile2!=null && (student.Mobile2.Length > 11 || student.Mobile2.Length < 11))
                return new ValidationResult("Wrong Number");
            return ValidationResult.Success;
        }
    }

}
