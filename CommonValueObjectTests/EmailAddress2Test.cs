namespace CommonValueObjectTests
{
    using CommonValueObject;
    using FluentAssertions;
    using Xunit;

    public class EmailAddress2Test
    {
        [Theory]
        [InlineData("a")]
        [InlineData("@y.com")]
        public void Invaild_email_address_should_fail(string strEmail)
        {
            var result = EmailAddress2.Create(strEmail);

            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error[0].ErrorMessage.Should().Be("The Email field is not a valid e-mail address.");
        }

        [Theory]
        [InlineData("x@y.com")]
        public void Vaild_email_address_should_pass(string strEmail)
        {
            var result = EmailAddress2.Create(strEmail);

            result.IsSuccess.Should().BeTrue();
        }
    }
}