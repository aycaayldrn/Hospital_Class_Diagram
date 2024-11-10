using Hospital_System.Models;

List<Appointment> la = new List<Appointment>{new ( new DateTime(3000, 3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
    new ( new DateTime(3002,3, 12,8,30,0), Appointment.AppointmentType.FollowUp, new object()),
    new ( new DateTime(3001, 3, 12,8,30,0) , Appointment.AppointmentType.FollowUp, new object())};
        
SerializeToFIle.saveAll();




