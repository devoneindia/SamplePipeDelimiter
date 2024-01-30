using SamplePipeDelimiter.Contexts;
using SamplePipeDelimiter.Models;
using System;
using System.Collections.Generic;
using static SamplePipeDelimiter.Program;
namespace SamplePipeDelimiter;

public class Program
{

    public static void Main(string[] args)
    {
        string line1 = "CO|1102336||KA42382||E||";
        string line2 = "CO|1102461||KA49869||0||";
        string line3 = "CO|1102685||KA66163||0RADAR SPEED MONITORING DEVIC||";

        // Parse and add string values to a list of PubAccCO
        List<PubAccCO> pubAccCOs = new List<PubAccCO>();
        pubAccCOs.Add(ParseLine(line1));
        pubAccCOs.Add(ParseLine(line2));
        pubAccCOs.Add(ParseLine(line3));

        foreach (var pubAccCo in pubAccCOs)
        {
            using (var dbContext = new UlsDbContext())
            {
               // dbContext.Add(pubAccCo);
               dbContext.PubAccCOs.Add(pubAccCo);
                dbContext.SaveChanges();
            }
        }
        // Display the parsed PubAccCO values
        foreach (var value in pubAccCOs)
        {
            Console.WriteLine($"RecordType: {value.RecordType},UniqueSystemIdentifier: {value.UniqueSystemIdentifier}, UlsFileNum: {value.UlsFileNum},CallSign: {value.CallSign}, CommentDate: {value.CommentDate}, Description: {value.Description}, StatusCode: {value.StatusCode}, StatusDate:{value.StatusDate}");
        }

    }
    public static PubAccCO ParseLine(string line)
    {
        string[] fields = line.Split('|');
        if (fields.Length < 8)
        {
            throw new ArgumentException("Invalid input format. Expected at least 8 fields separated by '|'.");
        }
        PubAccCO pubAccCOs = new PubAccCO()
        {
            RecordType = fields[0],
            UniqueSystemIdentifier = decimal.Parse(fields[1]),
            UlsFileNum = fields[2],
            CallSign = fields[3],
            CommentDate = fields[4],
            Description = fields[5],
            StatusCode = fields[6],
            StatusDate = string.IsNullOrWhiteSpace(fields[7]) ? DateTime.MinValue : DateTime.Parse(fields[7]),
        };        
        return pubAccCOs;
    }



}

