using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.PlaceOwnerService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceOwnerController : ControllerBase
{
    private readonly IPlaceOwnerService _placeOwnerService;
    private readonly IPlaceService _placeService;


    public PlaceOwnerController(IPlaceOwnerService placeOwnerService, IPlaceService placeService)
    {
        _placeOwnerService = placeOwnerService;
        _placeService = placeService;
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
    /// <summary>
    /// Create new place. Available for: PlaceOwner
    /// </summary>
    [HttpPost("createPlace")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Place> CreatePlace(CreatePlaceRequest request)
    {
        var response = _placeService.CreatePlace(request);
        return Ok(response);
    }
    
    [HttpDelete("deletePlace/{placeId:int}")]
    public ActionResult DeletePlace(int placeId)
    {
        _placeService.DeletePlace(placeId);
        return NoContent();
    }
    
}