﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [HttpPost("getConfirmedReservationsForOneDay")]
    [Authorize(Roles = RoleType.PlaceOwner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Reservation>> GetAllConfirmedReservationsForSpecificdLobbyAndDay(DateTime date, int placeId)
    {
        var reservations = _reservationService.GetAllConfirmedReservationsForSpecificdLobbyAndDay(date, placeId);
        return Ok(reservations);
    }
}