using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.LobbyService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Services.ReservationService;

public class ReservationServiceImpl : IReservationService
{
    private readonly IPlaceService _placeService;
    private readonly ILobbyService _lobbyService;
    private readonly AppDbContext _dbContext;

    public ReservationServiceImpl(IPlaceService placeService, ILobbyService lobbyService, AppDbContext dbContext)
    {
        _placeService = placeService;
        _lobbyService = lobbyService;
        _dbContext = dbContext;
    }
    
    public Reservation CreateReservation(CreateReservationRequest request)
    {
        var reservation = new Reservation()
        {
            NumberOfGuests = request.NumberOfGuests,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PhoneNumber = request.PhoneNumber,
            Status = ReservationStatus.Sent,
            Place = _placeService.GetPlaceById(request.PlaceId),
            Lobby = _lobbyService.GetLobbyById(request.LobbyId)
        };
        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
        return reservation;
    }

    public IEnumerable<ReservationResponse> GetAllConfirmedReservationsForSpecificPlaceAndDay(DateTime date, int placeId)
    {
        var reservations = _dbContext.Reservations.Where(r => r.Status == ReservationStatus.Confirmed
                                                              && (r.StartDate.Date <= date.Date &&
                                                                  r.EndDate.Date >= date.Date)
                                                              && r.PlaceId == placeId)
            .Include(r => r.Lobby.LobbyOwner.Client)
            .Select(r => MapReservationToReservationResponse(r))
            .ToList();
        if (reservations.Count == 0)
            throw new NotFoundException("Reservations for this day not found.");

        return reservations;
    }

    public IEnumerable<ReservationResponse> GetAllSentReservationsForPlace(int placeId)
    {
        var reservations = _dbContext.Reservations.Where(r => r.Status == ReservationStatus.Sent
                                                              && r.PlaceId == placeId)
            .Include(r => r.Lobby.LobbyOwner.Client)
            .Select(r => MapReservationToReservationResponse(r))
            .ToList();
        if (reservations.Count == 0)
            throw new NotFoundException("Reservations for this day not found.");
        
        return reservations;
    }

    public Reservation GetReservationById(int reservationId)
    {
        var reservation = _dbContext.Reservations.FirstOrDefault(r => r.Id == reservationId);

        if (reservation is null)
        {
            throw new NotFoundException("Reservation not found");
        }

        return reservation;
    }
    public Reservation? GetReservationByLobbyId(int lobbyId)
    {
        var reservation = _dbContext.Reservations.FirstOrDefault(r => r.LobbyId == lobbyId);
        return reservation;
    }

    public string DeleteReservationByLobbyId(int lobbyId)
    {
        var reservation = GetReservationByLobbyId(lobbyId);
        if (reservation == null) return "There is no reservation for this lobby";
        _dbContext.Reservations.Remove(reservation);
        _dbContext.SaveChanges();
        return "Reservation Deleted";
    }
    
    public string DeclinedReservation(int reservationId)
    {
        var reservation = GetReservationById(reservationId);
        reservation.Status = ReservationStatus.Declined;
        _dbContext.SaveChanges();
        
        return "Reservation declined";
    }

    public string ConfirmReservation(int reservationId)
    {
        var reservation = GetReservationById(reservationId);
        if (reservation.Status == ReservationStatus.Declined)
            throw new ForbiddenException("Declined reservation can't be confirmed.");
        reservation.Status = ReservationStatus.Confirmed;
        _dbContext.SaveChanges();
        
        return "Reservation confirmed";
    }

    public string DeleteReservation(int reservationId)
    {
        var reservation = GetReservationById(reservationId);
        _dbContext.Remove(reservation);
        _dbContext.SaveChanges();
        
        return "Reservation deleted";
    }

    public Reservation UpdateReservation(UpdateReservationRequest request, int reservationId)
    {
        var reservation = GetReservationById(reservationId);
        if (reservation.Status != ReservationStatus.Sent)
            throw new ForbiddenException("You can only update sent reservation.");
        reservation.StartDate = request.StartDate;
        reservation.EndDate = request.EndDate;
        reservation.PhoneNumber = request.PhoneNumber;
        reservation.NumberOfGuests = request.NumberOfGuests;
        _dbContext.SaveChanges();

        return reservation;
    }

    public static ReservationResponse MapReservationToReservationResponse(Reservation reservation)
    {
        var response = new ReservationResponse
        {
            Id = reservation.Id,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate,
            NumberOfGuests = reservation.NumberOfGuests,
            PhoneNumber = reservation.PhoneNumber,
            Status = reservation.Status,
            LobbyOwnerFullName = reservation.Lobby.LobbyOwner.Client.FirstName + " " +
                                 reservation.Lobby.LobbyOwner.Client.SurName
        };
        return response;
    }
}