using AplicacaoBiblioteca.Models;
using AplicacaoBiblioteca.Models.DTO;

namespace AplicacaoBiblioteca.Interfaces
{
    public interface ILivroRepository
    {
        List<LivroDTO> ObterLivros();
        public Livro CriarLivro(Livro livro);
        /// <summary>
        /// Deleta livro
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Numero de linhas afetadas, retorna 0 se registros não for encontrado</returns>
        public int DeletarLivro(int id);
        
        /// <summary>
        /// Atualiza livro
        /// </summary>
        /// <param name="livro"></param>
        /// <returns>Numero de linhas afetadas, retorna 0 se registros não for encontrado</returns>
        public int AtualizarLivro(Livro livro);

    }
}
