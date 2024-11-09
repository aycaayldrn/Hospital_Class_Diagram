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
            
            Assert.Fail("Expected ArgumentException");
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
            Shift s = new Shift(new DateTime(2021, 01, 01), new DateTime(2020, 01, 01), "test1");
            
            Assert.Fail("Expected ArgumentException");
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
        
        Shift s = new Shift(start,end,"test2");
        Assert.That(s.GetShiftDuration(), Is.EqualTo(end - start));
        Shift.RemoveShift(s);
    }
    
    [Test]
    public void Trying_to_create_Shift_with_specific_day_and_check_if_it_assigned_correctly()
    {
        String day = "2024-08-01";
        Shift s = new Shift(new DateTime(2019, 01, 01), new DateTime(2020, 01, 01), day);

        Assert.That(s.Day, Is.EqualTo(day));
        Shift.RemoveShift(s);
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Shifts_and_SetAppointments()
    {
        Shift.SetShifts(new List<Shift>());
        List<Shift> lb = new List<Shift>{new ( new DateTime(2004), new DateTime(2005),"test"), new ( new DateTime(2005), new DateTime(2006),"test"), new ( new DateTime(2006), new DateTime(2007),"test")};
        
        Shift.SetShifts(lb);
        
        Assert.That(Shift.GetShifts(), Is.EqualTo(lb));
        
        Shift.SetShifts(new List<Shift>());
    }
    
    [Test]
    public void Trying_to_create_same_Shift_throws_InvalidOperationException()
    {
        Shift b = new Shift(new DateTime(2004), new DateTime(2005),"test");
        try
        {
            Shift b2 = new Shift(new DateTime(2004), new DateTime(2005),"test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Shift_InvalidOperationException_excepted()
    {
        try
        {
            Shift.RemoveShift(new Shift());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Shifts_and_save_them_to_file()
    {
        Shift.SetShifts(new List<Shift>());
        List<Shift> la = new List<Shift>{new ( new DateTime(2004), new DateTime(2005),"test"), new ( new DateTime(2005), new DateTime(2006),"test"), new ( new DateTime(2006), new DateTime(2007),"test")};
        
        SerializeToFIle.saveAll();
        
        Shift.SetShifts(new List<Shift>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Shift.GetShifts(), Is.EqualTo(la));
    }
}