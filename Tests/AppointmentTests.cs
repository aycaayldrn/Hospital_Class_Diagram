namespace Tests;
using Hospital_System.Models;

public class AppointmentTests
{
    [Test]
    public void Trying_to_create_Appointment()
    {
        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,23,59,59);
        Appointment a = new Appointment(date);
        Assert.That(a.Date, Is.EqualTo(date));
        Appointment.removeAppointment(a);
    }
    
    [Test]
    public void Trying_to_create_Appointment_with_date_earlier_than_today()
    {
        try
        {
            DateTime date = new DateTime(2005, 3, 12,8,30,0);
            Appointment a = new Appointment(date);
            Assert.Fail("Should throw ArgumentException");
        }catch(ArgumentException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_List_of_Appointments_and_SetAppointments()
    {
        Appointment.SetAppointments(new List<Appointment>());
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0)), new ( new DateTime(3002,3, 12,8,30,0)), new ( new DateTime(3001, 3, 12,8,30,0))};
     
    
        Assert.That(Appointment.GetAppointments(), Is.EquivalentTo(la));
    }
    
    [Test]
    public void Trying_to_create_same_Appointment_throws_InvalidOperationException()
    {
        Appointment.SetAppointments(new List<Appointment>());
        DateTime date = new DateTime(3004, 3, 12,8,30,0);
        Appointment a = new Appointment(date);
        try
        {
            Appointment a2 = new Appointment(date);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Appointment_InvalidOperationException_excepted()
    {
        try
        {
            Appointment.removeAppointment(new Appointment());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_List_of_Appointments_and_save_them_to_file()
    {
        Appointment.SetAppointments(new List<Appointment>());
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0)), new ( new DateTime(3002,3, 12,8,30,0)), new ( new DateTime(3001, 3, 12,8,30,0))};
        
        SerializeToFIle.saveAll();
        
        Appointment.SetAppointments(new List<Appointment>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Appointment.GetAppointments(), Is.EqualTo(la));
    }
}