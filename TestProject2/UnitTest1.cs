using NUnit.Framework;
using System.Windows.Controls;
using System.Threading;
using Projekt.View;

namespace TestProject2
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class Tests
    {
        private MainWindow _mainWindow;
        private TextBlock _textBlock;

        [SetUp]
        public void Setup()
        {
            _mainWindow = new MainWindow();
            _mainWindow.Show();
           
        }

        [TearDown]
        public void TearDown()
        {
            _mainWindow.Close();
        }

        [Test]
        public void TestTextBlockContent()
        {
           
        }
    }
}
