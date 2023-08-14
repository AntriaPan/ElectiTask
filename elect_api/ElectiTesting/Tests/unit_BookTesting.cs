using ElectiTask.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ElectiTask.Tests
{
    public class BookTests
    {
        // Test: Creating a new Book instance with specific values
        [Fact]
        public void CanCreateBook()
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Book",
                Author = "John Doe",
                PublishedYear = "2022",
                ISBN = "1234567890"
            };

            // Act & Assert
            Assert.Equal("Sample Book", book.Title);
            Assert.Equal("John Doe", book.Author);
            Assert.Equal("2022", book.PublishedYear);
            Assert.Equal("1234567890", book.ISBN);
        }

        // Test: Creating a new Book instance with null values
        [Fact]
        public void CanCreateBookWithNullValues()
        {
            // Arrange
            var book = new Book();

            // Act & Assert
            Assert.Null(book.Id);
            Assert.Null(book.Title);
            Assert.Null(book.Author);
            Assert.Null(book.PublishedYear);
            Assert.Null(book.ISBN);
        }

        // Test: Setting and getting the Title property of a Book
        [Fact]
        public void CanSetBookTitle()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = "New Title";

            // Assert
            Assert.Equal("New Title", book.Title);
        }

        // Test: Trying to set an empty Title for a Book
        [Fact]
        public void CannotSetEmptyTitle()
        {
            // Arrange
            var book = new Book();

            // Act & Assert
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            var isValid = Validator.TryValidateProperty("", context, validationResults);

            // Assert
            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Title cannot be empty.", validationResults[0].ErrorMessage);
        }

        // Test: Ensuring PublishedYear property must have exactly four digits
        [Fact]
        public void PublishedYearMustHaveExactlyFourDigits()
        {
            // Arrange
            var book = new Book();

            // Act
            book.PublishedYear = "202";

            // Assert
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(book) { MemberName = nameof(Book.PublishedYear) };
            var isValid = Validator.TryValidateProperty(book.PublishedYear, context, validationResults);

            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Published year must be exactly 4 digits.", validationResults[0].ErrorMessage);
        }

        // Test: Ensuring ISBN property must have exactly thirteen digits
        [Fact]
        public void ISBNMustHaveExactlyThirteenDigits()
        {
            // Arrange
            var book = new Book();

            // Act
            book.ISBN = "123456789012";

            // Assert
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(book) { MemberName = nameof(Book.ISBN) };
            var isValid = Validator.TryValidateProperty(book.ISBN, context, validationResults);

            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("ISBN must be exactly 13 digits.", validationResults[0].ErrorMessage);
        }

        // Test: Updating the title of a Book
        [Fact]
        public void CanUpdateBookTitle()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = "Initial Title";
            book.Title = "Updated Title";

            // Assert
            Assert.Equal("Updated Title", book.Title);
        }

        // Test: Trying to update a Book with an empty title
        [Fact]
        public void CannotUpdateBookWithEmptyTitle()
        {
            // Arrange
            var book = new Book();

            // Act & Assert
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            Validator.TryValidateProperty("", context, validationResults);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Title cannot be empty.");
        }

        // Test: Trying to update a Book with an invalid PublishedYear
        [Fact]
        public void CannotUpdateBookWithInvalidPublishedYear()
        {
            // Arrange
            var book = new Book();

            // Act & Assert
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(book) { MemberName = nameof(Book.PublishedYear) };
            Validator.TryValidateProperty("202", context, validationResults);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Published year must be exactly 4 digits.");
        }
    }
}
