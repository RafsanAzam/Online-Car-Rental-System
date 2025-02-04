﻿using Microsoft.EntityFrameworkCore;
using Online_Car_Rental_System.Data;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations.Include(r => r.Car).ToList();

        }

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.Include(r => r.Car).FirstOrDefault(r => r.ReservationId == id);
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
            if(reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }

        public Reservation GetMostRecentUncompletedReservation()
        {
            return _context.Reservations
                .Where(r => r.Status == "Unconfirmed")
                .OrderByDescending(r => r.ReservationId)
                .FirstOrDefault();
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
