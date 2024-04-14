using Dane;
using Moq;
using System.ComponentModel;
namespace DaneTest
{
    public class Tests
    {
        private Ball ball;
        private double value = 100;

        [SetUp]
        public void Setup()
        {
            ball = new Ball();
            value = 100;

        }

        [Test]
        public void TestPropertyChanged()
        {
            var mock=new Mock<PropertyChangedEventHandler>();
            ball.PropertyChanged += mock.Object;
            ball.X = value;
            mock.Verify(m => m(ball, It.Is<PropertyChangedEventArgs>(args => args.PropertyName == "X")), Times.Once);
            ball.Y = value;
            mock.Verify(m => m(ball, It.Is<PropertyChangedEventArgs>(args => args.PropertyName == "Y")), Times.Once);
            Assert.Pass();
        }
    }
}