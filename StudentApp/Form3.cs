using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public partial class Form3 : Form
    {
        private readonly StudentRepository _repo = new StudentRepository();

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AllowUserToAddRows = false;

            LoadStudents();
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = _repo.GetAll();
        }

        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            txtEmail.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Ad və soyad boş ola bilməz!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 15 || age > 100)
            {
                MessageBox.Show("Yaşı düzgün tam ədəd kimi daxil edin!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-poçt ünvanını daxil edin!");
                return;
            }

            try
            {
                _repo.Add(new Student
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = age,
                    Email = txtEmail.Text.Trim()
                });

                ClearInputs();
                LoadStudents();
                MessageBox.Show("Tələbə uğurla əlavə olundu!");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show("Bu e-poçt artıq mövcuddur");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Verilənlər bazası xətası: " + ex.Message);
            }
        }
    }
}