using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static WebDriverLib.WebDriverLib;

namespace Facebook_Login
{
    internal class Program
    {
        static string CreateNewAccountBtn = "//a[contains(@class, '_42ft _4jy0 _6lti _4jy6 _4jy2 selected _51sy') and contains(text(), 'Create New Account')]";
        static string FirstNameInputEl = "//input[contains(@name, 'firstname')]";
        static string LastNameInputEl = "//input[contains(@name, 'lastname')]";
        static string EmailInputEl = "//input[contains(@name, 'reg_email__')]";
        static string ConfirmEmailInputEl = "//input[contains(@name, 'reg_email_confirmation__')]";
        static string PasswordInputEl = "//input[contains(@name, 'reg_passwd__')]";
        static string BirthdayDayEl = "//select[contains(@name, 'birthday_day')]";
        static string BirthdayMonthEl = "//select[contains(@name, 'birthday_month')]";
        static string BirthdayYearEl = "//select[contains(@name, 'birthday_year')]";
        static string MaleRadioEl = "//label[contains(@class, '_58mt') and contains(text(), 'Male')]";
        static string FemaleRadioEl = "//label[contains(@class, '_58mt') and contains(text(), 'Female')]";
        static string SubmitBtnEl = "//button[contains(@name, 'websubmit')]";

        static string firstName = "John";
        static string lastName = "Doe";
        static string email = "test@gmail.com";
        static string password = "abcd1234!";

        static void Main(string[] args)
        {
            //check if chromdriver.exe exists
            if (!File.Exists("chromedriver.exe"))
            {
                Console.WriteLine("Missing chromedriver.exe");
                return;
            }

            //setup webdriver
            var driver = SetupDriver("https://www.facebook.com/");
            if (!IsElementPresent(By.XPath(CreateNewAccountBtn), Driver))
                WaitElementExists(CreateNewAccountBtn, Driver);

            //create button
            var CreatebtnEl = CreateNewElement(By.XPath(CreateNewAccountBtn), Driver);
            CreatebtnEl.Click();

            Thread.Sleep(1000);

            //login info
            var inputFirstNameEl = CreateNewElement(By.XPath(FirstNameInputEl), Driver);
            inputFirstNameEl.SendKeys(firstName);

            var inputLastNameEl = CreateNewElement(By.XPath(LastNameInputEl), Driver);
            inputLastNameEl.SendKeys(lastName);

            var inputEmailEl = CreateNewElement(By.XPath(EmailInputEl), Driver);
            inputEmailEl.SendKeys(email);

            var inputConfirmEmailEl = CreateNewElement(By.XPath(ConfirmEmailInputEl), Driver);
            inputConfirmEmailEl.SendKeys(email);

            var inputPasswordEl = CreateNewElement(By.XPath(PasswordInputEl), Driver);
            inputPasswordEl.SendKeys(password);

            //birthday
            SelectElement DaySelect = new SelectElement(CreateNewElement(By.XPath(BirthdayDayEl), Driver));
            DaySelect.SelectByValue("1");

            SelectElement MonthSelect = new SelectElement(CreateNewElement(By.XPath(BirthdayMonthEl), Driver));
            MonthSelect.SelectByValue("6");

            SelectElement YearSelect = new SelectElement(CreateNewElement(By.XPath(BirthdayYearEl), Driver));
            YearSelect.SelectByValue("1992");

            //sex
            var maleradioEl = CreateNewElement(By.XPath(MaleRadioEl), Driver);
            maleradioEl.Click();

            //submit button
            var submitBtnEl = CreateNewElement(By.XPath(SubmitBtnEl), Driver);
            submitBtnEl.Click();

            Console.ReadLine();
        }
    }
}