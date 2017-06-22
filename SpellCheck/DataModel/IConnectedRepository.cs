using System.Collections.Generic;
using SpellCheck.Entities;

namespace SpellCheck
{
    public interface IConnectedRepository
    {
        void AddTest(SpellTest test);
        List<Spelling> GetSpellings(int testId);
        List<SpellTest> GetTests();
        void UpdateTest(SpellTest test);
    }
}