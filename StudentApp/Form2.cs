using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public partial class Form2 : Form
    {
        private readonly CourseRepository _repo = new CourseRepository();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dgvCourses.ReadOnly = true;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.AllowUserToAddRows = false;

            LoadCourses();
        }

        private void LoadCourses()
        {
            dgvCourses.DataSource = _repo.GetAll();
            lblCount.Text = $"Cəmi fənn sayı: {_repo.Count()}";
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtTitle.Clear();
            txtCredits.Clear();
        }

        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var c = (Course)dgvCourses.Rows[e.RowIndex].DataBoundItem;
            txtId.Text = c.Id.ToString();
            txtTitle.Text = c.Title;
            txtCredits.Text = c.Credits.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Fənnin adını daxil edin!");
                return;
            }

            if (!int.TryParse(txtCredits.Text, out int credits) || credits < 1 || credits > 10)
            {
                MessageBox.Show("Kredit 1 ilə 10 arasında tam ədəd olmalıdır!");
                return;
            }

            try
            {
                _repo.Add(new Course
                {
                    Title = txtTitle.Text.Trim(),
                    Credits = credits
                });

                ClearInputs();
                LoadCourses();
                MessageBox.Show("Fənn əlavə olundu!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Baza xətası: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Əvvəlcə cədvəldən fənn seçin!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Fənnin adını daxil edin!");
                return;
            }

            if (!int.TryParse(txtCredits.Text, out int credits) || credits < 1 || credits > 10)
            {
                MessageBox.Show("Kredit 1 ilə 10 arasında tam ədəd olmalıdır!");
                return;
            }

            try
            {
                _repo.Update(new Course
                {
                    Id = id,
                    Title = txtTitle.Text.Trim(),
                    Credits = credits
                });

                ClearInputs();
                LoadCourses();
                MessageBox.Show("Fənn yeniləndi!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Baza xətası: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Əvvəlcə cədvəldən silmək istədiyiniz fənni seçin!");
                return;
            }

            var confirm = MessageBox.Show("Bu fənni silmək istədiyinizə əminsiniz?", "Təsdiq",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _repo.Delete(id);
                ClearInputs();
                LoadCourses();
                MessageBox.Show("Fənn silindi!");
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Bu fənn üzrə tələbələrin qiymətləri var, əvvəlcə qiymətləri silin!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Verilənlər bazası xətası: " + ex.Message);
            }
        }

        private void Form2_Load_1(object sender, EventArgs e) => Form2_Load(sender, e);
        private void btnAdd_Click_1(object sender, EventArgs e) => btnAdd_Click(sender, e);
        private void btnUpdate_Click_1(object sender, EventArgs e) => btnUpdate_Click(sender, e);
        private void btnDelete_Click_1(object sender, EventArgs e) => btnDelete_Click(sender, e);
    }
}