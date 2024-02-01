using System;
using System.Collections.Generic;
using System.Linq;
using LoadDataProject.DbContexts;
using LoadDataProject.Models;
using Microsoft.EntityFrameworkCore;
namespace LoadDataProject;

public class Program
{
    private static readonly PubAccDbContext dbContext = new PubAccDbContext();
    public static void Main(string[] args)
    {
        List<string> inputLines = new List<string>
            {
                "EM|3532741|||WQSS791|1|1|123.25500000||6K00A3E|||1|||1",
                "EM|3532746|||WQSS796|1|1|156.80000000||16K0F3E|||2|||1",
                "EM|3532746|||WQSS796|1|1|156.90000000||16K0F3E|||3|||1",
                "EM|3532746|||WQSS796|1|1|156.45000000||6K00A3E|||1|||1",
                "EM|3534463|||WQSS796|1|1|122.95000000||16K0F3E|||1|||1",
                "EM|3534573|||WQSS845|1|1|978.95000000||650KM1A|||1|||1"
            };

        List<PubAccEM> entitiesToAdd = new List<PubAccEM>();
        foreach (var line in inputLines)
        {
            string[] values = line.Split('|');
            if (values.Length == 16)
            {
        PubAccEM pubAccCOModel = CreateEntityFromValues(values);

                // Detach any existing entity with the same key from the context
                //var existingEntity = dbContext.PubAccEMs.Local.SingleOrDefault(e => e.UniqueSystemIdentifier == pubAccCOModel.UniqueSystemIdentifier)
                //                    ?? dbContext.PubAccEMs.SingleOrDefault(e => e.UniqueSystemIdentifier == pubAccCOModel.UniqueSystemIdentifier);
                var existingEntity = dbContext.PubAccEMs.Find(pubAccCOModel.UniqueSystemIdentifier);
                if (existingEntity != null)
                {
                    dbContext.PubAccEMs.Add(pubAccCOModel);
                }
                else
                {
                    dbContext.PubAccEMs.Entry(existingEntity).CurrentValues.SetValues(pubAccCOModel);
                }

            }
        }
        //dbContext.PubAccEMs.AddRange(entitiesToAdd);
        dbContext.SaveChanges();
    }
    private static int ParseInt(string value)
    {
        int result;
        return int.TryParse(value, out result) ? result : 0;
    }

    private static double ParseDouble(string value)
    {
        double result;
        return double.TryParse(value, out result) ? result : 0.0;
    }

    private static DateTime ParseDateTime(string value)
    {
        DateTime result;
        return DateTime.TryParse(value, out result) ? result : DateTime.MinValue;
    }
    private static decimal ParseDecimal(string value)
    {
        decimal result;
        return decimal.TryParse(value, out result) ? result : 0.0m;
    }


    private static PubAccEM CreateEntityFromValues(string[] values)
    {
        return new PubAccEM
        {
            RecordType = values[0],
            UniqueSystemIdentifier = ParseInt(values[1]),
            UlsFileNumber = values[2],
            EbfNumber = values[3],
            CallSign = values[4],
            LocationNumber = ParseInt(values[5]),
            AntennaNumber = ParseInt(values[6]),
            FrequencyAssigned = (decimal?)ParseDouble(values[7]),
            EmissionActionPerformed = values[8],
            EmissionCode = values[9],
            DigitalModRate = ParseInt(values[10]),
            DigitalModType = values[11],
            FrequencyNumber = ParseInt(values[12]),
            StatusCode = values[13],
            StatusDate = ParseDateTime(values[14]),
            EmissionSequenceId = ParseInt(values[15])
        };

    }
}
