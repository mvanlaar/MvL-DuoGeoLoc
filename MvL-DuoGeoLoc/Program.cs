using CsvHelper;
using CsvHelper.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MvL_DuoGeoLoc
{
    class Program
    {
        static void Main(string[] args)
        {

            string remoteUri = "https://onderwijsdata.duo.nl/dataset/46325a47-6a7f-42dd-979a-2ef083d4ca1e/resource/8dea0466-0c57-4b0d-bdfd-2d9d684111a1/download/vestigingenso.csv";
            string fileName = "vestigingenso.csv", myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            myStringWebResource = remoteUri + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, fileName);
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + System.Environment.CurrentDirectory);

            List<SchoolInfo> ScholenLijst = new List<SchoolInfo>();

            using (var reader = new StreamReader("vestigingenso.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.PrepareHeaderForMatch = (header, index) => Regex.Replace(header, "-", string.Empty);

                var records = csv.GetRecords<dynamic>();

                foreach (var record in records)
                {
                    ScholenLijst.Add(new SchoolInfo {
                        VESTIGINGSNAAM = record.VESTIGINGSNAAM,
                        STRAATNAAM = record.STRAATNAAM,
                        HUISNUMMER = record.HUISNUMMERTOEVOEGING,
                        POSTCODE = record.POSTCODE,
                        PLAATSNAAM = record.PLAATSNAAM,
                        TELEFOONNUMMER = record.TELEFOONNUMMER,
                        INTERNETADRES = record.INTERNETADRES,
                        DENOMINATIE = record.DENOMINATIE,
                        ONDERWIJS = record.ONDERWIJS
                    });
                }
            }




        }

        public class SchoolInfo
        {
            public string VESTIGINGSNAAM { get; set; }
            public string STRAATNAAM { get; set; }
            public string HUISNUMMER { get; set; }
            public string POSTCODE { get; set; }
            public string PLAATSNAAM { get; set; }
            public string DENOMINATIE { get; set; }
            public string TELEFOONNUMMER { get; set; }
            public string INTERNETADRES { get; set; }
            public string ONDERWIJS { get; set; }
            public double LAT { get; set; }
            public double LNG { get; set; }

        }
    }
}
