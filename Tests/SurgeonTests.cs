namespace Tests;
using Hospital_System.Models;

public class SurgeonTests
{
    
    // [Test]
    // public void Trying_to_create_List_of_Surgeons_and_SetAppointments()
    // {
    //     foreach (var o in Surgeon.GetSurgeons().ToList())
    //     {
    //         Surgeon.RemoveSurgeon(o);
    //     }
    //     
    //     List<Surgeon> lb = new List<Surgeon>{new ( ), new ( ), new ()};
    //     
    //     Assert.That(Surgeon.GetSurgeons(), Is.EqualTo(lb));
    // }
    
    [Test]
    public void Trying_to_remove_nonExisting_Surgeon_InvalidOperationException_excepted()
    {
        foreach (var o in Surgeon.GetSurgeons().ToList())
        {
            Surgeon.RemoveSurgeon(o);
        }

        try
        {
            Surgeon.RemoveSurgeon(new Surgeon());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }


    //[Test]
    //public void Trying_to_create_List_of_Surgeons_and_save_them_to_file()
    //{
    //    Surgeon.SetSurgeons(new List<Surgeon>());
    //    List<Surgeon> la = new List<Surgeon> { new(), new(), new() };

    //    SerializeToFIle.saveAll();

    //    Surgeon.SetSurgeons(new List<Surgeon>());

    //    SerializeToFIle.loadAll();

    //    Assert.That(Surgeon.GetSurgeons(), Is.EqualTo(la));
    //}
}