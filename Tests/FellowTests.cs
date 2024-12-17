// namespace Tests;
// using Hospital_System.Models;
//
//
// public class FellowTests
// {
//     [Test]
//     public void Trying_to_assign_null_to_specialization_should_throw_ArgumentNullException()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//
//         try
//         {
//             Fellow f = new Fellow(null);
//             Assert.Fail("Expected ArgumentException");
//         }
//         catch (ArgumentException)
//         {
//             Assert.Pass();
//         }
//     }
//
//     [Test]
//     public void Trying_to_create_Fellow_with_specific_name_of_specialization_and_check_if_it_assigned_correctly()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//
//         String name = "Test2";
//         int id = 1;
//         Fellow f = new Fellow(name,name);
//         
//         Assert.That(f.Specialization, Is.EqualTo(name));
//         
//         Fellow.removeFellow(f);
//     }
//     
//     [Test]
//     public void Trying_to_create_Fellow_with_researchProject_name_of_specialization_and_check_if_it_assigned_correctly()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//
//         String name = "Test";
//         Fellow f = new Fellow("name",name);
//         
//         Assert.That(f.ResearchProject, Is.EqualTo(name));
//         Fellow.removeFellow(f);
//     }
//     
//      
//     [Test]
//     public void Trying_to_create_List_of_Fellows_and_SetAppointments()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//
//         List<Fellow> lf = new List<Fellow>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
//         
//         Assert.That(Fellow.GetFellows(), Is.EqualTo(lf));
//     }
//     
//     [Test]
//     public void Trying_to_create_same_Fellow_throws_InvalidOperationException()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//
//         Fellow b = new Fellow(1, "Test1","Test1");
//         try
//         {
//             Fellow b2 = new Fellow(2,"Test1","Test1");
//             Assert.Fail("Should throw InvalidOperationException");
//         }catch(InvalidOperationException o)
//         {
//             Fellow.removeFellow(b);
//             Assert.Pass();
//         }
//     }
//     
//     [Test]
//     public void Trying_to_remove_nonExisting_Fellow_InvalidOperationException_excepted()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//         
//         try
//         {
//             Fellow.removeFellow(new Fellow());
//             Assert.Fail("Should throw InvalidOperationException");
//         }catch(InvalidOperationException o)
//         {
//             Assert.Pass();
//         }
//     }
//     
//         
//     [Test]
//     public void Trying_to_create_List_of_Fellows_and_save_them_to_file()
//     {
//         foreach (var o in Fellow.GetFellows().ToList())
//         {
//             Fellow.removeFellow(o);
//         }
//         
//         List<Fellow> la = new List<Fellow>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
//         
//         SerializeToFIle.saveAll();
//         
//         foreach (Fellow o in la)
//         {
//             Fellow.removeFellow(o);
//         }
//         
//         SerializeToFIle.loadAll();
//         
//         Assert.That(Fellow.GetFellows(), Is.EqualTo(la));
//     }
// }