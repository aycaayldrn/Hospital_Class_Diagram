namespace Tests;
using Hospital_System.Models;

public class AppointmentTests
{
    [Test]
    public void Trying_to_create_Appointment()
    {
        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,8,30,0);
        Appointment a = new Appointment(date);
        Assert.That(a.Date, Is.EqualTo(date));
        Appointment.removeAppointment(a);
    }
    
    [Test]
    public void Trying_to_create_List_of_Appointments_and_SetAppointments()
    {
        List<Appointment> la = new List<Appointment>{new ( new DateTime(2005)), new ( new DateTime(2006)), new ( new DateTime(2007))};
        
        Appointment.SetAppointments(la);
        
        Assert.That(Appointment.GetAppointments(), Is.EqualTo(la));
        
        Appointment.SetAppointments(new List<Appointment>());
    }
    
    [Test]
    public void Trying_to_create_same_Appointment_throws_InvalidOperationException()
    {
        DateTime date = new DateTime(2005, 3, 12,8,30,0);
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
}