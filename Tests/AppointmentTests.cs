namespace Tests;
using Hospital_System.Models;

//dotnet nuget locals all --clear

public class AppointmentTests
{
    [Test]
    public void Trying_to_create_Appointment()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,23,59,59);
        Appointment.AppointmentType at = Appointment.AppointmentType.FollowUp;
        object dd = new object();
        Appointment a = new Appointment(date, at, dd);
        if (a.Date == date && a.Type == at && a.AssignedDoctor == dd)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Appointment.removeAppointment(a);
    }
    
    [Test]
    public void Trying_to_create_Appointment_with_date_earlier_than_today_should_throw_ArgumentException()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        try
        {
            DateTime date = new DateTime(2005, 3, 12,8,30,0);
            Appointment a = new Appointment(date, Appointment.AppointmentType.FollowUp, new object());
            Assert.Fail("Should throw ArgumentException");
        }catch(ArgumentException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Appointment_with_type_Surgery_with_doctor_that_isnt_Surgeon_should_throw_InvalidOperationException()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        try
        {
            Appointment a = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.Surgery, new object());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_List_of_Appointments_and_SetAppointments()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
                                                     new ( new DateTime(3002,3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
                                                     new ( new DateTime(3001, 3, 12,8,30,0) , Appointment.AppointmentType.FollowUp, new object())};
     
    
        Assert.That(Appointment.GetAppointments(), Is.EquivalentTo(la));
    }
    
    [Test]
    public void Trying_to_create_same_Appointment_throws_InvalidOperationException()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        DateTime date = new DateTime(3004, 3, 12,8,30,0);
        Appointment a = new Appointment(date , Appointment.AppointmentType.FollowUp, new object());
        try
        {
            Appointment a2 = new Appointment(date , Appointment.AppointmentType.FollowUp, new object());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Appointment.removeAppointment(a);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Appointment_InvalidOperationException_excepted()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
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
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }
        
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()), 
            new ( new DateTime(3002,3, 12,8,30,0), Appointment.AppointmentType.Consultation, new object()), 
            new ( new DateTime(3001, 3, 12,8,30,0) , Appointment.AppointmentType.FollowUp, new object())};
        
        SerializeToFIle.saveAll();
        foreach (var o in la)
        {
            Appointment.removeAppointment(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Appointment.GetAppointments(), Is.EqualTo(la));
    }
    
    // [Test]
    // public void Trying_to_assign_Bill_to_Patient_and_change_Department()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     Patient patient2 = new Patient(2,"Test1",new DateTime(2005));
    //     Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
    //     appointment.assignPatient(patient);
    //     if (!appointment.Patient.Equals(patient))
    //     {
    //         Assert.Fail();
    //     }
    //     appointment.changeAppointment(patient2);
    //
    //     if (patient2.Equals(appointment.Patient))
    //     {
    //         Patient.RemovePatient(patient2);
    //         Patient.RemovePatient(patient);
    //         Appointment.removeAppointment(appointment);
    //         Assert.Pass();
    //     }else{
    //         Assert.Fail();
    //     }
    // }
    //
    // [Test]
    // public void Trying_to_assign_Bill_to_Patient_and_change_to_null_Patient_should_throw_ArgumentException()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
    //     appointment.assignPatient(patient);
    //     if (!appointment.Patient.Equals(patient))
    //     {
    //         Assert.Fail();
    //     }
    //
    //     try
    //     {
    //         appointment.changeAppointment(null);
    //         Assert.Fail("Expected ArgumentException");
    //     }
    //     catch (ArgumentException ae)
    //     {
    //         Patient.RemovePatient(patient);
    //         Appointment.removeAppointment(appointment);
    //         Assert.Pass();
    //     }
    // }
    
    // [Test]
    // public void Trying_to_assign_Bill_to_Patient_and_change_to_same_Patient_should_throw_InvalidOperationException()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
    //     appointment.assignPatient(patient);
    //     if (!appointment.Patient.Equals(patient))
    //     {
    //         Assert.Fail();
    //     }
    //
    //     try
    //     {
    //         appointment.changeAppointment(patient);
    //         Assert.Fail("Expected InvalidOperationException");
    //     }
    //     catch (InvalidOperationException ae)
    //     {
    //         Patient.RemovePatient(patient);
    //         Appointment.removeAppointment(appointment);
    //         Assert.Pass();
    //     }
    // }
    
    [Test]
    public void Trying_to_assign_Bill_to_null_Patient_should_throw_ArgumentException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
        try
        {
            appointment.assignPatient(null);
            Assert.Fail("expected ArgumentException");
        }
        catch (ArgumentException a)
        {
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_Bill_to_Patient_when_it_already_assigned_to_another_should_throw_InvalidOperationException()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Patient patient2 = new Patient(2,"Test1",new DateTime(2005));
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
        appointment.assignPatient(patient);
        try
        {
            appointment.assignPatient(patient2);
            Assert.Fail("expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Patient.RemovePatient(patient2);
            Patient.RemovePatient(patient);
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }
    }
}