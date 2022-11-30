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
    
    ///<summary>
    /// Get place owner data
    /// </summary>
    [HttpGet("getPlaceOwnerData/{userId:int}")]
    [Authorize(Roles = RoleType.Moderator + "," + RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Client> GetPlaceOwner(int userId)
    {
        var placeOwner = _placeOwnerService.GetPlaceOwnerByUserId(userId);
        return Ok(placeOwner);
    }

    /// <summary>
    /// Update place owner data
    /// </summary>
    [HttpPatch("updatePlaceOwnerAccount/{userId:int}")]
    [Authorize(Roles = RoleType.Moderator + "," + RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Client> UpdateClient(UpdatePlaceOwnerRequest request, int userId)
    {
        var placeOwner = _placeOwnerService.UpdatePlaceOwnerByUserId(request, userId);
        return Ok(placeOwner);
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
    
    /// <summary>
    /// Delete place by id. Available for: PlaceOwner
    /// </summary>
    [HttpDelete("deletePlace/{placeId:int}")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult DeletePlace(int placeId)
    {
        _placeService.DeletePlace(placeId);
        return NoContent();
    }
    
    /// <summary>
    /// Update place by id. Available for: PlaceOwner
    /// </summary>
    [HttpPatch("updatePlace/{placeId:int}")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Place> UpdatePlace(int placeId, UpdatePlaceRequest request)
    {
        var response = _placeService.UpdatePlace(placeId, request);
        return Ok(response);
    }
    
    /// <summary>
    /// Get all places by userId. Available for: PlaceOwner
    /// </summary>
    [HttpGet("getPlaces/{userId:int}")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Place>> GetAllPlacesOwnedByPlaceOwner(int userId)
    {
        var response = _placeService.GetAllPlacesOwnedByPlaceOwner(userId);
        return Ok(response);
    }
    
}