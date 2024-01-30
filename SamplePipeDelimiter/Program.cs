    using Microsoft.EntityFrameworkCore;
    using SamplePipeDelimiter.Contexts;
    using SamplePipeDelimiter.Models;
    using System.Xml;

    namespace SamplePipeDelimiter;

    public class Program
    {
    public static PubAccCO PubAccCOModel { get; set; }
        public static void Main(string[] args)
        {

            using (UlsDbContext dbContext = new UlsDbContext())
            {
                // Step 5: Create and Insert Data
                string line1 = "CO|1102336||KA42382||E||";
                string[] values = line1.Split('|');


            string input = Console.ReadLine();
            if (input != null && decimal.TryParse(input, out decimal uniqueSystemIdentifier))
            {
                Console.Write("Enter RecordType: ");
                string recordType = Console.ReadLine();

                Console.Write("Enter UlsFileNum: ");
                string ulsFileNum = Console.ReadLine();

                Console.Write("Enter CallSign: ");
                string callSign = Console.ReadLine();

                Console.Write("Enter CommentDate: ");
                string commentDate = Console.ReadLine();

                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter StatusCode: ");
                string statusCode = Console.ReadLine();

                PubAccCO PubAccoModel = new PubAccCO
                    {
                        UniqueSystemIdentifier = uniqueSystemIdentifier,
                        RecordType = values[1],
                        UlsFileNum = values[3],
                        CallSign = values[4],
                        CommentDate = values[5],
                        Description = values[6],
                        StatusCode = values[7],
                    };



                    // Add entity to DbSet
                    dbContext.PubAccCOs.Add(PubAccoModel);

                    // Save changes to the database
                    dbContext.SaveChanges();
                }
            }
        Console.ReadLine();
    }
    }
