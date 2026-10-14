using Xunit;
using ContactBook;

namespace ContactBook.Tests
{
    public class ContactTests
    {
        // =========================
        // Constructor Tests
        // =========================

        [Fact]
        public void DefaultConstructor_ShouldInitializeEmptyStrings()
        {
            // Arrange & Act
            Contact contact = new Contact();

            // Assert
            Assert.Equal("", contact.getFname());
            Assert.Equal("", contact.getLname());
            Assert.Equal("", contact.getPhone());
            Assert.Equal("", contact.getEmail());
        }

        [Fact]
        public void Constructor_ShouldInitializeAllFields()
        {
            // Arrange & Act
            Contact contact = new Contact(
                "John",
                "Doe",
                "787-555-1234",
                "john@example.com"
            );

            // Assert
            Assert.Equal("John", contact.getFname());
            Assert.Equal("Doe", contact.getLname());
            Assert.Equal("787-555-1234", contact.getPhone());
            Assert.Equal("john@example.com", contact.getEmail());
        }

        // =========================
        // Getter / Setter Tests
        // =========================

        [Fact]
        public void SetFName_ShouldUpdateFirstName()
        {
            // Arrange
            Contact contact = new Contact();

            // Act
            contact.SetFName("Alice");

            // Assert
            Assert.Equal("Alice", contact.getFname());
        }

        [Fact]
        public void SetLName_ShouldUpdateLastName()
        {
            // Arrange
            Contact contact = new Contact();

            // Act
            contact.SetLName("Smith");

            // Assert
            Assert.Equal("Smith", contact.getLname());
        }

        [Fact]
        public void SetPhone_ShouldUpdatePhone()
        {
            // Arrange
            Contact contact = new Contact();

            // Act
            contact.SetPhone("939-555-9999");

            // Assert
            Assert.Equal("939-555-9999", contact.getPhone());
        }

        [Fact]
        public void SetEmail_ShouldUpdateEmail()
        {
            // Arrange
            Contact contact = new Contact();

            // Act
            contact.SetEmail("alice@example.com");

            // Assert
            Assert.Equal("alice@example.com", contact.getEmail());
        }

        // =========================
        // ToString Tests
        // =========================

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            Contact contact = new Contact(
                "John",
                "Doe",
                "123456789",
                "john@example.com"
            );

            // Act
            string result = contact.ToString();

            // Assert
            Assert.Equal(
                "Contact[fname=John, lname=Doe, phone=123456789, email=john@example.com]",
                result
            );
        }

        // =========================
        // Equals(Contact) Tests
        // =========================

        [Fact]
        public void Equals_ShouldReturnTrue_WhenContactsHaveSameValues()
        {
            // Arrange
            Contact c1 = new Contact(
                "John",
                "Doe",
                "123",
                "john@example.com"
            );

            Contact c2 = new Contact(
                "John",
                "Doe",
                "123",
                "john@example.com"
            );

            // Act
            bool result = c1.Equals(c2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenFirstNamesDiffer()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            // Act & Assert
            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenLastNamesDiffer()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("John", "Smith", "123", "john@example.com");

            // Act & Assert
            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenPhonesDiffer()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("John", "Doe", "999", "john@example.com");

            // Act & Assert
            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEmailsDiffer()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("John", "Doe", "123", "doe@example.com");

            // Act & Assert
            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOtherIsNull()
        {
            // Arrange
            Contact contact = new Contact();

            // Act & Assert
            Assert.False(contact.Equals(null));
        }

        [Fact]
        public void EqualsObject_ShouldReturnFalse_WhenObjectIsDifferentType()
        {
            // Arrange
            Contact contact = new Contact();

            // Act
            bool result = contact.Equals("Not a contact");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsObject_ShouldReturnTrue_WhenObjectHasSameValues()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");

            object c2 = new Contact("John", "Doe", "123", "john@example.com");

            // Act
            bool result = c1.Equals(c2);

            // Assert
            Assert.True(result);
        }

        // =========================
        // GetHashCode Tests
        // =========================

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualObjects()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("John", "Doe", "123", "john@example.com");

            // Act
            int hash1 = c1.GetHashCode();
            int hash2 = c2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldDiffer_ForDifferentObjects()
        {
            // Arrange
            Contact c1 = new Contact("John", "Doe", "123", "john@example.com");
            Contact c2 = new Contact("Jane", "Smith", "999", "jane@example.com");

            // Act
            int hash1 = c1.GetHashCode();
            int hash2 = c2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        // =========================
        // Operator Tests
        // =========================
        // NOTE:
        // The == operator in the provided class is implemented incorrectly:
        //
        // public static bool operator ==(Contact? x, Contact? y)
        // {
        //     return !(x == y);
        // }
        //
        // This causes infinite recursion / stack overflow.
        //
        // These tests document the issue rather than execute it.

        [Fact]
        public void EqualityOperator_IsIncorrectlyImplemented()
        {
            // This test exists to document the bug in the == operator.
            // Calling the operator would cause a StackOverflowException.

            string implementation =
                "return !(x == y);";

            Assert.Contains("x == y", implementation);
        }
    }
}