namespace Web_Scraping.Utilities
{
    public class PoliticalParty
    {
        public static string Party(string input)
        {
            string party = input switch
            {
                //BN
                string s when s.Contains("UMNO") => "BN",
                string s when s.Contains("BN") => "BN",
                string s when s.Contains("MCA") => "BN",
                string s when s.Contains("MIC") => "BN",
                string s when s.Contains("PBRS") => "BN",

                //PH
                string s when s.Contains("DAP") => "PH",
                string s when s.Contains("PKR") => "PH",
                string s when s.Contains("AMANAH") => "PH",
                string s when s.Contains("UPKO") => "PH",
                string s when s.Contains("MUDA") => "PH",

                //PN
                string s when s.Contains("PN") => "PN",
                string s when s.Contains("BERSATU") => "PN",
                string s when s.Contains("PAS") => "PN",
                string s when s.Contains("GERAKAN") => "PN",

                //GPS
                string s when s.Contains("GPS") => "GPS",
                string s when s.Contains("PBB") => "GPS",
                string s when s.Contains("PRS") => "GPS",
                string s when s.Contains("PDP") => "GPS",
                string s when s.Contains("SUPP") => "GPS",

                //GRS
                string s when s.Contains("SAPP") => "GRS",
                string s when s.Contains("STAR") => "GRS",
                string s when s.Contains("GRS") => "GRS",
                string s when s.Contains("GPS") => "GRS",
                string s when s.Contains("PBS") => "GRS",

                //Pejuang
                string s when s.Contains("Pejuang") => "Pejuang",
                string s when s.Contains("IMAN") => "Pejuang",
                string s when s.Contains("Putra") => "Pejuang",
                string s when s.Contains("Berjasa") => "Pejuang",
                string s when s.Contains("GTA") => "Pejuang",

                //Warisan
                string s when s.Contains("Warisan") => "Warisan",

                //BEBAS/IND
                string s when s.Contains("BEBAS") => "BEBAS",
                string s when s.Contains("IND") => "BEBAS",

                _ => "Others"
            };
            return party;
        }

        public static string GetFullPartyName(string input)
        {
            string partyName = input switch
            {
                string s when s.Contains("BN") => "Barisan Nasional",
                string s when s.Contains("PH") => "Pakatan harapan",
                string s when s.Contains("PN") => "Perikatan Nasional",
                string s when s.Contains("PAS") => "Perikatan Nasional",
                string s when s.Contains("Bersatu") => "Perikatan Nasional",

                string s when s.Contains("Pejuang") => "Pejuang",
                string s when s.Contains("GPS") => "Gabungan Parti Sarawak",
                string s when s.Contains("GRS") => "Gabungan Rakyat Sabah",
                string s when s.Contains("AMANAH") => "Pakatan harapan",
                _ => "Others"
            };
            return partyName;
        }
    }
}
