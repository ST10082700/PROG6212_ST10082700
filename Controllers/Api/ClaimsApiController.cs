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
        public ActionResult<IEnumerable<ClaimModel>> GetAllClaims()
        {
            var claims = _claimService.GetAllClaims();
            return Ok(claims);
        }

        [HttpGet("lecturer/{username}")]
        public ActionResult<IEnumerable<ClaimModel>> GetLecturerClaims(string username)
        {
            var claims = _claimService.GetClaimsByLecturer(username);
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public ActionResult<ClaimModel> GetClaimById(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }

        [HttpPost("approve/{id}")]
        public ActionResult ApproveClaim(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
                return NotFound();

            if (claim.Status == "Pending")
            {
                claim.Status = "Accepted";
                _claimService.UpdateClaim(claim);
                return Ok(new { message = "Claim approved successfully" });
            }
            return BadRequest(new { message = "Claim cannot be approved" });
        }

        [HttpPost("reject/{id}")]
        public ActionResult RejectClaim(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
                return NotFound();

            if (claim.Status == "Pending")
            {
                claim.Status = "Rejected";
                _claimService.UpdateClaim(claim);
                return Ok(new { message = "Claim rejected successfully" });
            }
            return BadRequest(new { message = "Claim cannot be rejected" });
        }
    }
}