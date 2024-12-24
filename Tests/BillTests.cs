namespace Tests;
using Hospital_System.Models;

public class Tests
{
    [Test]
    public void Trying_to_set_wrong_val_to_taxRate_should_catch_Exception()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        try
        {
            Bill.TaxRate = 1.2;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }

    }

    [Test]
    public void Trying_to_create_Bill_with_wrong_totalCost_val_should_catch_Exception()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        try
        {
            Bill b = new Bill(12, -1, new Service());
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_create_Bill_with_specific_Number()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        int number = 123;

        Bill b = new Bill(number, 100, new Service());
        Assert.That(b.Number, Is.EqualTo(number));
        Bill.removeBill(b);
    }

    [Test]
    public void Trying_to_calulate_final_cost()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        int totalCost = 100;
        Service service = new Service("Test2", 100d);
        Bill b = new Bill(124, totalCost, service);
        Assert.That(b.FinalCost, Is.EqualTo(totalCost * (1 + Bill.TaxRate)).Within(0.01));
        Bill.removeBill(b);
        Service.RemoveService(service);
    }

    [Test]
    public void Trying_to_create_List_of_Bills_and_SetAppointments()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        List<Bill> lb = new List<Bill>
            { new(24, 4312, new Service()), new(21, 4313, new Service()), new(44, 4232, new Service()) };


        Assert.That(Bill.GetBills(), Is.EqualTo(lb));
    }

    [Test]
    public void Trying_to_create_same_Bill_throws_InvalidOperationException()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        Bill b = new Bill(21, 3213, new Service());
        try
        {
            Bill b2 = new Bill(21, 3213, new Service());
            Assert.Fail("Should throw InvalidOperationException");
        }
        catch (InvalidOperationException o)
        {
            Bill.removeBill(b);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_create_Bill_with_null_Service_throws_ArgumentException()
    {
        try
        {
            Bill b = new Bill(21, 3213, null);
            Assert.Fail("Should throw ArgumentException");
        }
        catch (ArgumentException o)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_remove_nonExisting_Bill_InvalidOperationException_excepted()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        try
        {
            Bill.removeBill(new Bill());
            Assert.Fail("Should throw InvalidOperationException");
        }
        catch (InvalidOperationException o)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_create_List_of_Bills_and_save_them_to_file()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }

        List<Bill> la = new List<Bill>
            { new(24, 4312, new Service()), new(21, 4313, new Service()), new(44, 4232, new Service()) };


        SerializeToFIle.saveAll();

        foreach (Bill o in la)
        {
            Bill.removeBill(o);
        }

        SerializeToFIle.loadAll();
        foreach (var o in Bill.GetBills())
        {
            o.AddServiceToBill(new Service());
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }

        Assert.Pass();
    }



    [Test]
    public void Trying_to_assign_Bill_to_Patient_and_change_Department()
    {
        Patient patient = new Patient(1, "Test1", new DateTime(2005));
        Patient patient2 = new Patient(2, "Test1", new DateTime(2005));
        Bill bill = new Bill(21, 3213, new Service());
        bill.assignPatientBill(patient);
        if (!bill.Patient.Equals(patient))
        {
            Assert.Fail();
        }

        bill.changeBill(patient2);

        if (patient2.Equals(bill.Patient))
        {
            Patient.RemovePatient(patient2);
            Patient.RemovePatient(patient);
            Bill.removeBill(bill);
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }

    [Test]
    public void Trying_to_assign_Bill_to_Patient_and_change_to_null_Patient_should_throw_ArgumentException()
    {
        Patient patient = new Patient(1, "Test1", new DateTime(2005));
        Bill bill = new Bill(21, 3213, new Service());
        bill.assignPatientBill(patient);
        if (!bill.Patient.Equals(patient))
        {
            Assert.Fail();
        }

        try
        {
            bill.changeBill(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException ae)
        {
            Patient.RemovePatient(patient);
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_assign_Bill_to_Patient_and_change_to_same_Patient_should_throw_InvalidOperationException()
    {
        Patient patient = new Patient(1, "Test1", new DateTime(2005));
        Bill bill = new Bill(21, 3213, new Service());
        bill.assignPatientBill(patient);
        if (!bill.Patient.Equals(patient))
        {
            Assert.Fail();
        }

        try
        {
            bill.changeBill(patient);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException ae)
        {
            Patient.RemovePatient(patient);
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_assign_Bill_to_null_Patient_should_throw_ArgumentException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        try
        {
            bill.assignPatientBill(null);
            Assert.Fail("expected ArgumentException");
        }
        catch (ArgumentException a)
        {
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }

    [Test]
    public void
        Trying_to_assign_Bill_to_Patient_when_it_already_assigned_to_another_should_throw_InvalidOperationException()
    {
        Patient patient = new Patient(1, "Test1", new DateTime(2005));
        Patient patient2 = new Patient(2, "Test1", new DateTime(2005));
        Bill bill = new Bill(21, 3213, new Service());
        bill.assignPatientBill(patient);
        try
        {
            bill.assignPatientBill(patient2);
            Assert.Fail("expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Patient.RemovePatient(patient2);
            Patient.RemovePatient(patient);
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_add_Appointment_to_Bill_and_then_delete_it()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12, 8, 30, 0),
            Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        Bill bill = new Bill(21, 3213, new Service());
        bill.AddAppointmentToBill(appointment);
        if (bill.Appointments.Contains(appointment) && appointment.Bills.Contains(bill))
        {
            bill.RemoveAppointmentFromBill(appointment);
            if (bill.Appointments.Contains(appointment) || appointment.Bills.Contains(bill))
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
    public void Trying_to_add_Appointment_to_Bill()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12, 8, 30, 0),
            Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        Bill bill = new Bill(21, 3213, new Service());
        bill.AddAppointmentToBill(appointment);
        if (bill.Appointments.Contains(appointment) && appointment.Bills.Contains(bill))
        {
            Bill.removeBill(bill);
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_many_Appointments_to_Bill()
    {
        Bill bill = new Bill(21, 3213, new Service());
        List<Appointment> appointments = new List<Appointment>
        {
            new(new DateTime(3000, 3, 12, 8, 30, 0), Appointment.AppointmentType.FollowUp, new object(), new Bill(),
                new Staff()),
            new(new DateTime(3001, 3, 12, 8, 30, 0), Appointment.AppointmentType.FollowUp, new object(), new Bill(),
                new Staff()),
            new(new DateTime(3002, 3, 12, 8, 30, 0), Appointment.AppointmentType.FollowUp, new object(), new Bill(),
                new Staff())
        };
        foreach (var e in appointments)
        {
            bill.AddAppointmentToBill(e);
        }

        foreach (var e in bill.Appointments)
        {
            if (appointments.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }

        Bill.removeBill(bill);
        foreach (var e in appointments)
        {
            Appointment.removeAppointment(e);
        }

        Assert.Pass();
    }

    [Test]
    public void Trying_to_add_null_Appointment_to_Bill_throws_ArgumentException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12, 8, 30, 0),
            Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        try
        {
            appointment.AddBillToAppointment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Appointment.removeAppointment(appointment);
            Assert.Pass();
        }
    }

    [Test]
    public void
        Trying_to_add_Appointment_to_Bill_and_then_try_to_add_same_Appointment_throws_InvalidOperationException()
    {
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12, 8, 30, 0),
            Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
        Bill bill = new Bill(21, 3213, new Service());
        bill.AddAppointmentToBill(appointment);
        try
        {
            bill.AddAppointmentToBill(appointment);
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
    public void Trying_to_remove_Appointment_that_not_exist_in_list_from_Bill_should_throw_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12, 8, 30, 0),
            Appointment.AppointmentType.FollowUp, new object(), new Bill(), new Staff());
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
    public void Trying_to_add_Service_to_Bill_and_then_delete_it()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Service service = new Service("Test", 100d);
        bill.AddServiceToBill(service);
        if (bill.Services.Contains(service) && service.Bills.Contains(bill))
        {
            bill.RemoveServiceFromBill(service);
            if (bill.Services.Contains(service) || service.Bills.Contains(bill))
            {
                Assert.Fail();
            }

            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_Service_to_Bill()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Service service = new Service("Test", 100d);
        bill.AddServiceToBill(service);
        if (bill.Services.Contains(service) && service.Bills.Contains(bill))
        {
            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_many_Services_to_Bill()
    {
        Bill bill = new Bill(21, 3213, new Service());
        List<Service> services = new List<Service>
        {
            new("Test", 100d),
            new("Test1", 100d),
            new("Test2", 100d)
        };
        foreach (var e in services)
        {
            bill.AddServiceToBill(e);
        }

        services.Add(new Service());

        foreach (var e in bill.Services)
        {
            if (services.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }

        services.Remove(new Service());
        Bill.removeBill(bill);
        foreach (var e in services)
        {
            Service.RemoveService(e);
        }

        Assert.Pass();
    }

    [Test]
    public void Trying_to_add_null_Service_to_Bill_throws_ArgumentException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        try
        {
            bill.AddServiceToBill(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }


    [Test]
    public void Trying_to_add_Service_to_Bill_and_then_try_to_add_same_Service_throws_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Service service = new Service("Test", 100d);
        bill.AddServiceToBill(service);
        try
        {
            bill.AddServiceToBill(service);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_Prescription_to_Bill_and_then_delete_it()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        bill.assignPrescriptionToBill(prescription);
        if (bill.Prescriptions.Contains(prescription) && prescription.Bills.Contains(bill))
        {
            bill.removePrescriptionFromBill(prescription);
            if (bill.Prescriptions.Contains(prescription) || prescription.Bills.Contains(bill))
            {
                Assert.Fail();
            }

            Prescription.RemovePrescription(prescription);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_Prescription_to_Bill()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        bill.assignPrescriptionToBill(prescription);
        if (bill.Prescriptions.Contains(prescription) && prescription.Bills.Contains(bill))
        {
            Prescription.RemovePrescription(prescription);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_many_Prescriptions_to_Bill()
    {
        Bill bill = new Bill(21, 3213, new Service());
        List<Prescription> prescriptions = new List<Prescription>
        {
            new(1, "Test", 0.3f, 4, false, new Bill()),
            new(2, "Test", 0.3f, 4, false, new Bill()),
            new(3, "Test", 0.3f, 4, false, new Bill())
        };
        foreach (var e in prescriptions)
        {
            bill.assignPrescriptionToBill(e);
        }

        foreach (var e in bill.Prescriptions)
        {
            if (prescriptions.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }

        Bill.removeBill(bill);
        foreach (var e in prescriptions)
        {
            Prescription.RemovePrescription(e);
        }

        Assert.Pass();
    }

    [Test]
    public void Trying_to_add_null_Prescription_to_Bill_throws_ArgumentException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        try
        {
            bill.assignPrescriptionToBill(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Bill.removeBill(bill);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_add_Prescription_to_Bill_and_then_try_to_add_same_Service_throws_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        bill.assignPrescriptionToBill(prescription);
        try
        {
            bill.assignPrescriptionToBill(prescription);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Prescription.RemovePrescription(prescription);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }
}