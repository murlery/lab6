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
        public int Power = 100;
        public float X; // ну точка же, вот и две координаты
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(Particle particle);

        // базовый класс для отрисовки точечки
        public abstract void Render(Graphics g);
        
    }
    public class DogHeadPoint : DogHead
    {
        public DogPopaPoint popaPoint;
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

                    p.X = popaPoint.X - points[0].X;
                    p.Y = popaPoint.Y - points[0].Y;
                    p.SpeedX = points[1].X;
                    p.SpeedY = points[1].Y;
                }

            }
        }
        public override void Render(Graphics g)
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

    public class DogPopaPoint : DogHead
    {
        
        public int Angle = 0;
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
        }
        public override void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\popa.png");
            // буду рисовать окружность с диаметром равным Power
            g.DrawImage(
                   image,
                   (X - Power / 2)-140 ,
                   Y - Power / 2,
                   Power*2,
                   Power*2
               );

           
        }
    }
    public class foodPoint : DogHead
    {
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {

        }
        public override void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\food.png");
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


}
