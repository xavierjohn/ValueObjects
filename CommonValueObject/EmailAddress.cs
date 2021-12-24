namespace CommonValueObject
{
    using CSharpFunctionalExtensions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EmailAddress : ValueObject
    {
        [Required, EmailAddress]
        public string Email { get; }

        private EmailAddress(string value)
        {
            Email = value;
        }

        public static Result<EmailAddress, Error> Create(string strEmail)
        {
            var model = new EmailAddress(strEmail);
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var errorResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, context, errorResults, true))
            {
                return Result.Failure<EmailAddress, Error>(Errors.General.ValueIsInvalid("emailAddress"));
            }
            return model;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}