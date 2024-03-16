using projektWSP;

namespace nununitWSP
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MainWindow_Initialization_SetsTextBoxTextToHelloWorld()
        {

            var mainWindow = new MainWindow();
            var expectedText = "Hello, World!";
            Assert.AreEqual(expectedText, mainWindow.Textbox1.Text);
        }
    }
}