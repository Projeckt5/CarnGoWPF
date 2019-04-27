using System;
using System.Runtime.CompilerServices;

namespace CarnGo.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to the database console interface");
            Console.WriteLine("Your choice of commands are:" +
                              "\n\"1\" : Pull all data" +
                              "\n\"2\" : Empty database");
            do
            {
                try
                {
                    Console.Write("> ");
                    var command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                                Commands.PullAllData();
                            break;

                        case "2":
                            Commands.EmptyDatabase();
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
