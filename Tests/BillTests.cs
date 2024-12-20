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
            Bill b = new Bill(12, -1);
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
        
        Bill b = new Bill(number, 100);
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
        Bill b = new Bill(124, totalCost);
        Assert.That(b.FinalCost, Is.EqualTo(totalCost*(1+Bill.TaxRate)).Within(0.01));
        Bill.removeBill(b);
    }
    
    [Test]
    public void Trying_to_create_List_of_Bills_and_SetAppointments()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }
        List<Bill> lb = new List<Bill>{new ( 24,4312), new ( 21,4313), new ( 44,4232)};
        
        
        Assert.That(Bill.GetBills(), Is.EqualTo(lb));
    }
    
    [Test]
    public void Trying_to_create_same_Bill_throws_InvalidOperationException()
    {
        foreach (var o in Bill.GetBills().ToList())
        {
            Bill.removeBill(o);
        }
        Bill b = new Bill(21,3213);
        try
        {
            Bill b2 = new Bill(21,3213);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Bill.removeBill(b);
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
        }catch(InvalidOperationException o)
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
        
        List<Bill> la = new List<Bill>{new ( 24,4312), new ( 21,4313), new ( 44,4232)};
        
        
        SerializeToFIle.saveAll();
        
        foreach (Bill o in la)
        {
            Bill.removeBill(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Bill.GetBills(), Is.EqualTo(la));
    }
}