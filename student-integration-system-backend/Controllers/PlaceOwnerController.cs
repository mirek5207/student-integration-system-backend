using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.PlaceOwnerService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceOwnerController : ControllerBase
{
    private readonly IPlaceOwnerService _placeOwnerService;


    public PlaceOwnerController(IPlaceOwnerService placeOwnerService)
    {
        _placeOwnerService = placeOwnerService;
    }

    /// <summary>
    /// Creates new place owner
    /// </summary>
    [HttpPost("createAccount")]
    public ActionResult<AuthenticationResponse> CreatePlaceOwner(PlaceOwnerSignUpRequest request)
    {
        var response =  _placeOwnerService.RegisterPlaceOwner(request);
        return Ok(response);
    }
}