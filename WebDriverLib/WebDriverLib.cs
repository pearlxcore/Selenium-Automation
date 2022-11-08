using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriverLib
{
    public class WebDriverLib
    {
        public static IWebDriver Driver { get; set; }
        public static ChromeDriverService DriverService { get; set; }
        public static ChromeOptions Option { get; set; }

        private static int Timeout = 2;

        public static bool IsClickable(string xpath, IWebDriver driver)
        {
            try
            {
                WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, Timeout));
                webDriverWait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static void WaitUrlContain(string text, IWebDriver driver)
        {
            WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, Timeout));
            webDriverWait.Until<bool>(ExpectedConditions.UrlContains(text));
        }

        public static void WaitElementVisible(string xpath, IWebDriver driver)
        {
            try
            {
                WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, Timeout));
                webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            }
            catch { }

        }

        public static void WaitElementExists(string xpath, IWebDriver driver)
        {
            WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, Timeout));
            webDriverWait.Until<IWebElement>(ExpectedConditions.ElementExists(By.XPath(xpath)));
        }

        public static void WaitElementClickable(string xpath, IWebDriver driver)
        {
            WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, Timeout));
            webDriverWait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
        }

        public static void ClickElement(IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static IWebElement NewElementByXpath(string xpath, IWebDriver driver)
        {
            IWebElement element;
            element = driver.FindElement(By.XPath(xpath));
            return element;
        }

        public static IWebElement CreateNewElement(By by, IWebDriver driver)
        {
            IWebElement element;
            element = driver.FindElement(by);
            return element;
        }

        public static IWebElement NewElementById(string id, IWebDriver driver)
        {
            IWebElement element;
            element = driver.FindElement(By.Id(id));
            return element;
        }

        public static bool IsElementDisplayed(By locator, IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout)).Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return driver.FindElement(locator).Displayed;
        }

        public static bool IsElementPresent(By by, IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static ChromeDriverService SetupDriver(string url)
        {
            DriverService = ChromeDriverService.CreateDefaultService();
            Option = new ChromeOptions();

            Option.AddArgument("--disable-extensions");
            DriverService.HideCommandPromptWindow = true;
            Option.AddArgument("--disable-blink-features=AutomationControlled");
            Option.AddArgument("user-data-dir=" + Environment.CurrentDirectory + "\\google_user_data");
            Option.AddArgument($"--profile-directory=Default");
            Option.PageLoadStrategy = PageLoadStrategy.Eager;
            Option.AddExcludedArgument("enable-automation");
            Option.AddArgument("no-sandbox");
            Driver = new ChromeDriver(DriverService, Option, TimeSpan.FromMinutes(3.0));
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            return DriverService;
        }
    }
}