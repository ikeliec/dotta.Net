using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace dotta.Net.Demo;

[ApiController]
[Route("[controller]")]
public class FaceController : ControllerBase
{
    private readonly Dotta _dotta;

    public FaceController(Dotta dotta) => _dotta = dotta;

    /// <summary>
    /// Get facial attributes on photo
    /// </summary>
    /// <param name="command"></param>
    [Produces("application/json")]
    [ProducesResponseType(typeof(DottaResponse<FaceAttributesResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [HttpPost("Attributes")]
    public async Task<IActionResult> Attributes([FromForm] FaceAttributesCommand command)
    {
        var response = await _dotta.FaceAttributes(command.Photo);
        return Ok(response);
    }

    /// <summary>
    /// Detect face from photo
    /// </summary>
    /// <param name="command"></param>
    [Produces("application/json")]
    [ProducesResponseType(typeof(DottaResponse<FaceDetectResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [HttpPost("Detect")]
    public async Task<IActionResult> Detect([FromForm] FaceDetectCommand command)
    {
        var response = await _dotta.FaceDetection(command.Photo);
        return Ok(response);
    }

    /// <summary>
    /// Run comparison on two photos
    /// </summary>
    /// <param name="command"></param>
    [Produces("application/json")]
    [ProducesResponseType(typeof(DottaResponse<FaceMatchResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [HttpPost("Match")]
    public async Task<IActionResult> Match([FromForm] FaceMatchCommand command)
    {
        var response = await _dotta.FaceMatch(command.PhotoOne, command.PhotoTwo);
        return Ok(response);
    }

    /// <summary>
    /// Run active liveness analysis on collection of photos
    /// </summary>
    /// <param name="command"></param>
    [Produces("application/json")]
    [ProducesResponseType(typeof(DottaResponse<FaceActiveLivenessResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [HttpPost("ActiveLiveness")]
    public async Task<IActionResult> ActiveLiveness([FromForm] FaceActiveLivenessCommand command)
    {
        var response = await _dotta.ActiveLivenessCheck(command.Photos);
        return Ok(response);
    }
}
