using System.Threading.Tasks;
using Xunit;
using Moq;
using PROG6212___CMCS___ST10082700.Services;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Data;

public class ClaimServiceTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly ClaimService _claimService;

    public ClaimServiceTests()
    {
        _mockContext = new Mock<ApplicationDbContext>();
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _claimService = new ClaimService(_mockContext.Object, _mockEnvironment.Object);
    }

    [Fact]
    public async Task AddClaimAsync_Should_Add_Claim()
    {
        // Arrange
        var claim = new ClaimModel
        {
            ClaimName = "Test Claim",
            HoursWorked = 5,
            HourlyRate = 100,
            LecturerUsername = "lecturer@test.com"
        };

        // Act
        await _claimService.AddClaimAsync(claim, null);

        // Assert
        _mockContext.Verify(m => m.Claims.Add(claim), Times.Once);
        //_mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ApproveClaimAsync_Should_Update_Status()
    {
        // Arrange
        var claimId = 1;
        var claim = new ClaimModel { Id = claimId, Status = "Pending" };
        _mockContext.Setup(m => m.Claims.FindAsync(claimId)).ReturnsAsync(claim);

        // Act
        var result = await _claimService.ApproveClaimAsync(claimId, "approver");

        // Assert
        Assert.True(result);
        Assert.Equal("Approved", claim.Status);
    }
}
