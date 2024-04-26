using System;
using System.Data;
using System.Data.SqlClient;

public class Matchmaking
{
    static Guid TempGetPlayerId(string player_name)
    {
        // Connexion à la base de données
        string connectionString = "Server=localhost;Port=5432;Database=my_api_rest;User Id=me;Password=password";
        
        // Get id from user
        Guid playerId = null; // Le retour
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT id FROM users WHERE name = '{player_name}'";
            // Exécutez la requête et récupérez l'ID du joueur qui est un string
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                playerId = (Guid)command.ExecuteScalar();
            }

            Console.WriteLine($"L'ID du joueur avec le nom {player_name} est {playerId}");
            
            connection.Close();
            
        }
        
        return playerId;
    }
    
    static string GetPlayerRank(Guid playerId)
    {
        string connectionString = "Server=localhost;Port=5432;Database=my_api_rest;User Id=me;Password=password";
        string rank = null; // Le retour
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT rank FROM users WHERE Id = '{playerId}'";
            // Exécutez la requête et récupérez le rang du joueur qui est un string
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                rank = (string)command.ExecuteScalar();
            }

            Console.WriteLine($"Le rang du joueur avec l'ID {playerId} est {rank}");

            connection.Close();
            
        }
        
        return rank;
    }

    static void Main()
    {
        // Execute la fonction GetPlayerRank avec un ID de joueur spécifique
        Guid playerId = TempGetPlayerId("me");
        string playerRank = GetPlayerRank(playerId);
    }
}