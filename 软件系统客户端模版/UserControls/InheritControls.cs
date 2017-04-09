using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace 软件系统客户端模版.UserControls
{
    public class ComboBoxEx : ComboBox
    {
        public ComboBoxEx()
        {
            InitializeComponent();
            DrawMode = DrawMode.OwnerDrawVariable;//手动绘制所有元素
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            Rectangle rec = e.Bounds;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.DrawBackground();
            
            if (Items[e.Index] is CommonLibrary.Account account)
            {
                //if (e.State == DrawItemState.Selected)
                //{
                //    //this code keeps the last item drawn from having a Bisque background. 
                //    //e.Graphics.FillRectangle(Brushes.Bisque, e.Bounds);

                //    e.Graphics.DrawString(account.姓名, Font, Brushes.White, rec);
                //    rec.Offset(100, 0);
                //    e.Graphics.DrawString(account.科名, Font, Brushes.White, rec);
                //}
                //else
                //{
                    e.Graphics.DrawString(account.姓名, Font, Brushes.Black, rec);
                    rec.Offset(100, 0);
                    e.Graphics.DrawString(account.科名, Font, Brushes.Gray, rec);
               // }
            }

            //e.DrawFocusRectangle();

        }



        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Text = "ComboBoxEx";
        }
    }
}
