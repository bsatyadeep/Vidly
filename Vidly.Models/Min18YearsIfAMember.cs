using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.payAsYouGo)
                return ValidationResult.Success;
            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is Required.");

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 Years Old to go on a Membership.");
        }
    }
}