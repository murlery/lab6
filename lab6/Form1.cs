using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab6
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();

        Emitter emitter;
      
        DogHeadPoint hP;
        DogPopaPoint pP;
        foodPoint fP;
      
        Random rnd;
        public Form1()
        {
            InitializeComponent();
            //picDisplay.MouseWheel += pickDisplay_MouseWheel;
            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new Emitter
            {
                Direction = 0,
                Spreading = 0,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Brown,
                ColorTo = Color.FromArgb(0, Color.SandyBrown),
                ParticlesPerTick = 30,
                X = (int)(picDisplay.Width-(picDisplay.Width * 0.9)),
                Y = (picDisplay.Height / 3)-40,

            };

            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся
                                        // добавил гравитон
                                        // привязываем гравитоны к полям
            pP = new DogPopaPoint
            {
                X = (float)(picDisplay.Width * 0.7),
                Y = picDisplay.Height / 2,

            };
            fP = new foodPoint
            {
                X = (float)(picDisplay.Width * 0.1),
                Y = picDisplay.Height / 3,

            };


            hP = new DogHeadPoint
            {
                popaPoint = pP,
                
                X = (float)((picDisplay.Width * 0.45)),
                Y = picDisplay.Height / 5
            };


            emitter.impactPoints.Add(hP);
            emitter.impactPoints.Add(fP);
            emitter.impactPoints.Add(pP);
           


        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        // добавил функцию обновления состояния системы

        private void timer1_Tick(object sender, EventArgs e)
        {

            emitter.UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.MistyRose); // А ЕЩЕ ЧЕРНЫЙ ФОН СДЕЛАЮ
                emitter.Render(g);
            }
            picDisplay.Invalidate();
        }
       
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
           
                pP.X = e.X;
                pP.Y = e.Y;
           

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"{tbDirection.Value}°";
            emitter.Direction = tbDirection.Value; 
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbGraviton_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"{tbGraviton.Value}°";
            emitter.Spreading = tbGraviton.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
            
//point1.Power = tbGraviton2.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pP.Y = picDisplay.Height / 2 - picDisplay.Height / 40 * tbDirection.Value;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void picDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                hP.X = e.X;
                hP.Y = e.Y;
            }
            if (e.Button == MouseButtons.Right)
            {
                pP.X = e.X;
                pP.Y = e.Y;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
