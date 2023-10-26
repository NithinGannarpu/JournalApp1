using System;
using static System.Console;
using System.IO;
namespace JournalApp
{
	public class Journal

	{

        private string journalName = "MyJournal.txt";

        private string startUpText = @"1. Add an Entry.
2. Delete the entire Journal.
3. Display all entries.
4. Exit.";

        private string art = @"+--------------+
   |.------------.|
   ||            ||
   || JournalApp ||
   ||            ||
   ||            ||
   |+------------+|
   +-..--------..-+
   .--------------.
  / /============\ \
 / /==============\ \
/____________________\
\____________________/";


        public void run()
		{
            Title = "Journal App";
            displayIntro();
            WriteLine("Welcome to your journal!!\n");
            
            createJournalFile();

            startUpScreen();

            displayOutro();
            
		}

        private void startUpScreen()
        {
            WriteLine("\nWhat would you like to do?\n");
            WriteLine(startUpText);
            string? opt= ReadLine();

            switch (opt)
            {
                case "1":

                    addEntry();
                    break;


                case "2":

                    clearFile();
                    break;

                case "3":

                    displayJournalContents();
                    break;

                case "4":

                    waitForKey();
                    break;

                default:
                    WriteLine("Please enter a valid option.");
                    startUpScreen();
                    break;



            }
        }

        private void createJournalFile()
        {
            try
            {
                if (!File.Exists(journalName))
                {
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;

                    File.CreateText(journalName);
                    WriteLine("Made a new file right here!!");

                }
            }
            catch (IOException ex)
            {

                WriteLine("An error occurred while creating the journal file: " + ex.Message);

            }
            catch (UnauthorizedAccessException ex)
            {

                WriteLine("You don't have permission to create the journal file: " + ex.Message);
                
            }
        }


        private void displayIntro()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;
            Clear();
            WriteLine(art);
            
        }

        private void displayOutro()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;

            WriteLine("Thank you for using it!!");
            //waitForKey();
            Clear();
        }

        private void waitForKey()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Hit enter to proceed!!");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                WriteLine("Hit enter to proceed!!");
            }
        }

        private void displayJournalContents()
        {
            try
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._");
                string[] readText = File.ReadAllLines(journalName);
                foreach (string s in readText)
                {
                    WriteLine(s);
                }
                WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._");
                waitForKey();

                startUpScreen();
            }
            catch (IOException ex)
            {

                WriteLine("An error occurred while displaying journal contents: " + ex.Message);
                
            }
        }


        private void clearFile()
        {
            File.WriteAllText(journalName, "");
            WriteLine("\nJOurnal has been cleared!!");
            waitForKey();
            startUpScreen();
        }

        private void addEntry()

        {
            try
            { 

                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("\nWhat would you like to add to your journal? ");
                string entry = ReadLine();
                ForegroundColor = ConsoleColor.DarkGreen;

                File.AppendAllText(journalName,$"\nEntry>> {entry}");
                WriteLine("\n An entry has been added!!");

                startUpScreen();

            }
            catch (IOException ex)
            {

                WriteLine("An error occurred while adding an entry: " + ex.Message);
               

            }

        }

    }

}


