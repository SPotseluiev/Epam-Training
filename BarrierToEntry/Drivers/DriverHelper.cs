using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BarrierToEntry.Drivers
{
    public class DriverHelper
    {
        public static IWebDriver driver = new ChromeDriver();
    }
}