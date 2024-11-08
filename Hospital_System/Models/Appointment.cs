﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Appointment
    {
        
        private static List<Appointment> _appointmentList = new List<Appointment>();
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Date cannot be null");
                }
                _date = value;
            }
        }

        public Appointment(DateTime date) {
            Date = date;
            addAppointment(this);
        }
        public Appointment(){}
    
        
        private static void addAppointment(Appointment appointment)
        {
            if (appointment== null)
            {
                throw new ArgumentException("Apointment cannot be null");
            }
           

            if (_appointmentList.Exists(a=>a.Equals(appointment)))
            {
                throw new InvalidOperationException("Appointment already added");
            }
            _appointmentList.Add(appointment);
        }


        public static void removeAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentException("Appointment cannot be null");
            }

            if (!_appointmentList.Contains(appointment))
            {
                throw new InvalidOperationException("appointment not found!");
            }
            _appointmentList.Remove(appointment);
        }

        public static List<Appointment> GetAppointments()
        {
            return new List<Appointment>(_appointmentList.AsReadOnly());
        }
        
        public static void SetAppointments(List<Appointment> containerAppointments)
        {
            _appointmentList = containerAppointments.Count == 0 ? new List<Appointment>(): containerAppointments;
        }

        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Appointment))
            {
                return false;
            }

            Appointment a = (Appointment)obj;

            return _date == a._date;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode();
        }

        public override string ToString()
        {
            return "date: " + _date;
        }
    }
    
    internal interface IAppointment
    {
        DateTime Date { get; set; }
        DateTime Time { get; set; }
    }
    
    
    
}
