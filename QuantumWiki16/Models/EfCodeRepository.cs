using Microsoft.AspNetCore.Http;
using System.Linq;

namespace QuantumWiki16.Models
{
    public class EfCodeRepository :
        ICodeRepository
    {
        //   F i e l d s   &   P r o p e r t i e s

        private AppDbContext _context;
        private ISession _session;

        //   C o n s t r u c t o r s

        public EfCodeRepository(AppDbContext context, IHttpContextAccessor sessionContext)
        {
            _context = context;
            _session = sessionContext.HttpContext.Session;  // currently created session
        }

        //   M e t h o d s

        // C r e a t e

        public Code AddCode(Code code)
        {
            _context.Codes.Add(code);
            _context.SaveChanges();
            return code;

        }

        // R e a d

        public IQueryable<Code> GetAllCode()
        {
            return _context.Codes;
        }  // end Get all code

        public Code GetCodeById(int id)
        {
            return _context.Codes.Find(id);  // could be null
        }  // Get code by id

        public IQueryable<Code> GetCodeByTitle(string keyword)
        {
            return _context.Codes.Where(u => u.Title.Contains(keyword));
        }  // get code by title

        public IQueryable<Code> GetCodeByDescription(string keyword)
        {
            return _context.Codes.Where(u => u.Description.Contains(keyword));
        }  // end get code by description

        public IQueryable<Code> GetCodeByCodeSegment(string keyword)
        {
            return _context.Codes.Where(u => u.CodeSegment.Contains(keyword));
        }  // end get code by code segment

        public IQueryable<Code> GetCodeByUser(int userId)
        {
            return _context.Codes.Where(u => u.Id == userId);
        }
        
        public IQueryable<Code> GetCodeByLanguage(int langId)  // after Lang built
        {
            return _context.Codes.Where(c => c.LangId == langId);
        }  // end get code by Lang Id

        // U p d a t e

        public Code UpdateCode(Code code)
        {
            Code codeToUpdate = _context.Codes.FirstOrDefault(c => c.CodeId == code.CodeId);
            if (codeToUpdate != null)
            {
                codeToUpdate.Title = code.Title;
                codeToUpdate.Description = code.Description;
                codeToUpdate.CodeSegment = code.CodeSegment;
                // only keys remain unchanged.
                _context.SaveChanges();
            }
            return codeToUpdate;

        }

        // D e l e t e

        public bool DeleteCode(int id)
        {
            Code codeToRemove = _context.Codes.Find(id);
            if (codeToRemove == null)
            {
                return false;  // this id is not in the database
            }
            _context.Codes.Remove(codeToRemove);
            _context.SaveChanges();
            return true;
        }  // end Delete code

    }
}
