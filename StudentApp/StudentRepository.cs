using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public class StudentRepository
    {
        // Bütün tələbələri gətirir (siyahını yeniləmək üçün)
        public List<Student> GetAll()
        {
            var list = new List<Student>();
            const string sql = "SELECT StudentId, FirstName, LastName, Age, Email FROM Students";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Student
                        {
                            StudentId = r.GetInt32(0),
                            FirstName = r.GetString(1),
                            LastName = r.GetString(2),
                            Age = r.IsDBNull(3) ? null : r.GetInt32(3),
                            Email = r.IsDBNull(4) ? "" : r.GetString(4)
                        });
                    }
                }
            }
            return list;
        }

        // Yeni tələbə əlavə edir (Parametrlərlə)
        public int Add(Student s)
        {
            const string sql = @"INSERT INTO Students (FirstName, LastName, Age, Email) 
                                 VALUES (@firstName, @lastName, @age, @email)";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@firstName", s.FirstName);
                cmd.Parameters.AddWithValue("@lastName", s.LastName);
                cmd.Parameters.AddWithValue("@age", (object?)s.Age ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@email", s.Email);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}