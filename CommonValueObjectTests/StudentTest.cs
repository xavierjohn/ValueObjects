namespace CommonValueObjectTests
{
    using CommonValueObject;
    using FluentAssertions;
    using Xunit;

    public class StudentAddressTest
    {
        [Fact]
        public void Invaild_student_should_fail()
        {
            var result = Student.Create(null, null, null);

            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(3);
            result.Error[0].ErrorMessage.Should().Be("The FirstName field is required.");
            result.Error[1].ErrorMessage.Should().Be("The LastName field is required.");
            result.Error[2].ErrorMessage.Should().Be("The Email field is required.");

            result = Student.Create(string.Empty, string.Empty, string.Empty);

            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(3);
            result.Error[0].ErrorMessage.Should().Be("The FirstName field is required.");
            result.Error[1].ErrorMessage.Should().Be("The LastName field is required.");
            result.Error[2].ErrorMessage.Should().Be("The Email field is required.");

            result = Student.Create("Xavier", "John", "Foo");

            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error[0].ErrorMessage.Should().Be("The Email field is not a valid e-mail address.");
        }

        [Fact]
        public void Vaild_Student_should_pass()
        {
            var result = Student.Create("Xavier", "John", "some@where.com");

            result.IsSuccess.Should().BeTrue();
        }
    }
}