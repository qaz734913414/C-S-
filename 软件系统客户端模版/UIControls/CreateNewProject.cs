using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 软件系统客户端模版.UIControls
{
    public partial class CreateNewProject : UserControl
    {
        public CreateNewProject()
        {
            InitializeComponent();

            comboBox1.DataSource = CommonLibrary.BasicOptions.Belongs;
            comboBox2.DataSource = CommonLibrary.BasicOptions.ProjectGrade;
            comboBox4.DataSource = CommonLibrary.BasicOptions.ProjectCategroy;
            ModalProject.DepartmentAccounts.Sort((a1, a2) => { return a1.科名.CompareTo(a2.科名); });
            comboBoxEx1.DataSource = ModalProject.DepartmentAccounts.ToArray();
        }

        private void CreateNewProject_Load(object sender, EventArgs e)
        {
            textBox_CurrentNode.TextChanged += delegate
            {
                label_text_length.Text = textBox_CurrentNode.Text.Length.ToString();
            };

            SizeChanged += delegate
            {
                if (Width > 0)
                {
                    userButton1.Location = new Point(Width / 2 - userButton1.Width / 2, userButton1.Location.Y);
                }
            };

            comboBox1.SelectedIndexChanged += delegate
            {
                comboBoxEx1.DataSource = ModalProject.DepartmentAccounts.Where(p=>p.科名==comboBox1.SelectedItem.ToString()).ToArray();
            };
        }

        private void userButton_login_Click(object sender, EventArgs e)
        {
            using (FormMemberSelect fms = new FormMemberSelect())
            {
                if (!string.IsNullOrEmpty(textBox3.Text)) fms.SetSelectedMember(CurrentSelected);
                if (fms.ShowDialog() == DialogResult.OK)
                {
                    CurrentSelected = fms.GetSelectedMember();
                    textBox3.Text = CurrentSelected.ToShowCreate();
                }
            }
        }

        private CommonLibrary.ProjectMember CurrentSelected { get; set; } = new CommonLibrary.ProjectMember();

        private void userButton1_Click(object sender, EventArgs e)
        {
            //第一步验证输入的数据
            if (textBox2.Text.Length < 5)
            {
                MessageBox.Show("项目名称长度不允许小于5个字。");
                return;
            }
            if (textBox2.Text.Length > 80)
            {
                MessageBox.Show("项目名称长度不允许大于80个字。");
                return;
            }
            if (dateTimePicker2.Value < dateTimePicker1.Value.Date)
            {
                MessageBox.Show("项目计划完成时间不能小于项目开始时间");
                return;
            }
            if (!int.TryParse(textBox4.Text, out int progress))
            {
                MessageBox.Show("项目进度的数据必须为数字，您输入的格式不正确！");
                return;
            }
            if (progress < 0 || progress > 100)
            {
                MessageBox.Show("项目进度的必须为0-100，您输入的范围不正确！");
                return;
            }
            if (textBox_CurrentNode.Text.Length > 200)
            {
                MessageBox.Show("项目当前节点的文字长度不能超过200字！");
                return;
            }

            //创建新项目
            CommonLibrary.Project project = new CommonLibrary.Project();
            project.ProjectId = textBox1.Text;
            project.PorjectName = textBox2.Text;
            project.ProjectDepartment = ((CommonLibrary.BasicOptions)comboBox1.SelectedItem).Description;
            project.ProjectPriority = ((CommonLibrary.BasicOptions)comboBox2.SelectedItem).IntegerCode;
            project.ProjectCategory = ((CommonLibrary.BasicOptions)comboBox4.SelectedItem).IntegerCode;
            project.Leader = ((CommonLibrary.Account)comboBox4.SelectedItem).姓名;
            project.Members = CurrentSelected;
            project.Progress = progress;
            project.CurrentNode = textBox_CurrentNode.Text;

            try
            {
                if (project.InsertSqlDatabase()==1)
                {
                    MessageBox.Show("项目新增成功！");
                }
                else
                {
                    MessageBox.Show("由于未知原因，项目新增失败！");
                }
            }
            catch(Exception ex)
            {
                BasicFramework.SoftBasic.ShowExceptionMessage(ex);
            }
        }
    }
}
