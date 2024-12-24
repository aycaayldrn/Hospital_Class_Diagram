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
        Appointment a = new Appointment(date, at, dd, new Bill(), new Staff());
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
    public void Trying_to_create_Appointment_with_null_Bill_should_throw_ArgumentException()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }

        try
        {
            Appointment appointment = new Appointment(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59), Appointment.AppointmentType.FollowUp, new object(), null, new Staff());
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Appointment_with_null_Staff_should_throw_ArgumentException()
    {
        foreach (var o in Appointment.GetAppointments())
        {
            Appointment.removeAppointment(o);
        }

        try
        {
            Appointment appointment = new Appointment(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59), Appointment.AppointmentType.FollowUp, new object(), new Bill(), null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
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
            Appointment a = new Appointment(date, Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
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
            Appointment a = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.Surgery, new object(), new Bill(), new Staff());
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
        
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff()),
                                                     new ( new DateTime(3002,3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff()),
                                                     new ( new DateTime(3001, 3, 12,8,30,0) , Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff())};
     
    
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
        Appointment a = new Appointment(date , Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            Appointment a2 = new Appointment(date , Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
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
        
        List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff()), 
            new ( new DateTime(3002,3, 12,8,30,0), Appointment.AppointmentType.Consultation, new object(), new Bill(), new Staff()), 
            new ( new DateTime(3001, 3, 12,8,30,0) , Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff())};
        
        SerializeToFIle.saveAll();
        foreach (var o in la)
        {
            Appointment.removeAppointment(o);
        }
        
        SerializeToFIle.loadAll();

        foreach (var o in Appointment.GetAppointments())
        {
            o.addStaffToAppointment(new Staff());
            o.AddBillToAppointment(new Bill());
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_assign_Appointment_to_null_Patient_should_throw_ArgumentException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
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
    public void Trying_to_assign_Appointment_to_Patient_when_it_already_assigned_to_another_should_throw_InvalidOperationException()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Patient patient2 = new Patient(2,"Test1",new DateTime(2005));
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
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
    
    [Test]
    public void Trying_to_add_Staff_to_Appointment_and_then_delete_it()
    {
        Staff staff = new Staff(2,"Test2","test" , new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        appointment.addStaffToAppointment(staff);
        if (appointment.Staffs.Contains(staff) && staff.Appointments.Contains(appointment)) {
            appointment.removeStaffFromAppointment(staff);
            if (appointment.Staffs.Contains(staff) || staff.Appointments.Contains(appointment))
            {
                Assert.Fail();
            }
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Staff_to_Appointment()
    {
        Staff staff = new Staff(2,"Test2","test", new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        appointment.addStaffToAppointment(staff);
        if (appointment.Staffs.Contains(staff) && staff.Appointments.Contains(appointment)) {
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Staff_to_Appointment()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        List<Staff> staffs = new List<Staff>{new (1,"Test2","test", new Shift()),
            new (2,"Test2","test", new Shift()),
            new (3,"Test2","test", new Shift())};
        foreach (var e in staffs)
        {
            appointment.addStaffToAppointment(e);
        }
        staffs.Add(new Staff());
        foreach (var e in appointment.Staffs)
        {
            if (staffs.Contains(e) && e.Appointments.Contains(appointment))
            {
                
            }
            else
            {
                Assert.Fail(e.ToString());
            }
        }
        staffs.Remove(new Staff());
        Appointment.removeAppointment(appointment);
        foreach (var e in staffs)
        {
            Staff.RemoveStaff(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Staff_to_Appointment_throws_ArgumentNullException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            appointment.addStaffToAppointment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Appointment.removeAppointment(appointment);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Staff_to_Appointment_and_then_try_to_add_same_Prescription_throws_InvalidOperationException()
    {
        Staff staff = new Staff(2,"Test2","test", new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        appointment.addStaffToAppointment(staff);
        try
        {
            appointment.addStaffToAppointment(staff);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Appointment_to_Staff_and_then_remove_it()
    {
        Staff staff = new Staff(2,"Test2","test", new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        appointment.addStaffToAppointment(staff);
        if (appointment.Staffs.Contains(staff) && staff.Appointments.Contains(appointment)) {
            staff.RemoveAppointmentFromStaff(appointment);
            if (appointment.Staffs.Contains(staff) || staff.Appointments.Contains(appointment))
            {
                Assert.Fail();
            }
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Appointment_that_not_exist_in_list_from_Staff_should_throw_InvalidOperationException()
    {
        Staff staff = new Staff(2,"Test2","test", new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            staff.RemoveAppointmentFromStaff(appointment);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    
    [Test]
    public void Trying_to_remove_Staff_that_not_exist_in_list_from_Appointment_should_throw_InvalidOperationException()
    {
        Staff staff = new Staff(2,"Test2","test", new Shift());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            appointment.removeStaffFromAppointment(staff);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Staff.RemoveStaff(staff); 
            Appointment.removeAppointment(appointment); 
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Bill_that_not_exist_in_list_from_Appointment_should_throw_InvalidOperationException()
    {
        Bill bill = new Bill(21,3213, new Service());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            bill.RemoveAppointmentFromBill(appointment);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException argumentException)
        {
            Bill.removeBill(bill);
            Appointment.removeAppointment(appointment);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Appointment_and_then_remove_it()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        Bill bill = new Bill(21,3213, new Service());
        appointment.AddBillToAppointment(bill);
        if (bill.Appointments.Contains(appointment)&&appointment.Bills.Contains(bill)){
            bill.RemoveAppointmentFromBill(appointment);
            if (bill.Appointments.Contains(appointment)||appointment.Bills.Contains(bill))
            {
                Assert.Fail();
            }
            Bill.removeBill(bill);
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Appointment_and_then_try_to_add_same_Bill_throws_InvalidOperationException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        Bill bill = new Bill(21,3213, new Service());
        appointment.AddBillToAppointment(bill);
        try
        {
            appointment.AddBillToAppointment(bill);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Bill.removeBill(bill);
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    
    [Test]
    public void Trying_to_add_null_Bill_to_Appointment_throws_ArgumentException()
    {
        Bill bill = new Bill(21,3213, new Service());
        try
        {
            bill.AddAppointmentToBill(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Bill.removeBill(bill);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_many_Bills_to_Appointment()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        List<Bill> bills = new List<Bill>{new (25, 3213, new Service()),
            new (22, 312, new Service()),
            new (23, 432, new Service())};
        foreach (var e in bills)
        {
            appointment.AddBillToAppointment(e);
        }
        bills.Add(new Bill());
        foreach (var e in appointment.Bills)
        {
            if (bills.Contains(e))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        bills.Remove(new Bill());
        Appointment.removeAppointment(appointment);
        foreach (var e in bills)
        {
            Bill.removeBill(e);
        }
        Assert.Pass();
    }
}