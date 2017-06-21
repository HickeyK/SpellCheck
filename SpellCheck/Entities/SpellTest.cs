using System;
using System.Collections.Generic;

namespace SpellCheck.Entities
{
    public class SpellTest
    {
        public SpellTest()
        {
            DateCreated = DateTime.Today;
            Spellings = new List<Spelling>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Spelling> Spellings { get; set; }

        public List<TestOccurance>  TestOccurances { get; set; }
    }
}
