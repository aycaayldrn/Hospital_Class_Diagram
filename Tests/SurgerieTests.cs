namespace Tests;
using Hospital_System.Models;

public class SurgerieTests
{
    [Test]
    public void Trying_to_create_Surgerie_with_null_name_should_throw_ArgumentNullException()
    {
        try
        {
            Surgerie s = new Surgerie(null);
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
}