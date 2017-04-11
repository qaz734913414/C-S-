using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 软件系统客户端模版
{
    public partial class FormMemberSelect : Form
    {
        public FormMemberSelect()
        {
            InitializeComponent();

            comboBoxEx1.DataSource = ModalProject.DepartmentAccounts.ToArray();
        }

        private void FormMemberSelect_Load(object sender, EventArgs e)
        {

        }

        private void userButton_add_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem is CommonLibrary.Account account)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == account.姓名)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = textBox1.Text;
                        return;
                    }
                }

                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = account.姓名;
                dataGridView1.Rows[index].Cells[1].Value = textBox1.Text;

                label1.Text = "总人数：" + dataGridView1.RowCount;
            }
        }

        private void userButton_delete_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
            label1.Text = "总人数：" + dataGridView1.RowCount;
        }

        /// <summary>
        /// 获取已经选择的成员数据
        /// </summary>
        /// <returns></returns>
        public CommonLibrary.ProjectMember GetSelectedMember()
        {
            CommonLibrary.ProjectMember mem = new CommonLibrary.ProjectMember();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                mem.ItemAdd(new CommonLibrary.Member()
                {
                    MemberName = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    TaskDistribution = dataGridView1.Rows[i].Cells[1].Value.ToString()
                });
            }
            return mem;
        }

        public void SetSelectedMember(CommonLibrary.ProjectMember mem)
        {
            foreach(var m in mem)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = m.MemberName;
                dataGridView1.Rows[index].Cells[1].Value = m.TaskDistribution;
            }
        }

        private void userButton_save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
