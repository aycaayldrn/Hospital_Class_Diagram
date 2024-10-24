namespace Tests;
using Hospital_System.Models;


public class FellowTests
{
    [Test]
    public void Trying_to_assign_null_to_specialization_should_throw_ArgumentNullException()
    {
        try
        {
            Fellow f = new Fellow(null);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_create_Fellow_with_specific_name_of_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Fellow f = new Fellow(name);
        
        Assert.That(f.Specialization, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_create_Fellow_with_researchProject_name_of_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Fellow f = new Fellow("name",name);
        
        Assert.That(f.ResearchProject, Is.EqualTo(name));
    }
}