using System.Linq;

namespace QuantumWiki16.Models
{
    public interface ITutorialRepository
    {
        // C r e a t e

        public Tutorial AddTutorial(Tutorial tut);

        // R e a d

        public IQueryable<Tutorial> GetAllTutorials();
        public Tutorial GetTutorialById(int id);
        public IQueryable<Tutorial> GetTutorialByTitle(string keyword);
        public IQueryable<Tutorial> GetTutorialBySubject(string keyword);

        // U p d a t e

        public Tutorial UpdateTutorial(Tutorial tut);

        // D e l e t e

        public bool DeleteTutorial(int id);
    }
}
