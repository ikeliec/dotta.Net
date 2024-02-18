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
        var faceAttributes = await _dotta.FaceAttributes(command.Photo);
        return Ok(faceAttributes);
    }
}
