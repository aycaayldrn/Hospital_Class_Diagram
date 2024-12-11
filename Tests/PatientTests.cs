namespace Tests;
using Hospital_System.Models;

public class PatientTests
{
    [Test]
    public void Trying_to_create_patient_with_null_name_should_throw_ArgumentNullException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        try
        {
            Patient p = new Patient(1, null, new DateTime(2024, 8, 18));

            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_patient_with_birthday_in_future_should_throw_ArgumentNullException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        try
        {
            Patient p = new Patient(1, null, new DateTime(3000, 8, 18));

            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Patient_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        String name = "Test2";
        Patient p = new Patient(1, name, new DateTime(2024, 8, 18));

        Assert.That(p.Name, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_set_Diagnoses_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Diagnoses = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_set_Treatments_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Treatments = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_set_Allergies_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Allergies = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_get_Age()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        DateTime date = new DateTime(2003, 12, 18);
        
        Patient p = new Patient(1, "name", date);
        Assert.That(p.Age, Is.EqualTo(date.Month > DateTime.Today.Month || date.Day > DateTime.Today.Day ? DateTime.Today.Year - date.Year - 1 : DateTime.Today.Year - date.Year));
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Patients_and_SetAppointments()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        List<Patient> lp = new List<Patient>{new ( 1,"Test1",new DateTime(2005)), new ( 2,"Test2",new DateTime(2005)), new (3,"Test3",new DateTime(2005))};
        
        Assert.That(Patient.GetPatients(), Is.EqualTo(lp));
    }
    
    [Test]
    public void Trying_to_create_same_Patient_throws_InvalidOperationException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        Patient b = new Patient(1,"Test1",new DateTime(2005));
        try
        {
            Patient b2 = new Patient(1,"Test1",new DateTime(2005));
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Patient.RemovePatient(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Patient_InvalidOperationException_excepted()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        try
        {
            Patient.RemovePatient(new Patient());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Patients_and_save_them_to_file()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        List<Patient> la = new List<Patient>{new ( 1,"Test34",new DateTime(2009)), new ( 2,"Test2",new DateTime(2005)), new (3,"Test3",new DateTime(2005))};
        
        SerializeToFIle.saveAll();
        
        foreach (Patient o in la.ToList())
        {
            Patient.RemovePatient(o);
        }
        
        SerializeToFIle.loadAll();

        Assert.That(Patient.GetPatients(), Is.EqualTo(la));
    }
    
    
    // [Test]
    // public void Trying_to_add_Insurance_Provider_to_Patient_and_then_delete_it()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     Insurance_Provider provider = new Insurance_Provider(1, "Test2");
    //     patient.AddInsuranceProviderToPatient(provider);
    //     foreach (var e in patient.GetProviders())
    //     {
    //         if (e.Id == provider.Id)
    //         {
    //             patient.RemoveInsuranceProviderFromPatient(provider);
    //             foreach (var e2 in patient.GetProviders())
    //             {
    //                 if (e2.Id == provider.Id)
    //                 {
    //                     Assert.Fail("Insurance Provider has not been deleted");
    //                 }
    //             }
    //             Patient.RemovePatient(patient);
    //             Insurance_Provider.removeProvider(provider);
    //             Assert.Pass();
    //         }
    //     }
    //     Assert.Fail("Insurance Provider has not been added");
    // }
    //
    // [Test]
    // public void Trying_to_add_Insurance_Provider_to_Patient()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     Insurance_Provider provider = new Insurance_Provider(1, "Test2");
    //     patient.AddInsuranceProviderToPatient(provider);
    //     foreach (var e in patient.GetProviders())
    //     {
    //         if (e.Id == provider.Id)
    //         {
    //             Insurance_Provider.removeProvider(provider);
    //             Patient.RemovePatient(patient);
    //             Assert.Pass();
    //         }
    //     }
    //     Assert.Fail();
    // }
    //
    // [Test]
    // public void Trying_to_add_many_Insurance_Providers_to_Patient()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     List<Insurance_Provider> providers = new List<Insurance_Provider>{new (1,"Test2"), new (2,"Test2"), new (3,"Test2")};
    //     foreach (var e in providers)
    //     {
    //         patient.AddInsuranceProviderToPatient(e);
    //     }
    //
    //     foreach (var e in patient.GetProviders())
    //     {
    //         if (providers.Exists(e => providers.Count==patient.GetProviders().Count))
    //         {
    //             
    //         }
    //         else
    //         {
    //             Assert.Fail();
    //         }
    //     }
    //     Patient.RemovePatient(patient);
    //     foreach (var e in providers)
    //     {
    //         Insurance_Provider.removeProvider(e);
    //     }
    //     Assert.Pass();
    // }
    //
    // [Test]
    // public void Trying_to_add_null_Insurance_Provider_to_Patient_throws_ArgumentNullException()
    // {
    //     Patient patient = new Patient(1,"Test1",new DateTime(2005));
    //     try
    //     {
    //         patient.AddInsuranceProviderToPatient(null);
    //         Assert.Fail("Expected ArgumentException");
    //     }
    //     catch (ArgumentException argumentException)
    //     {
    //         Patient.RemovePatient(patient);
    //         Assert.Pass();   
    //     }
    // }
    
    
    [Test]
    public void Trying_to_add_Appointment_to_Patient_and_then_delete_it()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
        patient.addAppointmentForPatient(appointment);
        foreach (var e in patient.GetPatientsAppointments())
        {
            if (e.Equals(appointment))
            {
                patient.removeAppointmentFromPatient(appointment);
                foreach (var e2 in patient.GetPatientsAppointments())
                {
                    if (e2.Equals(appointment))
                    {
                        Assert.Fail("Appointment has not been deleted");
                    }
                }
                Patient.RemovePatient(patient);
                Appointment.removeAppointment(appointment);
                Assert.Pass();
            }
        }
        Assert.Fail("Appointment has not been added");
    }
    
    [Test]
    public void Trying_to_add_Appointment_to_Patient()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Appointment appointment = new Appointment(new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object());
        patient.addAppointmentForPatient(appointment);
        foreach (var e in patient.GetPatientsAppointments())
        {
            if (e.Equals(appointment))
            {
                Appointment.removeAppointment(appointment);
                Patient.RemovePatient(patient);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Appointment_to_Patient()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        List<Appointment> appointments = new List<Appointment>{new (new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
            new (new DateTime(3001, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
            new (new DateTime(3002, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object())};
        foreach (var e in appointments)
        {
            patient.addAppointmentForPatient(e);
        }
    
        foreach (var e in patient.GetPatientsAppointments())
        {
            if (appointments.Exists(e => appointments.Count==patient.GetPatientsAppointments().Count))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Patient.RemovePatient(patient);
        foreach (var e in appointments)
        {
            Appointment.removeAppointment(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Appointment_to_Patient_throws_ArgumentNullException()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        try
        {
            patient.addAppointmentForPatient(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Patient.RemovePatient(patient);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Patient_and_then_delete_it()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Bill bill = new Bill(124, 100);
        patient.addBillForPatient(bill);
        foreach (var e in patient.GetPatientsBills())
        {
            if (e.Equals(bill))
            {
                patient.removeBillFromPatient(bill);
                foreach (var e2 in patient.GetPatientsBills())
                {
                    if (e2.Equals(bill))
                    {
                        Assert.Fail("Bill has not been deleted");
                    }
                }
                Patient.RemovePatient(patient);
                Bill.removeBill(bill);
                Assert.Pass();
            }
        }
        Assert.Fail("Bill has not been added");
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Patient()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        Bill bill = new Bill(124, 100);
        patient.addBillForPatient(bill);
        foreach (var e in patient.GetPatientsBills())
        {
            if (e.Equals(bill))
            {
                Bill.removeBill(bill);
                Patient.RemovePatient(patient);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Bill_to_Patient()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        List<Bill> bills = new List<Bill>{new (1, 100), new (2, 100), new (3, 100)};
        foreach (var e in bills)
        {
            patient.addBillForPatient(e);
        }
    
        foreach (var e in patient.GetPatientsAppointments())
        {
            if (bills.Exists(e => bills.Count==patient.GetPatientsAppointments().Count))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Patient.RemovePatient(patient);
        foreach (var e in bills)
        {
            Bill.removeBill(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Bill_to_Patient_throws_ArgumentNullException()
    {
        Patient patient = new Patient(1,"Test1",new DateTime(2005));
        try
        {
            patient.addAppointmentForPatient(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Patient.RemovePatient(patient);
            Assert.Pass();   
        }
    }
}