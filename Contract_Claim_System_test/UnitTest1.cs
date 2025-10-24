using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.services;
using Microsoft.AspNetCore.Http;

namespace Contract_Claim_System_test
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddAttachmentAsync_Should_Save_File_And_Return_Attachment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ClaimService(context);

            var mockFile = new FormFile(
                new MemoryStream(new byte[256]),
                0, 256, "file", "test.pdf"
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            // Act
            var attachment = await service.AddAttachmentAsync(1, mockFile, "Lecturer A");

            // Assert
            Assert.NotNull(attachment);
            Assert.Equal("Lecturer A", attachment.UploadedBy);
            Assert.Contains(".pdf", attachment.FilePath);
        }

        private ApplicationDbContext GetInMemoryDbContext()
        {
            throw new NotImplementedException();
        }
    }
}