using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Web_Scraping__Selenium_.Model;
using Web_Scraping__Selenium_.Utilities;

namespace Web_Scraping__Selenium_
{
    internal class Program
    {
        static IWebDriver Driver { get; set; }
        static List<CandidateModel.Root> modelList = new List<CandidateModel.Root>();

        static void Main(string[] args)
        {
            //seting up chromedriver
            SetupDriver();

            //start scraping from https://www.hmetro.com.my/utama/2022/11/899856/pru-15-senarai-calon-parlimen and convert output to json format
            ScrapData();
        }

        private static void ScrapData()
        {
            //first data's xpath is //*[@id="main"].../p[4] and end with /p[225] so just iterate every element using for loop
            //code is working but output value is messed up due to typos and html formatting error on webpage
            //additional code to fix those value but too lazy to fix them all

            for (int i = 4; i < 226; i++)
            {
                var el = DriverHelper.NewElementByXpath($"//*[@id=\"main\"]/div/div[1]/div[1]/div[1]/div/div/div[2]/div[2]/div/p[{i}]", Driver);
                var model = new CandidateModel.Root();
                var candidateList = model.CandidateDetail;
                using (StringReader reader = new StringReader(el.Text))
                {
                    string line = string.Empty;

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null && line.StartsWith("P"))
                        {

                            var parlimentCode = "";
                            var parlimentName = "";
                            if (line.Equals("P.008: POKOK SENAbr>1. Noraini Md Salleh (Warisan)"))
                            {
                                int idx = line.LastIndexOf('(');

                                var parlimentLine = line.Substring(0, 19);
                                var candidateLine = line.Substring(20);

                                parlimentCode = parlimentLine.Split(':')[0];
                                parlimentName = parlimentLine.Split(':')[1];

                                model.ParlimentName = parlimentName.Replace("br", "");
                                model.ParlimentCode = parlimentCode.TrimStart().TrimEnd();

                                var candidateName = line.Substring(0, idx);
                                var partyName = line.Substring(idx + 1);
                                candidateName = candidateName.Substring(candidateName.IndexOf('.') + 1).TrimStart().TrimEnd();
                                partyName = partyName.Replace(")", "");

                                candidateList.Add(new CandidateModel.CandidateDetail
                                {
                                    CandidateName = candidateName,
                                    Party = PoliticalParty.Party(partyName)
                                });
                            }
                            else
                            {
                                if (line.Contains(':'))
                                {
                                    parlimentCode = line.Split(':')[0];
                                    parlimentName = line.Split(':')[1];
                                }
                                else
                                {
                                    parlimentCode = line.Split(' ')[0];
                                    parlimentName = line.Split(' ')[1];
                                }

                                model.ParlimentName = parlimentName;
                                model.ParlimentCode = parlimentCode.TrimStart().TrimEnd();
                            }
                        }
                        else if (line != null && !line.StartsWith("P.") && (!line.Equals("Ads By") || !line.Equals("ADVERTISEMENT")))
                        {
                            var candidateName = "";
                            var partyName = "";

                            int idx = line.LastIndexOf('(');

                            if (line.Equals("2. Datuk Dr Mohd Khairuddin Aman RazaliBN-Calon Langsung/Direct Candidate"))
                            {
                                candidateName = line.Substring(0, 38);
                                partyName = line.Substring(39);
                            }
                            else
                            {
                                candidateName = line.Substring(0, idx);
                                partyName = line.Substring(idx + 1);
                            }

                            candidateName = candidateName.Substring(candidateName.IndexOf('.') + 1).TrimStart().TrimEnd();
                            partyName = partyName.Replace(")", "");

                            candidateList.Add(new CandidateModel.CandidateDetail
                            {
                                CandidateName = candidateName,
                                Party = PoliticalParty.Party(partyName)
                            });
                        }
                    }
                }
                modelList.Add(model);
            }
            var json = JsonConvert.SerializeObject(modelList);
            Console.ReadLine();
        }

        private static void SetupDriver()
        {

            DriverHelper.DriverService = ChromeDriverService.CreateDefaultService();
            DriverHelper.Option = new ChromeOptions();
            DriverHelper.Option.AddArgument("--disable-extensions");
            DriverHelper.DriverService.HideCommandPromptWindow = true;
            DriverHelper.Option.AddArgument("--disable-blink-features=AutomationControlled");
            DriverHelper.Option.AddArgument("user-data-dir=" + Environment.CurrentDirectory + "\\google_user_data");
            DriverHelper.Option.AddArgument($"--profile-directory=Default");
            DriverHelper.Option.PageLoadStrategy = PageLoadStrategy.Eager;
            DriverHelper.Option.AddExcludedArgument("enable-automation");
            DriverHelper.Option.AddArgument("no-sandbox");
            Driver = new ChromeDriver(DriverHelper.DriverService, DriverHelper.Option, TimeSpan.FromMinutes(3.0));
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.hmetro.com.my/utama/2022/11/899856/pru-15-senarai-calon-parlimen");
            //wait chrome to load
            Thread.Sleep(1000);
        }
    }
}