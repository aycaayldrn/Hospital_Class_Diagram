﻿namespace Tests;
using Hospital_System.Models;

public class SurgeonTests
{
    
    [Test]
    public void Trying_to_create_List_of_Surgeons_and_SetAppointments()
    {
        Surgeon.SetSurgeons(new List<Surgeon>());
        List<Surgeon> lb = new List<Surgeon>{new ( ), new ( ), new ()};
        
        Surgeon.SetSurgeons(lb);
        
        Assert.That(Surgeon.GetSurgeons(), Is.EqualTo(lb));
        
        Surgeon.SetSurgeons(new List<Surgeon>());
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Surgeon_InvalidOperationException_excepted()
    {
        try
        {
            Surgeon.RemoveSurgeon(new Surgeon());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    // [Test]
    // public void Trying_to_create_List_of_Surgeons_and_save_them_to_file()
    // {
    //     Surgeon.SetSurgeons(new List<Surgeon>());
    //     List<Surgeon> la = new List<Surgeon>{new ( ), new ( ), new ()};
    //     
    //     SerializeToFIle.saveAll();
    //     
    //     Surgeon.SetSurgeons(new List<Surgeon>());
    //     
    //     SerializeToFIle.loadAll();
    //     
    //     Assert.That(Surgeon.GetSurgeons(), Is.EqualTo(la));
    // }
}