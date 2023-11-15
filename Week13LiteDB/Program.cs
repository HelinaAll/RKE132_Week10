using System.Data.SQLite;

ReadData(CreateConnection());


static SQLiteConnection CreateConnection()
{

    SQLiteConnection connection = new SQLiteConnection("Data Source_mydb.db; Version = 3; New = True; Compress = True;");

      try
      {
          connection.Open();
          Console.WriteLine("DB found");
      }
      catch
      {
          Console.WriteLine("DB not found");
      }

      return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT customer.firstName, customer.lastName, status.statustype " +
        "FROM customerStatus " +
        "JOIN customer on customer.rowid = customerStatus.customerId " +
        "JOIN status on status.rowid = customerStatus.statusId " +
        "ORDER BY status.statustype ";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerStringFirstName = reader.GetString(0);
        string readerStringLastName = reader.GetString(1);
        string readerStringStatus = reader.GetString(2);

        Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; Status {readerStringStatus}");
    }

    myConnection.Close();
}
