﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 软件系统客户端模版
{
    public partial class CreateNewProject : UserControl
    {
        public CreateNewProject()
        {
            InitializeComponent();
        }

        private void CreateNewProject_Load(object sender, EventArgs e)
        {
            textBox_CurrentNode.TextChanged += delegate
            {
                label_text_length.Text = textBox_CurrentNode.Text.Length.ToString();
            };


        }
        
    }
}
