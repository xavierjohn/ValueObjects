namespace CommonValueObjectTests
{
    using CommonValueObject;
    using FluentAssertions;
    using Xunit;

    public class EmailAddresssTest
    {
        [Theory]
        [InlineData("a")]
        [InlineData("@y.com")]
        public void Invaild_email_address_should_fail(string strEmail)
        {
            var result = EmailAddress.Create(strEmail);

            result.IsFailure.Should().BeTrue();
        }

        [Theory]
        [InlineData("x@y.com")]
        public void Vaild_email_address_should_pass(string strEmail)
        {
            var result = EmailAddress.Create(strEmail);

            result.IsSuccess.Should().BeTrue();
        }
    }
}