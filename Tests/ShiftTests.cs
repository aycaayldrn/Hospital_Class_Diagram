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
        DateTime start = new DateTime(2019, 01, 01);
        DateTime end = new DateTime(2020, 01, 01);
        
        Shift s = new Shift(start,end,"test");
        Assert.That(s.GetShiftDuration(), Is.EqualTo(end - start));
    }
}