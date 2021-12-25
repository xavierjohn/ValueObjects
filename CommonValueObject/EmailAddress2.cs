namespace CommonValueObject
{
    using CSharpFunctionalExtensions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EmailAddress2 : ValueObject
    {
        [Required, EmailAddress]
        public string Email { get; }

        private EmailAddress2(string value)
        {
            Email = value;
        }

        public static Result<EmailAddress2, List<ValidationResult>> Create(string strEmail)
        {
            var model = new EmailAddress2(strEmail);
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var errorResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, context, errorResults, true))
            {
                return Result.Failure<EmailAddress2, List<ValidationResult>>(errorResults);
            }
            return model;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}