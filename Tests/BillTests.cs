namespace Tests;
using Hospital_System.Models;

public class Tests
{
    [Test]
    public void Trying_to_set_wrong_val_to_taxRate_should_catch_Exception()
    {
        try
        {
            Bill.TaxRate = 1.2;
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Bill_with_wrong_totalCost_val_should_catch_Exception()
    {
        try
        {
            Bill b = new Bill(12, -1);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Bill_with_specific_Number()
    {
        int number = 12;
        
        Bill b = new Bill(number, 100);
        Assert.That(b.Number, Is.EqualTo(number));
    }
    
    [Test]
    public void Trying_to_calulate_final_cost()
    {
        int totalCost = 100;
        Bill b = new Bill(12, totalCost);
        Assert.That(b.FinalCost, Is.EqualTo(totalCost*(1+Bill.TaxRate)).Within(0.01));
    }
}