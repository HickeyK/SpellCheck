using SpellCheck.Entities;
using System.Collections.Generic;
using System.Linq;

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

        public bool SaveTestOccurance(TestOccurance testOccurance)
        {

            _context.TestOccurance.Add(testOccurance);
            _context.SaveChanges();
            return true;
        }

        public void SaveTestAnswers(List<TestAnswer> testAnswers)
        {
            foreach (var answer in testAnswers)
            {
                _context.TestAnswer.Add(answer);
            }
            _context.SaveChanges();
        }

        public void DeleteTest(SpellTest spellTest)
        {
            if (spellTest.TestOccurances != null)
            {
                foreach (var o in spellTest.TestOccurances)
                {
                    var ta = _context.TestAnswer.Where(a => a.TestOccuranceId == o.Id).ToList();
                    _context.TestAnswer.RemoveRange(ta);
                }

                _context.TestOccurance.RemoveRange(spellTest.TestOccurances);
            }

            if (spellTest.Spellings != null)
            {
                _context.Spelling.RemoveRange(spellTest.Spellings);
            }

            _context.SpellTest.Remove((spellTest));

            _context.SaveChanges();
        }
    }


}
