namespace CommonValueObject
{
    using CSharpFunctionalExtensions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student : ValueObject
    {
        [Required]
        public string FirstName { get; }

        [Required]
        public string LastName { get; }


        public EmailAddress2 Email { get; }

        private Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private Student(string firstName, string lastName, EmailAddress2 email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static Result<Student, List<ValidationResult>> Create(string firstName, string lastName, string email)
        {
            var errorResults = new List<ValidationResult>();
            var model = new Student(firstName, lastName);
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, errorResults, true);

            var emailResult = EmailAddress2.Create(email);
            if (emailResult.IsFailure)
            {
                errorResults.AddRange(emailResult.Error);
            }

            if (errorResults.Count > 0)
            {
                return Result.Failure<Student, List<ValidationResult>>(errorResults);
            }
            return new Student(firstName, lastName, emailResult.Value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Email;
        }
    }
}