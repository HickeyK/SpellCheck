using SpellCheck.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SpellCheck
{

    class ConnectedRepository
    {
        private readonly SpellCheckContext _context = new SpellCheckContext();



        public List<SpellTest> GetTests()
        {

            return _context.SpellTest.ToList();

        }

        public List<Spelling> GetSpellings(int testId)
        {

            var spellTest = _context.SpellTest.Include("Spellings").Where(st => st.Id == testId);


            var t = spellTest.FirstOrDefault();

            return spellTest.FirstOrDefault().Spellings;
        }
    }
}
