using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public class CourseRepository
    {
        public List<Course> GetAll()
        {
            var list = new List<Course>();
            const string sql = "SELECT CourseId, CourseName, Credits FROM Courses";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Course
                        {
                            Id = r.GetInt32(0),
                            Title = r.GetString(1),
                            Credits = r.GetInt32(2)
                        });
                    }
                }
            }
            return list;
        }

        public int Add(Course c)
        {
            const string sql = "INSERT INTO Courses (CourseName, Credits) VALUES (@title, @credits)";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@title", c.Title);
                cmd.Parameters.AddWithValue("@credits", c.Credits);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Course c)
        {
            const string sql = "UPDATE Courses SET CourseName = @title, Credits = @credits WHERE CourseId = @id";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@title", c.Title);
                cmd.Parameters.AddWithValue("@credits", c.Credits);
                cmd.Parameters.AddWithValue("@id", c.Id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            const string sql = "DELETE FROM Courses WHERE CourseId = @id";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Count()
        {
            const string sql = "SELECT COUNT(*) FROM Courses";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}