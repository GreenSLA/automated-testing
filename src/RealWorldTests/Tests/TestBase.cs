using NUnit.Framework;

namespace RealWorldTests;

public class TestBase
{
    protected AppManager.AppManager app;

    [SetUp]
    public void SetupTest()
    {
        app = AppManager.AppManager.GetInstance();
    }
}
