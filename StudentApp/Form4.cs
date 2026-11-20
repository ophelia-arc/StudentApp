using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace StudentApp
{
    public partial class Form4 : Form
    {
        private int _selectedBookId = 0;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dgvBooks.ReadOnly = true;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.AllowUserToAddRows = false;

            dgvLoans.ReadOnly = true;
            dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoans.AllowUserToAddRows = false;

            LoadComboBoxes();
            LoadBooks();
            LoadActiveLoans();
        }

        private void LoadComboBoxes()
        {
            using (var con = new SqlConnection(Db.LibraryConnStr))
            {
                var daAuthors = new SqlDataAdapter("SELECT AuthorID, FullName FROM Authors", con);
                var dtAuthors = new DataTable();
                daAuthors.Fill(dtAuthors);
                cmbAuthor.DisplayMember = "FullName";
                cmbAuthor.ValueMember = "AuthorID";
                cmbAuthor.DataSource = dtAuthors;

                var daBooks = new SqlDataAdapter("SELECT BookID, Title FROM Books", con);
                var dtBooks = new DataTable();
                daBooks.Fill(dtBooks);
                cmbLoanBook.DisplayMember = "Title";
                cmbLoanBook.ValueMember = "BookID";
                cmbLoanBook.DataSource = dtBooks;

                var daMembers = new SqlDataAdapter("SELECT MemberID, FullName FROM Members", con);
                var dtMembers = new DataTable();
                daMembers.Fill(dtMembers);
                cmbLoanMember.DisplayMember = "FullName";
                cmbLoanMember.ValueMember = "MemberID";
                cmbLoanMember.DataSource = dtMembers;
            }
        }

        private void LoadBooks()
        {
            const string sql = @"
                SELECT 
                    b.BookID,
                    b.Title AS [Kitab],
                    b.PublishYear AS [Nəşr ili],
                    a.FullName AS [Müəllif],
                    b.Price AS [Qiymət]
                FROM Books b
                INNER JOIN Authors a ON b.AuthorID = a.AuthorID";

            using (var con = new SqlConnection(Db.LibraryConnStr))
            using (var da = new SqlDataAdapter(sql, con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvBooks.DataSource = dt;
            }
        }

        private void LoadActiveLoans()
        {
            const string sql = @"
                SELECT 
                    l.LoanID,
                    b.Title AS [Kitab],
                    m.FullName AS [Üzv],
                    l.LoandDate AS [Verilmə tarixi]
                FROM Loans l
                INNER JOIN Books b ON l.BookID = b.BookID
                INNER JOIN Members m ON l.MemberID = m.MemberID
                WHERE l.ReturnDate IS NULL";

            using (var con = new SqlConnection(Db.LibraryConnStr))
            using (var da = new SqlDataAdapter(sql, con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvLoans.DataSource = dt;
            }
        }

        private void ClearBookInputs()
        {
            _selectedBookId = 0;
            txtTitle.Clear();
            txtYear.Clear();
            txtPrice.Clear();
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBooks.Rows[e.RowIndex];
            _selectedBookId = Convert.ToInt32(row.Cells["BookID"].Value);
            txtTitle.Text = row.Cells["Kitab"].Value?.ToString();
            txtYear.Text = row.Cells["Nəşr ili"].Value?.ToString();
            cmbAuthor.Text = row.Cells["Müəllif"].Value?.ToString();
            txtPrice.Text = row.Cells["Qiymət"].Value?.ToString();
        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                !int.TryParse(txtYear.Text, out int year) ||
                !decimal.TryParse(txtPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) ||
                price <= 0)
            {
                MessageBox.Show("Kitabın adını, nəşr ilini və müsbət qiymətini düzgün daxil edin!");
                return;
            }

            const string sql = "INSERT INTO Books (Title, PublishYear, AuthorID, Price) VALUES (@title, @year, @aid, @price)";

            using (var con = new SqlConnection(Db.LibraryConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@aid", cmbAuthor.SelectedValue);
                cmd.Parameters.AddWithValue("@price", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            ClearBookInputs();
            LoadBooks();
            LoadComboBoxes();
            MessageBox.Show("Kitab əlavə olundu!");
        }

        private void btnBookUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBookId == 0)
            {
                MessageBox.Show("Əvvəlcə cədvəldən kitab seçin!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                !int.TryParse(txtYear.Text, out int year) ||
                !decimal.TryParse(txtPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) ||
                price <= 0)
            {
                MessageBox.Show("Məlumatları düzgün daxil edin!");
                return;
            }

            const string sql = @"UPDATE Books 
                                 SET Title = @title, PublishYear = @year, AuthorID = @aid, Price = @price 
                                 WHERE BookID = @id";

            using (var con = new SqlConnection(Db.LibraryConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@aid", cmbAuthor.SelectedValue);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@id", _selectedBookId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            ClearBookInputs();
            LoadBooks();
            LoadActiveLoans();
            LoadComboBoxes();
            MessageBox.Show("Kitab yeniləndi!");
        }

        private void btnBookDelete_Click(object sender, EventArgs e)
        {
            if (_selectedBookId == 0)
            {
                MessageBox.Show("Əvvəlcə cədvəldən silmək istədiyiniz kitabı seçin!");
                return;
            }

            const string sql = "DELETE FROM Books WHERE BookID = @id";

            try
            {
                using (var con = new SqlConnection(Db.LibraryConnStr))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", _selectedBookId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearBookInputs();
                LoadBooks();
                LoadComboBoxes();
                MessageBox.Show("Kitab silindi!");
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Bu kitab icarə tarixçəsində olduğu üçün silinə bilməz!");
            }
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            if (cmbLoanBook.SelectedValue == null || cmbLoanMember.SelectedValue == null)
                return;

            using (var con = new SqlConnection(Db.LibraryConnStr))
            {
                con.Open();

                const string checkSql = "SELECT COUNT(*) FROM Loans WHERE BookID = @bid AND ReturnDate IS NULL";
                using (var checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@bid", cmbLoanBook.SelectedValue);
                    int activeCount = (int)checkCmd.ExecuteScalar();
                    if (activeCount > 0)
                    {
                        MessageBox.Show("Bu kitab hazırda icarədədir, əvvəlcə qaytarılmalıdır!");
                        return;
                    }
                }

                const string sql = "INSERT INTO Loans (BookID, MemberID, LoandDate) VALUES (@bid, @mid, GETDATE())";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@bid", cmbLoanBook.SelectedValue);
                    cmd.Parameters.AddWithValue("@mid", cmbLoanMember.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadActiveLoans();
            MessageBox.Show("Kitab icarəyə verildi!");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvLoans.CurrentRow == null)
            {
                MessageBox.Show("Qaytarmaq üçün sağdakı siyahıdan bir icarə seçin!");
                return;
            }

            int loanId = Convert.ToInt32(dgvLoans.CurrentRow.Cells["LoanID"].Value);
            const string sql = "UPDATE Loans SET ReturnDate = GETDATE() WHERE LoanID = @lid";

            using (var con = new SqlConnection(Db.LibraryConnStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@lid", loanId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadActiveLoans();
            MessageBox.Show("Kitab qaytarıldı!");
        }
    }
}