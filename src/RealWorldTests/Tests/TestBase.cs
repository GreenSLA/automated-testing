using NUnit.Framework;

namespace RealWorldTests;

public class TestBase
{
    protected AppManager app;

    [SetUp]
    public void SetupTest()
    {
        app = AppManager.GetInstance();
    }
}
