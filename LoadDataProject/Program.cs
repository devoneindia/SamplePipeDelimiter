﻿using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SamplePipeDelimiter.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using LoadDataProject.Models;
using Npgsql;

namespace LoadDataProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set up DB context
            var optionsBuilder = new DbContextOptionsBuilder<PubAccDbContext>();
            using (var context = new PubAccDbContext(optionsBuilder.Options))
            {
                string line1 = "EM|3532741|||WQSS791|1|1|123.25500000||6K00A3E|||1|||1";
                string line2 = "EM|3532746|||WQSS796|1|1|156.80000000||16K0F3E|||2|||1";
                string line3 = "EM|3532746|||WQSS796|1|1|156.90000000||16K0F3E|||3|||1";
                string line4 = "EM|3532746|||WQSS796|1|1|156.45000000||6K00A3E|||1|||1";
                string line5 = "EM|3534463|||WQSS796|1|1|122.95000000||16K0F3E|||1|||1";
                string line6 = "EM|3534573|||WQSS845|1|1|978.95000000||650KM1A|||1|||1";

                // Parse each line and insert it into the database 
                InsertData(context, line1);
                InsertData(context, line2);
                InsertData(context, line3);
                InsertData(context, line4);
                InsertData(context, line5);
                InsertData(context, line6);
            }
        }

        private static PubAccEM ParseInput(string input)
        {
            var parts = input.Split('|');
            return new PubAccEM
            {
                RecordType = parts[0],
                UniqueSystemIdentifier = decimal.Parse(parts[1]),
                UlsFileNumber = parts[2],
                EbfNumber = parts[3],
                CallSign = parts[4],
                LocationNumber = string.IsNullOrEmpty(parts[5]) ? null : int.Parse(parts[5]),
                AntennaNumber = string.IsNullOrEmpty(parts[6]) ? null : int.Parse(parts[6]),
                FrequencyAssigned = string.IsNullOrEmpty(parts[7]) ? null : decimal.Parse(parts[7]),
                EmissionActionPerformed = parts[8],
                EmissionCode = parts[9],
                DigitalModRate = string.IsNullOrEmpty(parts[10]) ? null : decimal.Parse(parts[10]),
                DigitalModType = parts[11],
                FrequencyNumber = string.IsNullOrEmpty(parts[12]) ? null : int.Parse(parts[12]),
                StatusCode = parts[13],
                StatusDate = string.IsNullOrEmpty(parts[14]) ? null : DateTime.Parse(parts[14]),
                EmissionSequenceId = string.IsNullOrEmpty(parts[15]) ? null : int.Parse(parts[15])
            };
        }

        private static void InsertData(PubAccDbContext context, string line)
        {
            // Your ParseInput method needs to be defined correctly outside this method
            var pubAccEM = ParseInput(line); // Assuming this method returns a PubAccEM object

            // Construct your SQL INSERT statement
            string sql = @"INSERT INTO main.pubacc_em 
                    (record_type, unique_system_identifier, uls_file_number, ebf_number, call_sign, location_number, antenna_number, frequency_assigned, emission_action_performed, emission_code, digital_mod_rate, digital_mod_type, frequency_number, status_code, status_date, emission_sequence_id) 
                    VALUES 
                    (@RecordType, @UniqueSystemIdentifier, @UlsFileNumber, @EbfNumber, @CallSign, @LocationNumber, @AntennaNumber, @FrequencyAssigned, @EmissionActionPerformed, @EmissionCode, @DigitalModRate, @DigitalModType, @FrequencyNumber, @StatusCode, @StatusDate, @EmissionSequenceId)";

            // Execute the SQL command
            context.Database.ExecuteSqlRaw(sql,
                new NpgsqlParameter("@RecordType", pubAccEM.RecordType ?? (object)DBNull.Value),
                new NpgsqlParameter("@UniqueSystemIdentifier", pubAccEM.UniqueSystemIdentifier),
                new NpgsqlParameter("@UlsFileNumber", pubAccEM.UlsFileNumber ?? (object)DBNull.Value),
                new NpgsqlParameter("@EbfNumber", pubAccEM.EbfNumber ?? (object)DBNull.Value),
                new NpgsqlParameter("@CallSign", pubAccEM.CallSign ?? (object)DBNull.Value),
                new NpgsqlParameter("@LocationNumber", pubAccEM.LocationNumber ?? (object)DBNull.Value),
                new NpgsqlParameter("@AntennaNumber", pubAccEM.AntennaNumber ?? (object)DBNull.Value),
                new NpgsqlParameter("@FrequencyAssigned", pubAccEM.FrequencyAssigned ?? (object)DBNull.Value),
                new NpgsqlParameter("@EmissionActionPerformed", pubAccEM.EmissionActionPerformed ?? (object)DBNull.Value),
                new NpgsqlParameter("@EmissionCode", pubAccEM.EmissionCode ?? (object)DBNull.Value),
                new NpgsqlParameter("@DigitalModRate", pubAccEM.DigitalModRate ?? (object)DBNull.Value),
                new NpgsqlParameter("@DigitalModType", pubAccEM.DigitalModType ?? (object)DBNull.Value),
                new NpgsqlParameter("@FrequencyNumber", pubAccEM.FrequencyNumber ?? (object)DBNull.Value),
                new NpgsqlParameter("@StatusCode", pubAccEM.StatusCode ?? (object)DBNull.Value),
                new NpgsqlParameter("@StatusDate", pubAccEM.StatusDate ?? (object)DBNull.Value),
                new NpgsqlParameter("@EmissionSequenceId", pubAccEM.EmissionSequenceId ?? (object)DBNull.Value)
            );

            Console.WriteLine($"Data inserted successfully for {pubAccEM.UniqueSystemIdentifier}.");
        }
    }
}