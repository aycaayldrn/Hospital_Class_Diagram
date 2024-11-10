using Hospital_System.Models;

namespace Tests;

public class DummyDoctor:Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public DummyDoctor(){}

}