using Data;
using Models;
using Services;
using System;
using System.Collections.Generic;

namespace AdoNetExam
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationService.Init();
            ICollection<Doctor> doctors;
            ICollection<VisitHistory> visitTime;
            using (var doctorDataAccess = new DoctorDataAccess())
            {
                doctors = doctorDataAccess.Select();
            }
            Console.WriteLine(@"                                *********************************************
                                *			                    *
                                *			                    *
                                *       Welcome to the Hospital System!     *
                                *			                    *
                                *			                    *
                                *********************************************");
            var patient = new Patient();
            while (true)
            {
                Console.Write("Enter your Full name:\n");
                using (var patientDataAccess = new PatientDataAccess())
                {
                    patient.FullName = Console.ReadLine();
                    patientDataAccess.Insert(patient);
                    visitTime = patientDataAccess.SelectVisitTime();
                }
                Console.Clear();

                Console.Write("\n1. Sign up for an appointment\n\n0. Exit\nChoose: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Choose doctor's direction: \n");
                        var direction = Console.ReadLine();
                        foreach (var doctor in doctors)
                        {
                            if (doctor.Direction.Name.Contains(direction))
                            {
                                using (var patientDataAccess = new PatientDataAccess())
                                {
                                    foreach(var date in visitTime)
                                    {
                                        if(date.DoctorId == doctor.Id && date.VisitDate.Hour <= DateTime.Now.Hour)
                                        {
                                            doctor.IsFree = true;
                                        }
                                    }
                                    if (doctor.Schedule.StartTime.Hour >= DateTime.Now.Hour &&
                                  (doctor.Schedule.EndTime.Hour - 1) <= DateTime.Now.Hour &&
                                  doctor.IsFree == true)
                                    {

                                        patientDataAccess.InsertHistory(doctor, patient);

                                    }
                                }
                            }
                        }
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}