using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Schema;

namespace Project_Drawing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            drawing = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
            Graphics.FromImage(drawing).Clear(Color.White);
        }

        bool mDown = false;
        Pen _pen;
        bool erase;
        int pX = -1;
        int pY = -1;
        readonly Bitmap drawing;


        private void Form1_Load(object sender, EventArgs e)
        {
            _pen = new Pen(Color.AliceBlue);
            cbbrushsize.SelectedIndex = 15;
        }

        private void btndraw_Click(object sender, EventArgs e)
        {
            erase = false;
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            colord.ShowDialog();
        }

        private void btnerase_Click(object sender, EventArgs e)
        {
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            panel1.CreateGraphics().FillRectangle(whiteBrush, panel1.ClientRectangle);

            Graphics g = Graphics.FromImage(drawing);
            g.Clear(Color.White);
        }

        private void btneraser_Click(object sender, EventArgs e)
        {
            erase = true;
        }

        private void cbbrushsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pen.Width = Convert.ToInt32(cbbrushsize.SelectedItem);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDown)
            {
                Graphics panel = Graphics.FromImage(drawing);


                _pen.EndCap = LineCap.Round;
                _pen.StartCap = LineCap.Round;
                _pen.Width = Convert.ToInt32(cbbrushsize.SelectedItem);


                _ = (erase) ? _pen.Color = Color.White : _pen.Color = colord.Color;


                panel.DrawLine(_pen, pX, pY, e.X, e.Y);

                panel1.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
            }

            pX = e.X;
            pY = e.Y;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mDown = true;

            pX = e.X;
            pY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mDown = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(drawing, new Point(0, 0));
        }
    }
}
