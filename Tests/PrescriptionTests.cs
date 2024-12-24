namespace Tests;
using Hospital_System.Models;

public class PrescriptionTests
{
    [Test]
    public void Trying_to_create_Prescription_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        String name = "Test1";
        Prescription p = new Prescription(1, name, 0.3f, 4, false, new Bill());

        Assert.That(p.MedicationName, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_null_name_throws_ArgumentNullException()
    {
        try
        {
            Prescription p = new Prescription(1, null, 0.3f, 4, false, new Bill());
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Dosage_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        float dosage = 1.4f;
        Prescription p = new Prescription(1, "Test2", dosage, 4, false, new Bill());

        Assert.That(p.Dosage, Is.EqualTo(dosage));
        Prescription.RemovePrescription(p);
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_null_Bill_throws_ArgumentException()
    {
        try
        {
            Prescription p = new Prescription(1, "Test2", 1.2f, 4, false, null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Duration_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        int duration = 14;
        Prescription p = new Prescription(1, "Test3", 1.2f, duration, false, new Bill());

        Assert.That(p.Duration, Is.EqualTo(duration));
        Prescription.RemovePrescription(p);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Prescriptions_and_SetAppointments()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        List<Prescription> lp = new List<Prescription>{new ( 1, "Test1", 1.2f, 4, false, new Bill()), new (2, "Test2", 1.2f, 4, false, new Bill()), new ( 3, "Test3", 1.2f, 4, false, new Bill())};
        
        
        Assert.That(Prescription.GetPrescriptions(), Is.EqualTo(lp));
    }
    
    [Test]
    public void Trying_to_create_same_Prescription_throws_InvalidOperationException()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        Prescription b = new Prescription(5, "Test5", 1.2f, 4, false, new Bill());
        try
        {
            Prescription b2 = new Prescription(5, "Test5", 1.2f, 4, false, new Bill());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Prescription.RemovePrescription(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Prescription_InvalidOperationException_excepted()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        try
        {
            Prescription.RemovePrescription(new Prescription());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Prescriptions_and_save_them_to_file()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        List<Prescription> la = new List<Prescription>{new ( 1, "Test1", 1.2f, 4, false, new Bill()), new (2, "Test2", 1.2f, 4, false, new Bill()), new ( 3, "Test3", 1.2f, 4, false, new Bill())};
        
        SerializeToFIle.saveAll();
        
        foreach (Prescription o in la)
        {
            Prescription.RemovePrescription(o);
        }
        
        SerializeToFIle.loadAll();
        foreach (var o in Prescription.GetPrescriptions())
        {
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
        [Test]
    public void Trying_to_add_Bill_to_Prescription_and_then_delete_it()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        prescription.addBillToPrescription(bill);
        if (bill.Prescriptions.Contains(prescription) && prescription.Bills.Contains(bill))
        {
            prescription.RemoveBillFromPrescription(bill);
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
    public void Trying_to_add_Bill_to_Prescription()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        prescription.addBillToPrescription(bill);
        if (bill.Prescriptions.Contains(prescription) && prescription.Bills.Contains(bill))
        {
            Prescription.RemovePrescription(prescription);
            Bill.removeBill(bill);
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void Trying_to_add_many_Bills_to_Prescription()
    {
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        List<Bill> bills = new List<Bill> {
            new(21, 3213, new Service()),
            new(22, 3213, new Service()),
            new(23, 3213, new Service())
        };
        foreach (var e in bills)
        {
            prescription.addBillToPrescription(e);
        }
        
        bills.Add(new Bill());

        foreach (var e in prescription.Bills)
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

        Prescription.RemovePrescription(prescription);
        foreach (var e in bills)
        {
            Bill.removeBill(e);
        }

        Assert.Pass();
    }

    [Test]
    public void Trying_to_add_null_Bill_to_Prescription_throws_ArgumentException()
    {
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        try
        {
            prescription.RemoveBillFromPrescription(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Prescription.RemovePrescription(prescription);
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_add_Bill_to_Service_and_then_try_to_add_same_Bill_throws_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new Service());
        Prescription prescription = new Prescription(1, "Test", 0.3f, 4, false, new Bill());
        prescription.addBillToPrescription(bill);
        try
        {
            prescription.addBillToPrescription(bill);
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