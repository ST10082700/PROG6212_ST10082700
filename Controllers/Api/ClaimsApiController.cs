using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Models.DTOs;
using PROG6212___CMCS___ST10082700.Services;
using Microsoft.AspNetCore.Http;

namespace PROG6212___CMCS___ST10082700.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsApiController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsApiController(IClaimService claimService, IHttpContextAccessor httpContextAccessor)
        {
            _claimService = claimService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("lecturer/{username}")]
        public async Task<IActionResult> GetLecturerClaims(string username)
        {
            var claims = await _claimService.GetClaimsByLecturerAsync(username);
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            // Get the username of the approver from the session (or another source)
            var approverUsername = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(approverUsername))
            {
                return Unauthorized(new { message = "Approver username is missing or invalid" });
            }

            var result = await _claimService.ApproveClaimAsync(id, approverUsername);
            if (result)
                return Ok(new { message = "Claim approved successfully" });

            return BadRequest(new { message = "Claim cannot be approved" });
        }

        [HttpPost("reject/{id}")]
        public async Task<IActionResult> RejectClaim(int id)
        {
            // Get the username of the approver from the session (or another source)
            var approverUsername = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(approverUsername))
            {
                return Unauthorized(new { message = "Approver username is missing or invalid" });
            }

            var result = await _claimService.RejectClaimAsync(id, approverUsername);
            if (result)
                return Ok(new { message = "Claim rejected successfully" });

            return BadRequest(new { message = "Claim cannot be rejected" });
        }
    }
}
