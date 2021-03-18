using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Data
{
    public class DoctorDataAccess : DbDataAccess<Doctor>
    {
        public override void Delete(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Doctor entity)
        {
            throw new NotImplementedException();
        }   

        public override ICollection<Doctor> Select()
        {
            var selectSqlScript = @"select d.id, d.name, s.id as scheduleId, s.startTime, s.endTime, dir.id as directionId, dir.name as directionName from Doctors d 
                                    join Directions dir on d.directionId = dir.id
                                    join Schedules s on d.scheduleId = s.id";
            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var doctors = new List<Doctor>();

            while (dataReader.Read())
            {
                doctors.Add(new Doctor
                {
                    Id = Guid.Parse(dataReader["id"].ToString()),
                    FullName = dataReader["name"].ToString(),
                    Schedule = new Schedule
                    {
                        Id = Guid.Parse(dataReader["scheduleId"].ToString()),
                        StartTime = DateTime.Parse(dataReader["startTime"].ToString()),
                        EndTime = DateTime.Parse(dataReader["endTime"].ToString())
                    },
                    Direction = new Direction
                    {
                        Id = Guid.Parse(dataReader["directionId"].ToString()),
                        Name = dataReader["directionName"].ToString()
                    }
                });
            }
            dataReader.Close();
            command.Dispose();
            return doctors;
        }
        public override void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
