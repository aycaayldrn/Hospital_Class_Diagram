namespace Tests;
using Hospital_System.Models;

public class ShiftTests
{
    [Test]
    public void Trying_to_create_Shift_with_null_day_should_throw_ArgumentNullException()
    {
        try
        {
            Shift s = new Shift(new DateTime(2001, 01, 01), new DateTime(2020, 01, 01), null);
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Shift_with_startTime_later_then_endTime_should_throw_ArgumentNullException()
    {
        try
        {
            Shift s = new Shift(new DateTime(2021, 01, 01), new DateTime(2020, 01, 01), "test");
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_get_ShiftDuration()
    {
        DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,8,30,0);
        DateTime end = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,18,30,0);
        
        Shift s = new Shift(start,end,"test");
        Assert.That(s.GetShiftDuration(), Is.EqualTo(end - start));
    }
    
    [Test]
    public void Trying_to_create_Shift_with_specific_day_and_check_if_it_assigned_correctly()
    {
        String day = "2024-08-01";
        Shift s = new Shift(new DateTime(2019, 01, 01), new DateTime(2020, 01, 01), day);

        Assert.That(s.Day, Is.EqualTo(day));
    }
}