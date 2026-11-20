using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public partial class Form1 : Form
    {
        private int _selectedGradeId = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvGrades.ReadOnly = true;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.AllowUserToAddRows = false;

            LoadComboBoxes();
            LoadGrades();
        }

        private void LoadComboBoxes()
        {
            using (var con = new SqlConnection(Db.ConnStr))
            {
                var daStudents = new SqlDataAdapter(
                    "SELECT StudentId, FirstName + ' ' + LastName AS FullName FROM Students", con);
                var dtStudents = new DataTable();
                daStudents.Fill(dtStudents);

                cmbStudent.DisplayMember = "FullName";
                cmbStudent.ValueMember = "StudentId";
                cmbStudent.DataSource = dtStudents;

                var daCourses = new SqlDataAdapter(
                    "SELECT CourseId, CourseName FROM Courses", con);
                var dtCourses = new DataTable();
                daCourses.Fill(dtCourses);

                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseId";
                cmbCourse.DataSource = dtCourses;
            }
        }

        private void LoadGrades()
        {
            const string sql = @"
                SELECT 
                    g.GradeId,
                    s.FirstName + ' ' + s.LastName AS [Tələbə],
                    c.CourseName AS [Fənn],
                    g.Score AS [Qiymət]
                FROM Grades g
                INNER JOIN Students s ON g.StudentId = s.StudentId
                INNER JOIN Courses c ON g.CourseId = c.CourseId";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var da = new SqlDataAdapter(sql, con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvGrades.DataSource = dt;

                if (dgvGrades.Columns["GradeId"] != null)
                    dgvGrades.Columns["GradeId"].Visible = false;
            }

            CalculateAverage();
        }

        private void CalculateAverage()
        {
            if (cmbStudent.SelectedValue == null || cmbStudent.SelectedValue is DataRowView)
                return;

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            const string sql = "SELECT AVG(Score * 1.0) FROM Grades WHERE StudentId = @sid";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@sid", studentId);
                con.Open();
                object? result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    lblAverage.Text = "Orta bal: Qiymət yoxdur";
                }
                else
                {
                    decimal avg = Convert.ToDecimal(result);
                    lblAverage.Text = $"Orta bal: {avg:F2}";
                }
            }
        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAverage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtScore.Text, out int score) || score < 0 || score > 100)
            {
                MessageBox.Show("Qiymət 0 ilə 100 arasında tam ədəd olmalıdır!");
                return;
            }

            const string sql = "INSERT INTO Grades (StudentId, CourseId, Score) VALUES (@sid, @cid, @score)";

            try
            {
                using (var con = new SqlConnection(Db.ConnStr))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@sid", cmbStudent.SelectedValue);
                    cmd.Parameters.AddWithValue("@cid", cmbCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@score", score);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                txtScore.Clear();
                LoadGrades();
                MessageBox.Show("Qiymət əlavə olundu!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Baza xətası: " + ex.Message);
            }
        }

        private void dgvGrades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvGrades.Rows[e.RowIndex];
            _selectedGradeId = Convert.ToInt32(row.Cells["GradeId"].Value);
            cmbStudent.Text = row.Cells["Tələbə"].Value?.ToString();
            cmbCourse.Text = row.Cells["Fənn"].Value?.ToString();
            txtScore.Text = row.Cells["Qiymət"].Value?.ToString();

            CalculateAverage();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedGradeId == 0)
            {
                MessageBox.Show("Əvvəlcə cədvəldən dəyişmək istədiyiniz sətri seçin!");
                return;
            }

            if (!int.TryParse(txtScore.Text, out int score) || score < 0 || score > 100)
            {
                MessageBox.Show("Qiymət 0 ilə 100 arasında olmalıdır!");
                return;
            }

            const string sql = "UPDATE Grades SET StudentId = @sid, CourseId = @cid, Score = @score WHERE GradeId = @gid";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@sid", cmbStudent.SelectedValue);
                cmd.Parameters.AddWithValue("@cid", cmbCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.Parameters.AddWithValue("@gid", _selectedGradeId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            _selectedGradeId = 0;
            txtScore.Clear();
            LoadGrades();
            MessageBox.Show("Qiymət yeniləndi!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedGradeId == 0)
            {
                MessageBox.Show("Əvvəlcə cədvəldən silmək istədiyiniz sətri seçin!");
                return;
            }

            var confirm = MessageBox.Show("Bu qiyməti silmək istədiyinizə əminsiniz?", "Təsdiq",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            const string sql = "DELETE FROM Grades WHERE GradeId = @gid";

            using (var con = new SqlConnection(Db.ConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@gid", _selectedGradeId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            _selectedGradeId = 0;
            txtScore.Clear();
            LoadGrades();
            MessageBox.Show("Qiymət silindi!");
        }

        private void cmbStudent_SelectedIndexChanged_1(object sender, EventArgs e) => cmbStudent_SelectedIndexChanged(sender, e);
    }
}