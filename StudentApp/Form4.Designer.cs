namespace StudentApp
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtTitle = new TextBox();
            label2 = new Label();
            txtYear = new TextBox();
            label3 = new Label();
            cmbAuthor = new ComboBox();
            label4 = new Label();
            txtPrice = new TextBox();
            btnBookAdd = new Button();
            btnBookUpdate = new Button();
            btnBookDelete = new Button();
            dgvBooks = new DataGridView();
            label5 = new Label();
            cmbLoanBook = new ComboBox();
            label6 = new Label();
            cmbLoanMember = new ComboBox();
            btnLoan = new Button();
            btnReturn = new Button();
            label7 = new Label();
            dgvLoans = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLoans).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 0;
            label1.Text = "Kitab adı:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(74, 6);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(194, 23);
            txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 50);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 2;
            label2.Text = "Nəşr ili:";
            // 
            // txtYear
            // 
            txtYear.Location = new Point(74, 47);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(194, 23);
            txtYear.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 92);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 4;
            label3.Text = "Müəllif:";
            // 
            // cmbAuthor
            // 
            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthor.FormattingEnabled = true;
            cmbAuthor.Location = new Point(74, 89);
            cmbAuthor.Name = "cmbAuthor";
            cmbAuthor.Size = new Size(194, 23);
            cmbAuthor.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 135);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 6;
            label4.Text = "Qiymət:";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(74, 132);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(194, 23);
            txtPrice.TabIndex = 7;
            // 
            // btnBookAdd
            // 
            btnBookAdd.Location = new Point(37, 176);
            btnBookAdd.Name = "btnBookAdd";
            btnBookAdd.Size = new Size(75, 23);
            btnBookAdd.TabIndex = 8;
            btnBookAdd.Text = "Əlavə et";
            btnBookAdd.UseVisualStyleBackColor = true;
            btnBookAdd.Click += btnBookAdd_Click;
            // 
            // btnBookUpdate
            // 
            btnBookUpdate.Location = new Point(118, 176);
            btnBookUpdate.Name = "btnBookUpdate";
            btnBookUpdate.Size = new Size(75, 23);
            btnBookUpdate.TabIndex = 9;
            btnBookUpdate.Text = "Yenilə";
            btnBookUpdate.UseVisualStyleBackColor = true;
            btnBookUpdate.Click += btnBookUpdate_Click;
            // 
            // btnBookDelete
            // 
            btnBookDelete.Location = new Point(199, 176);
            btnBookDelete.Name = "btnBookDelete";
            btnBookDelete.Size = new Size(75, 23);
            btnBookDelete.TabIndex = 10;
            btnBookDelete.Text = "Sil";
            btnBookDelete.UseVisualStyleBackColor = true;
            btnBookDelete.Click += btnBookDelete_Click;
            // 
            // dgvBooks
            // 
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(12, 216);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.Size = new Size(492, 294);
            dgvBooks.TabIndex = 11;
            dgvBooks.CellClick += dgvBooks_CellClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(455, 9);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 12;
            label5.Text = "Kitab:";
            // 
            // cmbLoanBook
            // 
            cmbLoanBook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoanBook.FormattingEnabled = true;
            cmbLoanBook.Location = new Point(498, 6);
            cmbLoanBook.Name = "cmbLoanBook";
            cmbLoanBook.Size = new Size(194, 23);
            cmbLoanBook.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(455, 50);
            label6.Name = "label6";
            label6.Size = new Size(29, 15);
            label6.TabIndex = 14;
            label6.Text = "Üzv:";
            // 
            // cmbLoanMember
            // 
            cmbLoanMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoanMember.FormattingEnabled = true;
            cmbLoanMember.Location = new Point(498, 47);
            cmbLoanMember.Name = "cmbLoanMember";
            cmbLoanMember.Size = new Size(194, 23);
            cmbLoanMember.TabIndex = 15;
            // 
            // btnLoan
            // 
            btnLoan.Location = new Point(536, 84);
            btnLoan.Name = "btnLoan";
            btnLoan.Size = new Size(75, 23);
            btnLoan.TabIndex = 16;
            btnLoan.Text = "İcarəyə ver";
            btnLoan.UseVisualStyleBackColor = true;
            btnLoan.Click += btnLoan_Click;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(617, 84);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(75, 23);
            btnReturn.TabIndex = 17;
            btnReturn.Text = "Qaytar";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(536, 198);
            label7.Name = "label7";
            label7.Size = new Size(129, 15);
            label7.TabIndex = 18;
            label7.Text = "Qaytarılmamış kitablar:";
            // 
            // dgvLoans
            // 
            dgvLoans.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoans.Location = new Point(536, 216);
            dgvLoans.Name = "dgvLoans";
            dgvLoans.Size = new Size(463, 294);
            dgvLoans.TabIndex = 19;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 522);
            Controls.Add(dgvLoans);
            Controls.Add(label7);
            Controls.Add(btnReturn);
            Controls.Add(btnLoan);
            Controls.Add(cmbLoanMember);
            Controls.Add(label6);
            Controls.Add(cmbLoanBook);
            Controls.Add(label5);
            Controls.Add(dgvBooks);
            Controls.Add(btnBookDelete);
            Controls.Add(btnBookUpdate);
            Controls.Add(btnBookAdd);
            Controls.Add(txtPrice);
            Controls.Add(label4);
            Controls.Add(cmbAuthor);
            Controls.Add(label3);
            Controls.Add(txtYear);
            Controls.Add(label2);
            Controls.Add(txtTitle);
            Controls.Add(label1);
            Name = "Form4";
            Text = "Form4";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLoans).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTitle;
        private Label label2;
        private TextBox txtYear;
        private Label label3;
        private ComboBox cmbAuthor;
        private Label label4;
        private TextBox txtPrice;
        private Button btnBookAdd;
        private Button btnBookUpdate;
        private Button btnBookDelete;
        private DataGridView dgvBooks;
        private Label label5;
        private ComboBox cmbLoanBook;
        private Label label6;
        private ComboBox cmbLoanMember;
        private Button btnLoan;
        private Button btnReturn;
        private Label label7;
        private DataGridView dgvLoans;
    }
}