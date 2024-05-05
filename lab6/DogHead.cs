using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public abstract class DogHead
    {
        public int Power = 100; // сила притяжения
        public float X; // ну точка же, вот и две координаты
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(Particle particle);

        // базовый класс для отрисовки точечки
        public virtual void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\dogH.png");
            // буду рисовать окружность с диаметром равным Power
            g.DrawImage(
                   image,
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );
        }
    }
    public class EnterPoint : DogHead
    {
        public int Power = 100; // сила притяжения
        public ExitPoint exitPoint;
        public int Angle = 0;
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            if (r + particle.Radius < 100 / 2)
            {
                if (particle is ParticleColorful)
                {
                    var p = (particle as ParticleColorful);

                    var m = new Matrix();
                    m.Rotate(Angle);

                    var points = new[] { new PointF(gX, gY), new PointF(p.SpeedX, p.SpeedY) };
                    m.TransformPoints(points);

                    p.X = exitPoint.X - points[0].X;
                    p.Y = exitPoint.Y - points[0].Y;
                    p.SpeedX = points[1].X;
                    p.SpeedY = points[1].Y;
                }

            }
        }
        public override void Render(Graphics g)
        {
            base.Render(g);

            g.DrawString(
            $"Вход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Black),
            X,
            Y
            );
        }

          
           

          
        
    }

    public class ExitPoint : DogHead
    {
        public int Power = 100; // сила отторжения

        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {

        }
        public override void Render(Graphics g)
        {
            base.Render(g);

            g.DrawString(
            $"Выход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Black),
            X,
            Y
            );
        }
    }
}
