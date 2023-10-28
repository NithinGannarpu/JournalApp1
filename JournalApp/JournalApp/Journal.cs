using System;
using static System.Console;
using System.IO;
namespace JournalApp
{
	public class Journal

	{

        public string journalName = "MyJournal.txt";

        public string startUpText = @"1. Add an Entry.
2. Delete the entire Journal.
3. Display all entries.
4. Exit.";

        public string art = @"+--------------+
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


        public virtual void run()
		{
            Title = "Journal App";
            displayIntro();
            WriteLine("Welcome to your journal!!\n");
            
            createJournalFile(journalName);

            startUpScreen(journalName);

            displayOutro();
            
		}

        public virtual void startUpScreen(string jj)
        {

            WriteLine("\nWhat would you like to do?\n");
            WriteLine(startUpText);
            string? opt= ReadLine();

            switch (opt)
            {
                case "1":

                    addEntry(jj);
                    break;

                case "2":

                    clearFile(jj);
                    break;

                case "3":

                    displayJournalContents(jj);
                    break;

                case "4":

                    waitForKey();
                    break;

                default:
                    WriteLine("Please enter a valid option.");
                    startUpScreen(jj);
                    break;


            }

        }

        public virtual void createJournalFile(string journalNam)
        {
            try
            {
                if (!File.Exists(journalNam))
                {
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;

                    File.CreateText(journalNam);
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


        public virtual void displayIntro()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;
            Clear();
            WriteLine(art);
            
        }

        public void displayOutro()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;

            WriteLine("Thank you for using it!!");
            waitForKey();
            Clear();
        }

        public virtual void waitForKey()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Hit enter to proceed!!");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                WriteLine("Hit enter to proceed!!");
            }
        }

        public virtual void displayJournalContents(string jname)
        {
            try
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._");
                string[] readText = File.ReadAllLines(jname);
                foreach (string s in readText)
                {
                    WriteLine(s);
                }
                WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._");
                waitForKey();

                startUpScreen(jname);
            }
            catch (IOException ex)
            {

                WriteLine("An error occurred while displaying journal contents: " + ex.Message);
                
            }
        }


        public virtual void clearFile(string jname)
        {
            File.WriteAllText(jname, "");
            WriteLine("\nJOurnal has been cleared!!");
            waitForKey();
            startUpScreen(jname);
        }



        public virtual void addEntry(string jname)

        {
            try
            { 

                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("\nWhat would you like to add to your journal? ");
                string entry = ReadLine();
                ForegroundColor = ConsoleColor.DarkGreen;

                File.AppendAllText(jname,$"\nEntry>> {entry}");
                WriteLine("\n An entry has been added!!");

                startUpScreen(jname);

            }
            catch (IOException ex)
            {

                WriteLine("An error occurred while adding an entry: " + ex.Message);
               

            }

        }

    }

}


