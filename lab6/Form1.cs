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
      
        EnterPoint ep;
        ExitPoint exp;
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
                X = 0,
                Y = picDisplay.Height / 2,

            };

            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся
                                        // добавил гравитон
                                        // привязываем гравитоны к полям
            exp = new ExitPoint
            {
                X = (float)(picDisplay.Width * 0.5),
                Y = picDisplay.Height / 2,
               
            };

            ep = new EnterPoint
            {
                exitPoint = exp,
                
                X = (float)(picDisplay.Width * 0.25),
                Y = picDisplay.Height / 2
            };

            emitter.impactPoints.Add(ep);
            emitter.impactPoints.Add(exp);

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
           
                exp.X = e.X;
                exp.Y = e.Y;
           

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            lblDirection.Text = $"Изменение угла на {tbDirection.Value}°";
            ep.Angle = tbDirection.Value;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbGraviton_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"Изменение угла разброса {tbGraviton.Value}°";
            emitter.Spreading = tbGraviton.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
            
//point1.Power = tbGraviton2.Value;
        }

        
    }
}
