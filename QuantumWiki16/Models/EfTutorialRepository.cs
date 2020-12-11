using System.Linq;

namespace QuantumWiki16.Models
{
    public class EfTutorialRepository
        : ITutorialRepository
    {
        // F i e l d s   &   P r o p e r t i e s

        private AppDbContext _context;

        //   C o n s t r u c t o r s

        public EfTutorialRepository(AppDbContext context)
        {
            _context = context;
        }

        //   M e t h o d s

        // C r e a t e

        public Tutorial AddTutorial(Tutorial tut)
        {
            _context.Tutorials.Add(tut);
            _context.SaveChanges();
            return tut;
        }

        // R e a d

        public IQueryable<Tutorial> GetAllTutorials()
        {
            return _context.Tutorials;
        }

        public Tutorial GetTutorialById(int id)
        {
            return _context.Tutorials.Find(id);
        }

        public IQueryable<Tutorial> GetTutorialByTitle(string keyword)
        {
            return _context.Tutorials.Where(u => u.Title.Contains(keyword));

        }

        public IQueryable<Tutorial> GetTutorialBySubject(string keyword)
        {
            return _context.Tutorials.Where(u => u.Subject.Contains(keyword));

        }

        // U p d a t e

        public Tutorial UpdateTutorial(Tutorial tut)
        {
            Tutorial tutorialToUpdate = _context.Tutorials.SingleOrDefault(t => t.TutId == tut.TutId);
            if (tutorialToUpdate != null)
            {
                tutorialToUpdate.Title = tut.Title;
                tutorialToUpdate.Url = tut.Url;
                tutorialToUpdate.Subject = tut.Subject;
                _context.SaveChanges();
            }
            return tutorialToUpdate;
        }

        // D e l e t e

        public bool DeleteTutorial(int id)
        {
            Tutorial tutorialToRemove = _context.Tutorials.Find(id);
            if (tutorialToRemove != null)
            {
                return false;
            }
            _context.Tutorials.Remove(tutorialToRemove);
            _context.SaveChanges();
            return true;
        }
    }
}
