using Microsoft.EntityFrameworkCore;
using SamplePipeDelimiter.Contexts;
using SamplePipeDelimiter.Models;
using System;

namespace SamplePipeDelimiter         // in here  write the logic to read pipe-delimited input from the console, parse it, and convert it into PubAccCO objects.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup DB context
            var optionsBuilder = new DbContextOptionsBuilder<UlsDbContext>();      // initializes an object to configure and build options for your UlsDbContext, which is then used to connect to your database. And over there in the UlsDbContext.cs we added a construtor to accept this options statement 
            optionsBuilder.UseNpgsql("User ID=localdbuser01;Password=localdbpassword)!;Host=localhost;Port=5432;Database=ulsdb;Connection Lifetime=0;");

            using (var context = new UlsDbContext(optionsBuilder.Options))
            {
                // Define your pipe-delimited lines
                string line1 = "CO|1102336||KA42382||E||";
                string line2 = "CO|1102461||KA49869||0||";
                string line3 = "CO|1102685||KA66163||0RADAR SPEED MONITORING DEVIC||";

                // Parse each line and insert into database
                InsertData(context, line1);                                               //  passing each line (e.g., line1, line2, line3) and the context (database context) to the InsertData method. The purpose is to process each line (parse the pipe-delimited data) and then insert the resulting data as a new record into your database using the Entity Framework Core's DbContext
                InsertData(context, line2);
                InsertData(context, line3);
            }
        }

        private static void InsertData(UlsDbContext context, string line)           // the string line here This is a single line of your pipe-delimited data. It represents one record that you want to insert into your database
        {
            try
            {
                var pubAccCO = ParseInput(line);              // here we are calling the ParseInput method and passing in the current pipe delimited line of data. This method is responsible to take the pipe delimited strings and convert them into instances of my table 'PubAccCO

                // Add the new object to the context and save changes
                context.PubAccCOs.Add(pubAccCO);                                  // This line adds the newly created pubAccCO object to the DbContext. PubAccCOs is a DbSet property in your context that represents the collection of all PubAccCO objects that are being tracked by the context. When you add an object to this collection, you're staging it for insertion into the corresponding table in your database.
                context.SaveChanges();                                           // saving changes to the database 

                Console.WriteLine($"Data inserted successfully for {pubAccCO.UniqueSystemIdentifier}.");
            }
            catch (Exception ex)                                                    //  This part catches any exceptions that may have been thrown during the processing, adding, or saving operations. It then prints out an error message to the console.
            {
                Console.WriteLine($"Error inserting data: {ex.Message}");
            }
        }

        private static PubAccCO ParseInput(string input)
        {                                                       //convert the pipe-delimited string into a PubAccCO object
            var parts = input.Split('|');                          // splits the input string into an array of strings, parts, using the pipe character (|) as the delimiter. Each element of the array corresponds to a part of the input string between the pipes.
            return new PubAccCO                              //creating a new PubAccCo object 
            {
                RecordType = parts[0],
                UniqueSystemIdentifier = decimal.Parse(parts[1]),
                UlsFileNum = parts[2],
                CallSign = parts[3],
                CommentDate = parts[4],
                Description = parts.Length > 5 ? parts[5] : null,
                StatusCode = parts.Length > 6 ? parts[6] : null,
                StatusDate = parts.Length > 7 && DateTime.TryParse(parts[7], out DateTime dateValue) ? dateValue : null
            };
        }
    }
}
