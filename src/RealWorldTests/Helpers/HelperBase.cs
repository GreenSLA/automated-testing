using OpenQA.Selenium;

namespace RealWorldTests;

public class HelperBase
{
    protected AppManager manager;
    protected IWebDriver driver;
    protected bool acceptNextAlert = true;

    public HelperBase(AppManager manager)
    {
        this.manager = manager;
        this.driver = manager.Driver;
    }

    protected bool IsElementPresent(By by)
    {
        try
        {
            driver.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    protected bool IsAlertPresent()
    {
        try
        {
            driver.SwitchTo().Alert();
            return true;
        }
        catch (NoAlertPresentException)
        {
            return false;
        }
    }

    protected string CloseAlertAndGetItsText()
    {
        try
        {
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text ?? string.Empty;
            if (acceptNextAlert)
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }

            return alertText;
        }
        finally
        {
            acceptNextAlert = true;
        }
    }
}
