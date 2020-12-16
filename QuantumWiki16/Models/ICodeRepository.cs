using System.Linq;

namespace QuantumWiki16.Models
{
    public interface ICodeRepository
    {
        // C r e a t e

        public Code AddCode(Code code);

        // R e a d

        public IQueryable<Code> GetAllCode();
        public Code GetCodeById(int id);
        public IQueryable<Code> GetCodeByTitle(string keyword);
        public IQueryable<Code> GetCodeByDescription(string keyword);
        public IQueryable<Code> GetCodeByCodeSegment(string keyword);
        public IQueryable<Code> GetCodeByUser(int userId);
        public IQueryable<Code> GetCodeByLanguage(int langId);  // after Lang built

        // U p d a t e

        public Code UpdateCode(Code code);

        // D e l e t e

        public bool DeleteCode(int id);

    }
}
