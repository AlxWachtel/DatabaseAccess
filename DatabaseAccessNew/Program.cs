using System;
using MySql.Data.MySqlClient;

namespace DatabaseAccess
{
    class Program
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting database access procedure... \n");
            Console.WriteLine("Initialize connection...\n");
            Boolean check = initializeConnection();
            while (check == false)
            {
                Console.WriteLine("Do you want to enter the data again? Please enter Y or N.");
                String answer = Console.ReadLine();
                if (answer.Equals("Y"))
                {
                    check = initializeConnection();
                }
                else
                {
                    check = true;
                }
            }

            check = false;
            while (check == false)
            {
                Console.WriteLine("Please enter MySQL Command: ");
                String command = Console.ReadLine();
                executeCommand(command);
                Console.WriteLine("Do you want to enter another command? Y or N:");
                String answer = Console.ReadLine();
                if (answer.Equals("N"))
                {
                    check = true;
                }
            }

            Console.Write("Press any key to close the programm.");
            Console.ReadKey(true);
        }

        static Boolean initializeConnection()
        {
            Console.WriteLine("Please enter the server ip.");
            server = Console.ReadLine();
            Console.WriteLine("Please enter the database name.");
            database = Console.ReadLine();
            Console.WriteLine("Please enter the username.");
            uid = Console.ReadLine();
            Console.WriteLine("Please enter the password.");
            password = Console.ReadLine();


            Console.WriteLine("Thank You! Trying to establish connection...\n");
            string connectionString = "server=" + server + ";" + "uid=" + uid + ";" + "pwd=" + password + ";" + "database=" + database;
            Console.WriteLine("Connection String: " + connectionString);
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connection successful established.");
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error, could not establish connection!");
                return false;
            }
        }

        static void executeCommand(String commandText)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                MySqlDataReader reader = cmd.ExecuteReader();
                printResult(reader);
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error, please check command and try again.");
            }
        }

        static void printResult(MySqlDataReader reader)
        {

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    String matrNr = reader.GetString(0);
                    String vorname = reader.GetString(1);
                    String nachname = reader.GetString(2);
                    String adresse = reader.GetString(3);
                    String stadt = reader.GetString(4);
                    String plz = reader.GetString(5);
                    String fach = reader.GetString(6);
                    Console.WriteLine(matrNr + ";" + vorname + ";" + nachname + ";" + adresse + ";" + stadt + ";" + plz + ";" + fach);
                }
            }

        }
    }
}
