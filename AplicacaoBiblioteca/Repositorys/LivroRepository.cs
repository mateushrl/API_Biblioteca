using AplicacaoBiblioteca.Interfaces;
using AplicacaoBiblioteca.Models;
using AplicacaoBiblioteca.Models.DTO;
using Microsoft.Data.SqlClient;

namespace AplicacaoBiblioteca.Services
{

    public class LivroRepository : ILivroRepository
    {
        private readonly string _connectionString;
        public LivroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<LivroDTO> ObterLivros()
        {
            List<LivroDTO> listaLivros = new();

            using (SqlConnection connection = new(_connectionString))
            {

                const string query = "SELECT Id, Titulo, Autor FROM Livro";
                SqlCommand command = new(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listaLivros.Add(new LivroDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                        Autor = reader.GetString(reader.GetOrdinal("Autor"))
                    });
                }
                reader.Close();
            }

            return listaLivros;
        }
        public Livro CriarLivro(Livro livro)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                const string query = "INSERT INTO Livro (Titulo, Autor) VALUES (@Titulo, @Autor)";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@Titulo", livro.Titulo);
                command.Parameters.AddWithValue("@Autor", livro.Autor);

                connection.Open();

                int livroId = (int)command.ExecuteNonQuery();

                livro.Id = livroId;

                connection.Close();
            }

            return livro;
        }

        public int DeletarLivro(int id)
        {
            int numeroLinhasAfetadas = 0;
            using (SqlConnection connection = new(_connectionString))
            {
                const string query = "DELETE FROM Livro WHERE Id = @Id";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                numeroLinhasAfetadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return numeroLinhasAfetadas;

        }

        public int AtualizarLivro(Livro livro)
        {
            int numeroLinhasAfetadas = 0;
            using (SqlConnection connection = new(_connectionString))
            {
                const string query = "UPDATE Livro SET Titulo = @Titulo, Autor = @Autor WHERE Id = @Id";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@Id", livro.Id);
                command.Parameters.AddWithValue("@Titulo", livro.Titulo);
                command.Parameters.AddWithValue("@Autor", livro.Autor);

                connection.Open();

                numeroLinhasAfetadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return numeroLinhasAfetadas;
        }
    }

}
