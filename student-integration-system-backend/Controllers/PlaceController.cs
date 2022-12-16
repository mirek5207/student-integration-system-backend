using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private readonly IPlaceService _placeService;

    public PlaceController(IPlaceService placeService)
    {
        _placeService = placeService;
    }
    
    /// <summary>
    /// Get all places
    /// </summary>
    [HttpGet("getAllPlaces")]
    [Authorize(Roles = RoleType.Client + "," + RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Place>> GetAllPlaces()
    {
        var response = _placeService.GetAllPlaces();
        return Ok(response);
    }
}