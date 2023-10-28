using System;
using static System.Console;
using MySql.Data.MySqlClient;
namespace JournalApp
{
	public class JAppV2 : Journal
	{
        public new string art = " _________\n|^|     | |\n| |_____| |\n|  _____  |\n| |     | |\n| |_____| |\n|_|_____|_|\n";
        public string startUp = "Please type 1 for new user\nPlease type to 2 for existing user.";
		public override void run()
		{
            Title = "JournalApp2.0";
            
            wakeUpScreen();
            displayOutro();
            WriteLine("Cleared!!");

        }

        public override void displayIntro()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;
            Clear();
            WriteLine(art+"\n");
            WriteLine("Are you a new user or existing user?\n");

        }

        public void wakeUpScreen()
        {
            Clear();
            displayIntro();
            WriteLine(startUp);
            string? op = ReadLine();
            switch (op)
            {
                case "1":

                    newUser();
                    break;


                case "2":

                    existingUser();
                    break;

                default:
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("Please enter a valid option.");
                    ForegroundColor = ConsoleColor.Green;
                    wakeUpScreen();
                    break;

            }

        }

        
        public void newUser()
        {

            Clear();
            WriteLine("Would you like to register?\nPlease type Y for yes \n Please type N for no.");

            var comp = ReadLine();
            if (comp == "n")
            {
                WriteLine("Byee");
            }
            else if(comp== "y")
            {
                Clear();
                WriteLine("Welcome to the registration page");
                registration();
            }
            else
            {
                newUser();
            }


        }

        public void registration()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkRed;
            BackgroundColor = ConsoleColor.DarkCyan;
            WriteLine("This is the reistration Page");
            WriteLine("\nPlease enter your details :");
            WriteLine("\nFirst Name:");
            string? fname = ReadLine();
            WriteLine("\nLast Name:");
            string? lname = ReadLine();

            sqlshit(fname,lname);
            WriteLine("User registered successfully!!");
            waitForKey();
        }


        public void sqlshit(string fn, string ln)
        {
            bool continueRegistration = true;

            do
            {
                Clear();
                WriteLine("\nUsername: ");
                string uname = ReadLine();
                string connectionString = "Server=localhost;Database=album;User ID=root;Password=Howdy_bitch@2002;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                try
                {
                    conn.Open();
                    string query = "select username from userData;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    bool usernameExists = false;

                    while (reader.Read())
                    {
                        if (reader["username"].ToString() == uname)
                        {
                            usernameExists = true;
                            break;
                        }
                    }

                    reader.Close(); 

                    if (!usernameExists)
                    {
                        WriteLine("\nPassword: ");
                        string pass = ReadLine();
                        string insertSql = "INSERT INTO userData (firstname, lastname, username, password) VALUES (@Value1, @Value2, @Value3, @Value4)";
                        MySqlCommand cmdi = new MySqlCommand(insertSql, conn);
                        cmdi.Parameters.AddWithValue("@Value1", fn);
                        cmdi.Parameters.AddWithValue("@Value2", ln);
                        cmdi.Parameters.AddWithValue("@Value3", uname);
                        cmdi.Parameters.AddWithValue("@Value4", pass);
                        cmdi.ExecuteNonQuery();
                        break; 
                    }
                    else
                    {
                        WriteLine("Username already exists. Do you want to try another username? (Y/N): ");
                        string tryAnother = ReadLine();
                        if (tryAnother.Trim().ToUpper() != "Y")
                        {
                            continueRegistration = false;
                            break;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            } while (continueRegistration); 
        }


        //private bool trueCreds = false;


        public void existingUser()
        {
            Clear();
            WriteLine("Username: ");
            string unam = ReadLine();
            WriteLine("Password: ");
            string pas = ReadLine();

            string connectionString = "Server=localhost;Database=album;User ID=root;Password=Howdy_bitch@2002;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "select username,password from userData;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                bool validCredentials = false;

                while (reader.Read())
                {
                    if (reader["username"].ToString() == unam && reader["password"].ToString() == pas)
                    {
                        validCredentials = true;
                        break;
                    }
                }

                reader.Close();
                conn.Close();

                if (validCredentials)
                {
                    Clear();
                    WriteLine("Welcome User");
                    WriteLine(startUpText);
                    createJournalFile(unam);
                    startUpScreen(unam);
                }
                else
                {
                    WriteLine("Please enter the right details");
                    existingUser();
                }
            }
            catch (MySqlException ex)
            {
                WriteLine(ex.ToString());
            }
        }



    }
}

