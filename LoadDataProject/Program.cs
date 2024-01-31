using LoadDataProject.DbContexts;
using LoadDataProject.Models;
using System;

namespace LoadDataProject
{
    public class Program
    {
        public static readonly PubAccDbContext dbContext = new PubAccDbContext();

        public static void Main(string[] args)
        {
            using (var context = new PubAccDbContext())
            {
                string EmissionData = "em|3532741|||wqss791|1|1|123.25500000||6k00a3e|||1|||1" +
                                     "em|3532746|||wqss796|1|1|156.80000000||16k0f3e|||2|||1" +
                                     "em|3532746|||wqss796|1|1|156.90000000||16k0f3e|||3|||1" +
                                     "em|3532746|||wqss796|1|1|156.45000000||6k00a3e|||1|||1" +
                                     "em|3534463|||wqss796|1|1|122.95000000||16k0f3e|||1|||1" +
                                     "em|3534573|||wqss845|1|1|978.95000000||650km1a|||1|||1";
                InsertDataFromPipeDelimitedString(EmissionData, context);
            }
        }

        public static void InsertDataFromPipeDelimitedString(string EmissionData, PubAccDbContext dbContext)
        {
            string[] lines = EmissionData.Split('\n');

            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                if (values.Length == 16)
                {
                    PubAccEM record = new PubAccEM
                    {
                        RecordType = values[0],
                        UniqueSystemIdentifier = Convert.ToDecimal(values[1]),
                        UlsFileNumber = values[2],
                        EbfNumber = values[3],
                        CallSign = values[4],
                        LocationNumber = Convert.ToInt32(values[5]),
                        AntennaNumber = Convert.ToInt32(values[6]),
                        FrequencyAssigned = (decimal?)Convert.ToDouble(values[7]),
                        EmissionActionPerformed = values[8],
                        EmissionCode = values[9],
                        DigitalModRate = Convert.ToDecimal(values[10]),
                        DigitalModType = values[11],
                        FrequencyNumber = Convert.ToInt32(values[12]),
                        StatusCode = values[13],
                        StatusDate = Convert.ToDateTime(values[14]),
                        EmissionSequenceId = Convert.ToInt32(values[15])
                    };

                    dbContext.PubAccEMs.Add(record);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}

