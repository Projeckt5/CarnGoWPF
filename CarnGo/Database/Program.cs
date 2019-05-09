using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CarnGo.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to the database console interface");
            Console.WriteLine("Your choice of commands are:" +
                              "\n\"1\" : Create Database" +
                              "\n\"2\" : Pull all data" +
                              "\n\"3\" : Empty database" +
                              "\n\"4\" : Seed database");
            do
            {
                try
                {
                    Console.Write("> ");
                    var command = Console.ReadLine();
                    switch (command)
                    {

                        case "1":
                            Commands.CreateDatabase();
                            break;

                        case "2":
                            Task.Run(Commands.PullAllData);
                            break;

                        case "3":
                            Commands.EmptyDatabase();
                            break;

                        case "4":
                            Task.Run(Commands.SeedDatabase);
                            break;

                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid database request" +
                                      "\nWant to review exeption?" +
                                      "\ny/N");
                    string answer = Console.ReadLine();
                    if(answer == "y" || answer == "Y")
                        Console.WriteLine(e);
                    else if (answer == "n" || answer == "N" || answer == ""){}
                    else
                    {
                        Console.WriteLine("Invalid command, try again");
                    }

                }

            } while (true);
        }
    }
}
