using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Data
{
    public class PatientDataAccess : DbDataAccess<Patient>
    {
        public override void Delete(Patient entity)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Patient entity)
        {
            throw new NotImplementedException();
        }

        public void InsertHistory(Doctor doctor, Patient patient)
        {
            var insertSqlScript = $"Insert into PatientsVisitHistory (id, doctorId, patientId, visitDate) " +
    $"values (\'{Guid.NewGuid()}\',@doctorId, @patientId, \'{DateTime.Now}\'";
            using (var transaction = sqlConnection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = sqlConnection;
                command.CommandText = insertSqlScript;
                try
                {
                    command.Transaction = transaction;

                    var doctorIdSqlParameter = command.CreateParameter();
                    doctorIdSqlParameter.DbType = System.Data.DbType.Guid;
                    doctorIdSqlParameter.Value = doctor.Id;
                    doctorIdSqlParameter.ParameterName = "doctorId";

                    command.Parameters.Add(doctorIdSqlParameter);

                    var patientIdSqlParameter = command.CreateParameter();
                    patientIdSqlParameter.DbType = System.Data.DbType.Guid;
                    patientIdSqlParameter.Value = patient.Id;
                    patientIdSqlParameter.ParameterName = "patientId";

                    command.Parameters.Add(patientIdSqlParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }
        public ICollection<VisitHistory> SelectVisitTime()
        {
            var selectSqlScript = @"select id, visitDate, doctorId from PatientsVisitHistory";
            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var history = new List<VisitHistory>();

            while (dataReader.Read())
            {
                history.Add(new VisitHistory
                {
                    Id = Guid.Parse(dataReader["id"].ToString()),
                    VisitDate = DateTime.Parse(dataReader["visitDate"].ToString()),
                    DoctorId = Guid.Parse(dataReader["DoctorId"].ToString())
                });
            }
            dataReader.Close();
            command.Dispose();
            return history;
        }

        public override ICollection<Patient> Select()
        {
            throw new NotImplementedException();
        }

        public override void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
