namespace RealWorldTests.Tests;

public class TestBase
{
    protected AppManager.AppManager app;

    [SetUp]
    public void SetupTest()
    {
        app = AppManager.AppManager.GetInstance();
        app.Navigation.OpenHomePage();
    }
}
