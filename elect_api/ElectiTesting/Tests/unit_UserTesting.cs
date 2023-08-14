using ElectiTask.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ElectiTask.Tests
{
    public class UserTests
    {
        // Test: Creating a new User instance
        [Fact]
        public void CanCreateUser()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.NotNull(user); // Verify that a user instance can be created
        }

        // Test: Setting and getting the Username property
        [Fact]
        public void CanSetAndGetUsername()
        {
            // Arrange
            var user = new User();
            var username = "john_doe";

            // Act
            user.Username = username;

            // Assert
            Assert.Equal(username, user.Username); // Verify that the Username property can be set and retrieved
        }

        // Test: Setting and getting the Email property
        [Fact]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var user = new User();
            var email = "john@example.com";

            // Act
            user.Email = email;

            // Assert
            Assert.Equal(email, user.Email); // Verify that the Email property can be set and retrieved
        }

        // Test: Setting and getting the Password property
        [Fact]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var user = new User();
            var password = "mysecretpassword";

            // Act
            user.Password = password;

            // Assert
            Assert.Equal(password, user.Password); // Verify that the Password property can be set and retrieved
        }

        // Test: Maximum length constraint of the Username property
        [Fact]
        public void UsernameMaxLengthIs50()
        {
            // Arrange
            var user = new User();
            var maxLengthAttribute = typeof(User).GetProperty(nameof(User.Username)).GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;

            // Assert
            Assert.NotNull(maxLengthAttribute); // Verify that the StringLengthAttribute is applied to the Username property
            Assert.Equal(50, maxLengthAttribute.MaximumLength); // Verify that the maximum length of the Username property is set to 50
        }

        // Test: Creating a User with default values
        [Fact]
        public void CanCreateUserWithDefaultValues()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.Null(user.Username); // Verify that the Username property is null by default
            Assert.Null(user.Email); // Verify that the Email property is null by default
            Assert.Null(user.Password); // Verify that the Password property is null by default
        }

        // Test: Setting an empty password
        [Fact]
        public void PasswordCanBeEmpty()
        {
            // Arrange
            var user = new User();

            // Act
            user.Password = "";

            // Assert
            Assert.Equal("", user.Password); // Verify that an empty password can be set
        }

        // Test: Setting an empty email
        [Fact]
        public void EmailCanBeEmpty()
        {
            // Arrange
            var user = new User();

            // Act
            user.Email = "";

            // Assert
            Assert.Equal("", user.Email); // Verify that an empty email can be set
        }
    }
}
