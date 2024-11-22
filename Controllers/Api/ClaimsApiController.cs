using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Models.DTOs;
using PROG6212___CMCS___ST10082700.Services;

namespace PROG6212___CMCS___ST10082700.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsApiController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimsApiController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimModel>>> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("lecturer/{username}")]
        public async Task<ActionResult<IEnumerable<ClaimModel>>> GetLecturerClaims(string username)
        {
            var claims = await _claimService.GetClaimsByLecturerAsync(username);
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimModel>> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }

        [HttpPost("approve/{id}")]
        public async Task<ActionResult> ApproveClaim(int id)
        {
            var result = await _claimService.ApproveClaimAsync(id);
            if (result)
                return Ok(new { message = "Claim approved successfully" });
            return BadRequest(new { message = "Claim cannot be approved" });
        }

        [HttpPost("reject/{id}")]
        public async Task<ActionResult> RejectClaim(int id)
        {
            var result = await _claimService.RejectClaimAsync(id);
            if (result)
                return Ok(new { message = "Claim rejected successfully" });
            return BadRequest(new { message = "Claim cannot be rejected" });
        }
    }
}