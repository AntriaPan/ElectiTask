using Microsoft.AspNetCore.Mvc.Testing;
using ElectiTask.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace ElectiTask.Tests.Integration
{
    public class BooksControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public BooksControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        // This test verifies that the API endpoint to get a list of users returns a non-empty response.
        [Fact]
        public async Task UserGet_ReturnsListOfUsers()
        {
            // Act - Send a GET request to the user endpoint
            var response = await _httpClient.GetAsync("/api/users");

            // Assert
            response.EnsureSuccessStatusCode(); // Verify that the request was successful
            var content = await response.Content.ReadAsStringAsync();
            Assert.True(!string.IsNullOrEmpty(content)); // Verify that the response content is not empty
        }

        // This test verifies that the API endpoint to get a list of books returns a non-empty response.
        [Fact]
        public async Task BookGet_ReturnsListOfBooks()
        {
            // Act - Send a GET request to the book endpoint
            var response = await _httpClient.GetAsync("/api/books");

            // Assert
            response.EnsureSuccessStatusCode(); // Verify that the request was successful
            var content = await response.Content.ReadAsStringAsync();
            Assert.True(!string.IsNullOrEmpty(content)); // Verify that the response content is not empty
        }

        // This test verifies the process of creating a new book, updating it, and then deleting it.
        [Fact]
        public async Task UserCanCreateBookAndUpdateForUser()
        {
            // Arrange - Create a new book object
            var newBook = new Book
            {
                Title = "New Book",
                Author = "John Doe",
                PublishedYear = "2023",
                ISBN = "1234567890123"
            };

            // Serialize the book object to JSON and create content for the request
            var bookContent = new StringContent(JsonConvert.SerializeObject(newBook), Encoding.UTF8, "application/json");

            // Act - Create the book by sending a POST request
            var createBookResponse = await _httpClient.PostAsync("/api/books", bookContent);
            createBookResponse.EnsureSuccessStatusCode();
            var responseBody = await createBookResponse.Content.ReadAsStringAsync();
            var createdBook = JsonConvert.DeserializeObject<Book>(responseBody);

            // TODO: Print or log createdBook to verify its content

            // Assert - Verify the created book's properties
            Assert.Equal("New Book", createdBook.Title);
            Assert.Equal("John Doe", createdBook.Author);
            Assert.Equal("2023", createdBook.PublishedYear);
            Assert.Equal("1234567890123", createdBook.ISBN);

            // Act - Update the created book
            var updatedBookContent = new StringContent(JsonConvert.SerializeObject(createdBook), Encoding.UTF8, "application/json");
            var updateBookResponse = await _httpClient.PutAsync($"/api/books/{createdBook.Id}", updatedBookContent);
            updateBookResponse.EnsureSuccessStatusCode();

            // Act - Get the updated book
            var getUpdatedBookResponse = await _httpClient.GetAsync($"/api/books/{createdBook.Id}");
            getUpdatedBookResponse.EnsureSuccessStatusCode();
            var getUpdatedBookResponseBody = await getUpdatedBookResponse.Content.ReadAsStringAsync();
            var retrievedUpdatedBook = JsonConvert.DeserializeObject<Book>(getUpdatedBookResponseBody);

            // Assert - Verify the updated book's properties
            Assert.Equal(createdBook.Title, retrievedUpdatedBook.Title);
            Assert.Equal(createdBook.Author, retrievedUpdatedBook.Author);
            Assert.Equal(createdBook.PublishedYear, retrievedUpdatedBook.PublishedYear);
            Assert.Equal(createdBook.ISBN, retrievedUpdatedBook.ISBN);

            // Act - Delete the updated book
            var deleteBookResponse = await _httpClient.DeleteAsync($"/api/books/{createdBook.Id}");
            deleteBookResponse.EnsureSuccessStatusCode();

            // Act - Verify that the book has been deleted by trying to get it again
            var getDeletedBookResponse = await _httpClient.GetAsync($"/api/books/{createdBook.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getDeletedBookResponse.StatusCode);
        }

        // This test verifies that creating a book with invalid data results in a BadRequest response.
        [Fact]
        public async Task CannotCreateBookWithInvalidData()
        {
            // Arrange - Create an invalid book object (missing required fields)
            var invalidBook = new Book
            {
                // Missing required fields like Title, Author, etc.
            };

            // Serialize the invalid book object to JSON and create content for the request
            var bookContent = new StringContent(JsonConvert.SerializeObject(invalidBook), Encoding.UTF8, "application/json");

            // Act - Send a POST request to create the invalid book
            var createBookResponse = await _httpClient.PostAsync("/api/books", bookContent);

            // Assert - Verify that the response status code is BadRequest
            Assert.Equal(HttpStatusCode.BadRequest, createBookResponse.StatusCode);
        }

        // This test verifies that the API endpoint to get a list of books returns a non-empty list.
        [Fact]
        public async Task UserCanGetListOfBooks()
        {
            // Act - Send a GET request to the book endpoint
            var getBooksResponse = await _httpClient.GetAsync("/api/books");
            var responseBody = await getBooksResponse.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(responseBody);

            // Assert
            getBooksResponse.EnsureSuccessStatusCode(); // Verify that the request was successful
            Assert.NotEmpty(books); // Verify that the list of books is not empty
        }

    }
}


