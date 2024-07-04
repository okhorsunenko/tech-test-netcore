using Todo.Services;
using Xunit;

namespace Todo.Tests
{
    public class GravatarTests
    {
        [Theory]
        [InlineData("test@test.com", "b642b4217b34b1e8d3bd915fc65c4452")]
        [InlineData("TEST@TEST.COM", "b642b4217b34b1e8d3bd915fc65c4452")]
        public void GetHash_ReturnsCorrectHash_ForValidEmail(string email, string expectedHash)
        {
            var hash = Gravatar.GetHash(email);

            Assert.Equal(expectedHash, hash);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GetHash_ReturnsEmptyString_ForInvalidEmail(string email)
        {
            var hash = Gravatar.GetHash(email);

            Assert.Equal(string.Empty, hash);
        }
    }
}
