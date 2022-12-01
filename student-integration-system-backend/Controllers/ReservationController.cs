using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.ReservationService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    /// <summary>
    /// Creates new reservation
    /// </summary>
    [HttpPost("createReservation")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Reservation> CreateReservation(CreateReservationRequest request)
    {
        var reservation = _reservationService.CreateReservation(request);
        return Ok(reservation);
    }
    
    /// <summary>
    /// Get all confirmed reservation for specific lobby and day.
    /// </summary>
    [HttpGet("getConfirmedReservationsForOneDay")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Reservation>> GetAllConfirmedReservationsForSpecificPlaceAndDay(DateTime date, int placeId)
    {
        var reservations = _reservationService.GetAllConfirmedReservationsForSpecificPlaceAndDay(date, placeId);
        return Ok(reservations);
    }
    
    /// <summary>
    /// Get all sent reservation for specific lobby.
    /// </summary>
    [HttpGet("getSentReservationsForPlace")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Reservation>> GetAllSentReservationsForPlace(int placeId)
    {
        var reservations = _reservationService.GetAllSentReservationsForPlace(placeId);
        return Ok(reservations);
    }
    
    /// <summary>
    /// Update reservations status to declined.
    /// </summary>
    [HttpPatch("declineReservation")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> DeclineReservation(int reservationId)
    {
        var message = _reservationService.DeclinedReservation(reservationId);
        return Ok(message);
    }
}