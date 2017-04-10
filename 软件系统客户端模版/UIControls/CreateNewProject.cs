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

        //private void comboBox5_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Rectangle rec = e.Bounds;
        //    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        //    if ((e.State & DrawItemState.ComboBoxEdit) == 0)
        //    {
        //        e.DrawBackground();
        //    }
        //    else
        //    {
        //        e.Graphics.FillRectangle(SystemBrushes.Control, rec);
        //    }

        //    if (e.Index >= 0)
        //    {
        //        if (comboBox5.Items[e.Index] is CommonLibrary.Account account)
        //        {
        //            e.Graphics.DrawString(account.姓名, Font, Brushes.Black, rec);
        //            rec.Offset(80, 0);
        //            e.Graphics.DrawString(account.科名, Font, Brushes.Gray, rec);
        //        }
        //    }
        //    e.DrawFocusRectangle();
        //}
    }
}
