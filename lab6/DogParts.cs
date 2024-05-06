using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public abstract class DogParts
    {
        public int Size = 100;
        public float X; // ну точка же, вот и две координаты
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(Particle particle);

        // базовый класс для отрисовки точечки
        public abstract void Render(Graphics g);
        
    }
    public class DogHeadPoint : DogParts
    {
        public DogPopaPoint popaPoint;
        public int Angle = 0;
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
            //Вычисление положения частицы относительно головы собаки
            float rX = X - particle.X;
            float rY = Y - particle.Y;


            //Вычисление расстояния между частицами, спользуется формула расстояния между
            //двумя точками в двухмерном пространстве
            double R = Math.Sqrt(rX * rX + rY * rY);
            
            //положение точки в пространстве + радиус частицы меньше ли чем радиус головы собаки
            if (R + particle.Radius < Size / 2)
            {
                if (particle is ParticleColorful)
                {
                    var p = (particle as ParticleColorful);

                    //Создается новая матрица m и производится ее вращение на угол Angle
                    var m = new Matrix();
                    m.Rotate(Angle);

                    //Преобразуем относительное положение и вектор скорости частицы с помощью матрицы
                    var points = new[] { new PointF(rX, rY), new PointF(p.SpeedX, p.SpeedY) };
                    m.TransformPoints(points);

                    //Вычисляем новые координаты положения частицы, вычитая преобразованное относительное
                    //положение из исходного положения объекта
                    p.X = popaPoint.X - points[0].X;
                    p.Y = popaPoint.Y - points[0].Y;

                    //Вычисляем новый вектор скорости частицы, присваивая преобразованный вектор скорости из матрицы
                    p.SpeedX = points[1].X;
                    p.SpeedY = points[1].Y;
                }

            }
        }
        public override void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\dogH.png");
            
            // картинка с размером
            g.DrawImage(
                   image,
                   X - Size / 2,
                   Y - Size / 2,
                   Size,
                   Size
               );
        }        
    }

    public class DogPopaPoint : DogParts
    {
        
        public int Angle = 0;
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
        }
        public override void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\popa.png");
            // картинка с размером
            g.DrawImage(
                   image,
                   (X - Size / 2)-140 ,
                   Y - Size / 2,
                   Size*2,
                   Size*2
               );

           
        }
    }
    public class foodPoint : DogParts
    {
        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {

        }
        public override void Render(Graphics g)
        {
            System.Drawing.Image image = Image.FromFile("D:\\учебка\\Технология программирования\\lab6\\lab6\\food.png");
            // картинка с размером
            g.DrawImage(
                   image,
                   X - Size / 2,
                   Y - Size / 2,
                   Size,
                   Size
               );

           
        }
    }


}
