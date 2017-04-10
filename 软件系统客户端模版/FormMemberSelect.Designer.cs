namespace 软件系统客户端模版
{
    partial class FormMemberSelect
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.userButton_add = new BasicFramework.UserButton();
            this.userButton_delete = new BasicFramework.UserButton();
            this.userButton_save = new BasicFramework.UserButton();
            this.comboBoxEx1 = new 软件系统客户端模版.UserControls.ComboBoxEx();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.MemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemberTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MemberName,
            this.MemberTask});
            this.dataGridView1.Location = new System.Drawing.Point(12, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(741, 334);
            this.dataGridView1.TabIndex = 0;
            // 
            // userButton_add
            // 
            this.userButton_add.BackColor = System.Drawing.Color.Transparent;
            this.userButton_add.CustomerInformation = "";
            this.userButton_add.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.userButton_add.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.userButton_add.Location = new System.Drawing.Point(669, 11);
            this.userButton_add.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.userButton_add.Name = "userButton_add";
            this.userButton_add.Size = new System.Drawing.Size(84, 27);
            this.userButton_add.TabIndex = 7;
            this.userButton_add.UIText = "增加";
            this.userButton_add.Click += new System.EventHandler(this.userButton_add_Click);
            // 
            // userButton_delete
            // 
            this.userButton_delete.BackColor = System.Drawing.Color.Transparent;
            this.userButton_delete.CustomerInformation = "";
            this.userButton_delete.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.userButton_delete.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.userButton_delete.Location = new System.Drawing.Point(235, 394);
            this.userButton_delete.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.userButton_delete.Name = "userButton_delete";
            this.userButton_delete.Size = new System.Drawing.Size(84, 27);
            this.userButton_delete.TabIndex = 8;
            this.userButton_delete.UIText = "选中删除";
            this.userButton_delete.Click += new System.EventHandler(this.userButton_delete_Click);
            // 
            // userButton_save
            // 
            this.userButton_save.BackColor = System.Drawing.Color.Transparent;
            this.userButton_save.CustomerInformation = "";
            this.userButton_save.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.userButton_save.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.userButton_save.Location = new System.Drawing.Point(415, 394);
            this.userButton_save.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.userButton_save.Name = "userButton_save";
            this.userButton_save.Size = new System.Drawing.Size(84, 27);
            this.userButton_save.TabIndex = 9;
            this.userButton_save.UIText = "确认保存";
            this.userButton_save.Click += new System.EventHandler(this.userButton_save_Click);
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.Location = new System.Drawing.Point(12, 13);
            this.comboBoxEx1.MaxDropDownItems = 20;
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(179, 22);
            this.comboBoxEx1.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(197, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(466, 21);
            this.textBox1.TabIndex = 11;
            // 
            // MemberName
            // 
            this.MemberName.HeaderText = "姓名";
            this.MemberName.Name = "MemberName";
            this.MemberName.ReadOnly = true;
            this.MemberName.Width = 200;
            // 
            // MemberTask
            // 
            this.MemberTask.HeaderText = "工作内容";
            this.MemberTask.Name = "MemberTask";
            this.MemberTask.ReadOnly = true;
            this.MemberTask.Width = 480;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "总人数：";
            // 
            // FormMemberSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 434);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.userButton_save);
            this.Controls.Add(this.userButton_delete);
            this.Controls.Add(this.userButton_add);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormMemberSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "成员选择";
            this.Load += new System.EventHandler(this.FormMemberSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private BasicFramework.UserButton userButton_add;
        private BasicFramework.UserButton userButton_delete;
        private BasicFramework.UserButton userButton_save;
        private UserControls.ComboBoxEx comboBoxEx1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberTask;
        private System.Windows.Forms.Label label1;
    }
}