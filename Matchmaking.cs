using System;
using System.Data;
using System.Data.SqlClient;

public class Matchmaking
{
    static void GetPlayerRank()
}

public class Program
{
    static void Main()
    {
        string connectionString = "Votre_chaine_de_connexion"; // Remplacez par votre propre chaîne de connexion

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Exemple de requête SELECT
            string selectQuery = "SELECT * FROM VotreTable";
            DataTable dataTable = ExecuteQuery(connection, selectQuery);

            // Traiter les résultats
            foreach (DataRow row in dataTable.Rows)
            {
                // Accéder aux colonnes comme ceci
                int id = (int)row["ID"];
                string name = (string)row["Nom"];

                Console.WriteLine($"ID: {id}, Nom: {name}");
            }

            // Exemple de requête INSERT
            string insertQuery = "INSERT INTO VotreTable (Nom, Age) VALUES ('John Doe', 30)";
            int rowsAffected = ExecuteNonQuery(connection, insertQuery);

            Console.WriteLine($"Nombre de lignes affectées: {rowsAffected}");

            connection.Close();
        }
    }

    // Méthode pour exécuter une requête SELECT et renvoyer les résultats sous forme de DataTable
    private static DataTable ExecuteQuery(SqlConnection connection, string query)
    {
        DataTable dataTable = new DataTable();

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
        }

        return dataTable;
    }

    // Méthode pour exécuter une requête INSERT, UPDATE, DELETE, etc. et renvoyer le nombre de lignes affectées
    private static int ExecuteNonQuery(SqlConnection connection, string query)
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            return command.ExecuteNonQuery();
        }
    }
}