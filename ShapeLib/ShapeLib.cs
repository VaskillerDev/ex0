using System;
using NUnit.Framework;

/*
Задание:
Напишите на C# библиотеку для поставки внешним клиентам, которая умеет вычислять площадь круга по радиусу и треугольника по трем сторонам.
Дополнительно к работоспособности оценим:
    * Юнит-тесты
    * Легкость добавления других фигур
    * Вычисление площади фигуры без знания типа фигуры
    * Проверку на то, является ли треугольник прямоугольным

 Замечание: "Вычисление площади фигуры без знания типа фигуры" - непонятно каким образом вычислять фигуру в таком случае.
 Оставил вольность на совесть абстрактного класса Shape.
 Удобства ради, классы объявлены в одном файле.
*/
namespace ShapeLib
{
    internal abstract class Shape
    {
        protected abstract bool IsValidShape();

        public virtual Result<float> CalculateArea()
        {
            if (!IsValidShape()) return new Result<float>();
            return -1;
        }
    }

    internal class Circle : Shape
    {
        private readonly float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        protected override bool IsValidShape()
        {
            return _radius >= 0;
        }

        public override Result<float> CalculateArea()
        {
            var result = base.CalculateArea();
            if (result.IsNone()) return result;

            return (float) (Math.PI * Math.Pow(_radius, 2));
        }
    }

    internal class Triangle : Shape
    {
        private readonly float _sideA;
        private readonly float _sideB;
        private readonly float _sideC;

        public Triangle(float sideA, float sideB, float sideC)
        {
            _sideA = sideA;
            _sideB = sideB;
            _sideC = sideC;
        }

        protected override bool IsValidShape() => _sideA >= 0 && _sideB >= 0 && _sideC >= 0;

        public bool IsRectangular()
        {
            return CalcByPythagorean(_sideA, _sideB, _sideC)
                   || CalcByPythagorean(_sideB, _sideC, _sideA)
                   || CalcByPythagorean(_sideC, _sideA, _sideB);
        }

        private bool CalcByPythagorean(float a, float b, float c)
        {
            const double rate = 0.2;
            var a2 = Math.Pow(a, 2);
            var b2 = Math.Pow(b, 2);
            var c2 = Math.Pow(c, 2);

            return Math.Abs(c2 - (a2 + b2)) < rate;
        }

        public override Result<float> CalculateArea() // √p(p - a)(p - b)(p - c)
        {
            var result = base.CalculateArea();
            if (result.IsNone()) return result;

            var halfP = (_sideA + _sideB + _sideC) * 0.5f;
            var area = (float) Math.Sqrt(halfP * (halfP - _sideA) * (halfP - _sideB) * (halfP - _sideC));

            var isNotPossibleTriangle = double.IsNaN(area);
            return isNotPossibleTriangle ? new Result<float>() : area;
        }
    }

    internal class Square : Shape
    {
        private readonly float _side;

        public Square(float side) => _side = side;

        protected override bool IsValidShape() => _side >= 0;

        public override Result<float> CalculateArea()
        {
            var result = base.CalculateArea();
            if (result.IsNone()) return result;

            return _side * _side;
        }
    }

    public class ShapeLibTest
    {
        [Test]
        public void CalculateAreaTest()
        {
            {
                var circle = new Circle(4f);
                var result = circle.CalculateArea();
                Assert.AreEqual(result.IsSome(), true);
                Assert.AreEqual(result.Get(), 50.2654839f);
            }
            {
                // rectangular triangle
                var triangle = new Triangle(3, 4, 5);
                var result = triangle.CalculateArea();
                Assert.AreEqual(result.IsSome(), true);
                Assert.AreEqual(result.Get(), 6f);
                Assert.AreEqual(triangle.IsRectangular(), true);
            }
            {
                // impossible triangle
                var triangle = new Triangle(3, 4, 18);
                var result = triangle.CalculateArea();
                Assert.AreEqual(result.IsSome(), false);
            }
            {
                var square = new Square(5);
                var result = square.CalculateArea();
                Assert.AreEqual(result.IsSome(), true);
                Assert.AreEqual(result.Get(), 25f);
            }
        }
    }
}