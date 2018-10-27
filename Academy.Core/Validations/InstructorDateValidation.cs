using System;
using System.ComponentModel.DataAnnotations;
using Academy.Core.Instructors;

namespace Academy.Core.Validations
{
    public class InstructorDateValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var instructor = (Instructor) validationContext.ObjectInstance;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return base.IsValid(value, validationContext);
        }
    }
}
