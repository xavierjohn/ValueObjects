namespace CommonValueObject
{
    using CSharpFunctionalExtensions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student2 : ValueObject
    {
        [Required]
        public string FirstName { get; private set; } = String.Empty;

        [Required]
        public string LastName { get; private set; } = String.Empty;


        public EmailAddress2 Email { get; private set; }

        private Student2()
        {
            Email = EmailAddress2.Create("somewhere@unknown.com").Value;
        }

        public static Result<Student2, List<ValidationResult>> Create(string firstName, string lastName, string email)
        {
            var errorResults = new List<ValidationResult>();
            var model = new Student2
            {
                FirstName = firstName,
                LastName = lastName
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, errorResults, true);

            var emailResult = EmailAddress2.Create(email);
            if (emailResult.IsFailure)
            {
                errorResults.AddRange(emailResult.Error);
            }

            if (errorResults.Count > 0)
            {
                return Result.Failure<Student2, List<ValidationResult>>(errorResults);
            }
            return model;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Email;
        }
    }
}