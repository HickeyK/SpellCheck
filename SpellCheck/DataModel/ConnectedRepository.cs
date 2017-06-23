using SpellCheck.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SpellCheck
{

    public class ConnectedRepository : IConnectedRepository
    {
        private readonly SpellCheckContext _context = new SpellCheckContext();



        public List<SpellTest> GetTests()
        {
            return _context.SpellTest.ToList();
        }

        public List<Spelling> GetSpellings(int testId)
        {

            var spellTest = _context.SpellTest.Include("Spellings").Where(st => st.Id == testId);

            //var t = spellTest.FirstOrDefault();

            return spellTest.FirstOrDefault().Spellings;
        }

        public void AddTest(SpellTest test)
        {
            _context.SpellTest.Add(test);
            _context.SaveChanges();
        }

        public void UpdateTest(SpellTest test)
        {
            //_context.SpellTest.Attach(test);
            _context.SaveChanges();
        }

        public List<TestOccurance> GetTestOccurances(int testId)
        {
            return _context.TestOccurance.Where(o => o.SpellTestId == testId).ToList();
        }

        public List<TestAnswer> GetAnswers(int testOccuranceId)
        {
            var t = _context.TestAnswer.Where(a => a.TestOccuranceId == testOccuranceId).ToList();
            return t;
        }

    }


}
